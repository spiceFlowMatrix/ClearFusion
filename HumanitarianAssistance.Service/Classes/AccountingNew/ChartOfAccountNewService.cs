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
        public async Task<APIResponse> GetMainLevelAccount(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                var mainLevelList = await _uow.ChartOfAccountNewRepository.FindAllAsync(x => x.AccountLevelId == id && x.IsDeleted == false);

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
                                                                    .Where(x => x.ChartOfAccountNewId != parentId && x.ParentID == parentId && x.IsDeleted == false)
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




        public async Task<APIResponse> AddChartOfAccount(ChartOfAccountNewModel model)
        {
            APIResponse response = new APIResponse();

            try
            {
                bool sameAccount = await _uow.GetDbContext().ChartOfAccountNew.AnyAsync(x => x.AccountName.ToLower() == model.AccountName.ToLower());
                if (!sameAccount)
                {
                    ChartOfAccountNew obj1 = new ChartOfAccountNew();

                    //Main Level
                    if (model.AccountLevelId == (int)AccountLevels.MainLevel)
                    {
                        int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.MainLevel);

                        if (levelcount <= (int)AccountLevelLimits.MainLevel)
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
                            obj.ChartOfAccountNewCode = obj.ChartOfAccountNewId.ToString();

                            await _uow.ChartOfAccountNewRepository.UpdateAsyn(obj);

                            response.CommonId.LongId = obj.ChartOfAccountNewId;
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
                        int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.ControlLevel);

                        if (levelcount <= (int)AccountLevelLimits.ControlLevel)
                        {
                            ChartOfAccountNew obj = new ChartOfAccountNew();

                            bool parentPresent = await _uow.GetDbContext().ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewId == model.ParentID);

                            if (parentPresent)
                            {
                                obj.AccountLevelId = (int)AccountLevels.ControlLevel;
                                obj.AccountHeadTypeId = model.AccountHeadTypeId;
                                obj.ParentID = model.ParentID;
                                obj.AccountName = model.AccountName;
                                obj.CreatedById = model.CreatedById;
                                obj.CreatedDate = model.CreatedDate;
                                obj.IsDeleted = false;

                                await _uow.ChartOfAccountNewRepository.AddAsyn(obj);

                                response.CommonId.LongId = obj.ChartOfAccountNewId;
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
                        int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.SubLevel);

                        if (levelcount <= (int)AccountLevelLimits.SubLevel)
                        {
                            ChartOfAccountNew obj = new ChartOfAccountNew();

                            bool parentPresent = await _uow.GetDbContext().ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewId == model.ParentID);

                            if (parentPresent)
                            {
                                obj.AccountLevelId = (int)AccountLevels.ControlLevel;
                                obj.AccountHeadTypeId = model.AccountHeadTypeId;
                                obj.ParentID = model.ParentID;
                                obj.AccountName = model.AccountName;

                                obj.AccountFilterTypeId = model.AccountFilterTypeId != null ? model.AccountFilterTypeId : null;  //dropdown
                                obj.AccountTypeId = model.AccountTypeId != null ? model.AccountTypeId : null; //dropdown

                                obj.CreatedById = model.CreatedById;
                                obj.CreatedDate = model.CreatedDate;
                                obj.IsDeleted = false;

                                await _uow.ChartOfAccountNewRepository.AddAsyn(obj);

                                response.CommonId.LongId = obj.ChartOfAccountNewId;
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
                        int levelcount = await _uow.GetDbContext().ChartOfAccountNew.CountAsync(x => x.AccountLevelId == (int)AccountLevels.InputLevel);

                        if (levelcount <= (int)AccountLevelLimits.InputLevel)
                        {
                            ChartOfAccountNew obj = new ChartOfAccountNew();

                            bool parentPresent = await _uow.GetDbContext().ChartOfAccountNew.AnyAsync(x => x.ChartOfAccountNewId == model.ParentID);

                            if (parentPresent)
                            {
                                obj.AccountLevelId = (int)AccountLevels.ControlLevel;
                                obj.AccountHeadTypeId = model.AccountHeadTypeId;
                                obj.ParentID = model.ParentID;
                                obj.AccountName = model.AccountName;
                                obj.CreatedById = model.CreatedById;
                                obj.CreatedDate = model.CreatedDate;
                                obj.IsDeleted = false;

                                await _uow.ChartOfAccountNewRepository.AddAsyn(obj);

                                response.CommonId.LongId = obj.ChartOfAccountNewId;
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
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }


    }

}
