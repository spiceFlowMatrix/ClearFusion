using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using Microsoft.EntityFrameworkCore;
using DataAccess.DbEntities.AccountingNew;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Service.Classes.AccountingNew
{

    public class ChartOfAccountNewService : IChartOfAccountNewService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;

        public ChartOfAccountNewService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        /// <summary>
        /// Get Main Level Account (EXPECTED VALUE: 1)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List</returns>
        public async Task<APIResponse> GetMainLevelAccount(long id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var mainLevelList = await _uow.GetDbContext().ChartOfAccountNew
                                                             .Where(x => x.AccountHeadTypeId == id && x.AccountLevelId == (int)AccountLevels.MainLevel && x.IsDeleted == false)
                                                             .OrderBy(x=>x.ChartOfAccountNewId)
                                                             .ToListAsync();

                response.data.MainLevelAccountList = mainLevelList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllAccountsByParentId(long parentId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var mainLevelList = await _uow.GetDbContext().ChartOfAccountNew
                                                                    .Where(x => x.ChartOfAccountNewId != parentId &&
                                                                                x.ParentID == parentId &&
                                                                                x.IsDeleted == false)
                                                                    .OrderBy(x=>x.ChartOfAccountNewId)
                                                                    .ToListAsync();

                response.data.AllAccountList = mainLevelList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllAccountsByAccountHeadTypeId(long id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var mainLevelList = await _uow.GetDbContext().ChartOfAccountNew
                                                                        .Where(x => x.AccountHeadTypeId == id && x.IsDeleted == false)
                                                                        .OrderBy(x => x.ChartOfAccountNewId)
                                                                        .ToListAsync();

                response.data.AllAccountList = mainLevelList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<bool> GetAccountBalanceTypeByAccountType(int accountTypeId)
        {
            var accountType = await _uow.GetDbContext().AccountType.Where(x => x.AccountTypeId == accountTypeId)
                .FirstOrDefaultAsync();
            var accountHeadType = await _uow.GetDbContext().AccountHeadType
                .Where(x => x.AccountHeadTypeId == accountType.AccountHeadTypeId).FirstOrDefaultAsync();
            return accountHeadType.IsCreditBalancetype;
        }

        public async Task<APIResponse> AddChartOfAccount(ChartOfAccountNewModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {
                    if (model.AccountName != null)
                    {
                        model.AccountName = model.AccountName.Trim();
                    }
                }

                //bool sameAccount = await _uow.GetDbContext().ChartOfAccountNew.AnyAsync(x => x.AccountName.ToLower() == model.AccountName.ToLower());
                //if (!sameAccount)
                //{
                //Main Level
                if (model.AccountLevelId == (int)AccountLevels.MainLevel)
                {
                    int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.MainLevel && x.IsDeleted == false);

                    if (levelcount < (int)AccountLevelLimits.MainLevel)
                    {
                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        obj.AccountLevelId = (int)AccountLevels.MainLevel;
                        obj.AccountHeadTypeId = model.AccountHeadTypeId;
                        obj.ParentID = -1;
                        obj.AccountName = model.AccountName;
                        obj.CreatedById = model.CreatedById;
                        obj.CreatedDate = model.CreatedDate;
                        obj.IsDeleted = false;

                        await _uow.ChartOfAccountNewRepository.AddAsyn(obj);

                        obj.ParentID = obj.ChartOfAccountNewId;
                        obj.ChartOfAccountNewCode = (levelcount + 1).ToString();

                        await _uow.ChartOfAccountNewRepository.UpdateAsyn(obj);

                        response.data.ChartOfAccountNewDetail = obj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }

                //Control Level
                else if (model.AccountLevelId == (int)AccountLevels.ControlLevel)
                {
                    int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.ControlLevel && x.ParentID == model.ParentID);

                    if (levelcount < (int)AccountLevelLimits.ControlLevel)
                    {
                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        //bool parentPresent = await _uow.GetDbContext().ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewId == model.ParentID);

                        ChartOfAccountNew parentPresent = await _uow.GetDbContext().ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == model.ParentID);

                        if (parentPresent != null)
                        {
                            obj.AccountLevelId = (int)AccountLevels.ControlLevel;
                            obj.AccountHeadTypeId = model.AccountHeadTypeId;
                            obj.ParentID = model.ParentID;
                            obj.ChartOfAccountNewCode = parentPresent.ChartOfAccountNewCode + (levelcount + 1);
                            obj.AccountName = model.AccountName;
                            obj.CreatedById = model.CreatedById;
                            obj.CreatedDate = model.CreatedDate;
                            obj.IsDeleted = false;

                            await _uow.ChartOfAccountNewRepository.AddAsyn(obj);

                            response.data.ChartOfAccountNewDetail = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.ParentIdNotPresent;
                        }

                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }

                //Sub Level
                else if (model.AccountLevelId == (int)AccountLevels.SubLevel)
                {
                    int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.SubLevel && x.ParentID == model.ParentID);

                    if (levelcount < (int)AccountLevelLimits.SubLevel)
                    {
                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        ChartOfAccountNew parentPresent = await _uow.GetDbContext().ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == model.ParentID);

                        if (parentPresent != null)
                        {

                            obj.AccountLevelId = (int)AccountLevels.SubLevel;
                            obj.AccountHeadTypeId = model.AccountHeadTypeId;
                            obj.ParentID = model.ParentID;
                            obj.AccountName = model.AccountName;
                            obj.ChartOfAccountNewCode = parentPresent.ChartOfAccountNewCode + genrateCode((levelcount + 1).ToString());

                            obj.AccountFilterTypeId = model.AccountFilterTypeId != null ? model.AccountFilterTypeId : null;  //dropdown
                            // obj.AccountTypeId = model.AccountTypeId != null ? model.AccountTypeId : null; //dropdown

                            if (model.AccountTypeId != null)
                            {
                                obj.AccountTypeId = model.AccountTypeId;
                                // Get balance type.
                                obj.IsCreditBalancetype =
                                    await GetAccountBalanceTypeByAccountType((int) obj.AccountTypeId);
                            }
                            else
                            {
                                obj.AccountTypeId = null;
                                obj.IsCreditBalancetype = null;
                            }

                            obj.CreatedById = model.CreatedById;
                            obj.CreatedDate = model.CreatedDate;
                            obj.IsDeleted = false;

                            await _uow.ChartOfAccountNewRepository.AddAsyn(obj);

                            response.data.ChartOfAccountNewDetail = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.ParentIdNotPresent;
                        }

                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }

                //Input Level
                else if (model.AccountLevelId == (int)AccountLevels.InputLevel)
                {
                    int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.InputLevel && x.ParentID == model.ParentID);

                    if (levelcount < (int)AccountLevelLimits.InputLevel)
                    {
                        ChartOfAccountNew obj = new ChartOfAccountNew();

                        ChartOfAccountNew parentPresent = await _uow.GetDbContext().ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == model.ParentID);

                        if (parentPresent != null)
                        {
                            obj.AccountLevelId = (int)AccountLevels.InputLevel;
                            obj.AccountHeadTypeId = model.AccountHeadTypeId;
                            obj.ParentID = model.ParentID;
                            obj.ChartOfAccountNewCode = parentPresent.ChartOfAccountNewCode + genrateCode((levelcount + 1).ToString());
                            obj.AccountName = model.AccountName;
                            obj.CreatedById = model.CreatedById;
                            obj.CreatedDate = model.CreatedDate;

                            // set account type and balance type
                            obj.AccountTypeId = parentPresent.AccountTypeId;
                            obj.IsCreditBalancetype = parentPresent.IsCreditBalancetype;
                            obj.AccountFilterTypeId = parentPresent.AccountFilterTypeId;
                            //////////////////////

                            obj.IsDeleted = false;

                            await _uow.ChartOfAccountNewRepository.AddAsyn(obj);

                            response.data.ChartOfAccountNewDetail = obj;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = StaticResource.SuccessText;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.ParentIdNotPresent;
                        }

                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.ExceedLevelCount;
                    }
                }
                //}
                //else
                //{
                //    response.StatusCode = StaticResource.failStatusCode;
                //    response.Message = StaticResource.MandateNameAlreadyExist;
                //}

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllAccountFilter()
        {
            APIResponse response = new APIResponse();
            try
            {
                var mainLevelList = await _uow.GetDbContext().AccountFilterType.Where(x => x.IsDeleted == false).ToListAsync();

                response.data.AllAccountFilterList = mainLevelList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task UpdateBalanceMetadataForInputAccounts(ChartOfAccountNew subLvlAccount)
        {
            var accounts = await _uow.GetDbContext().ChartOfAccountNew.Where(x => x.ParentID == subLvlAccount.ChartOfAccountNewId).ToListAsync();
            var accType = await _uow.GetDbContext().AccountType
                .FirstOrDefaultAsync(x => x.AccountTypeId == subLvlAccount.AccountTypeId);
            var accHeadType = await _uow.GetDbContext().AccountHeadType
                .FirstOrDefaultAsync(x => x.AccountHeadTypeId == accType.AccountHeadTypeId);
            subLvlAccount.IsCreditBalancetype = accHeadType.IsCreditBalancetype;
            _uow.GetDbContext().ChartOfAccountNew.Update(subLvlAccount);
            foreach (var account in accounts)
            {
                account.IsCreditBalancetype = subLvlAccount.IsCreditBalancetype;
                account.AccountTypeId = subLvlAccount.AccountTypeId;
                account.AccountFilterTypeId = subLvlAccount.AccountFilterTypeId;
            }
            _uow.GetDbContext().ChartOfAccountNew.UpdateRange(accounts);
            await _uow.GetDbContext().SaveChangesAsync();
        }

        public async Task<APIResponse> EditChartOfAccount(ChartOfAccountNewModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {
                    var accountDetail = await _uow.GetDbContext().ChartOfAccountNew.FirstOrDefaultAsync(x => x.ChartOfAccountNewId == model.ChartOfAccountNewId);

                    if (accountDetail != null)
                    {
                        accountDetail.AccountName = model.AccountName?.Trim();
                        accountDetail.AccountTypeId = model.AccountTypeId;
                        accountDetail.AccountFilterTypeId = model.AccountFilterTypeId;

                        accountDetail.ModifiedDate = model.ModifiedDate;
                        accountDetail.IsDeleted = false;


                        await _uow.ChartOfAccountNewRepository.UpdateAsyn(accountDetail);

                        if (accountDetail.AccountLevelId == (int)AccountLevels.SubLevel)
                        {
                            // Updated all input-level accounts' account types and balance types if true
                            await UpdateBalanceMetadataForInputAccounts(accountDetail);
                        }

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = StaticResource.SuccessText;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.NoDataFound;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }


        //Create code
        public string genrateCode(string id)
        {
            string code = string.Empty;
            if (id.Length == 1)
                return code = "0" + id;
            else
                return code = id;
        }

    }
}
