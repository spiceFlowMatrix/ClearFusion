using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class ChartAccountDetailService : IChartAccoutDetail
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public ChartAccountDetailService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetAllChartAccountDetail()
        {
            APIResponse response = new APIResponse();
            try
            {
                var charlist = await Task.Run(() =>
                  _uow.GetDbContext().ChartAccountDetail.Include(c => c.AccountType).Where(a => a.IsDeleted == false).ToList()
                    );
                var chartaccountlist = charlist.Select(blog => new ChartAccountDetailModel
                {
                    AccountCode = blog.AccountCode,
                    AccountName = blog.AccountName,
                    AccountLevelId = blog.AccountLevelId,
                    AccountTypeName = blog.AccountType.AccountTypeName,
                    AccountTypeId = blog.AccountType.AccountTypeId,
                    ParentID = blog.ParentID,
                    ChartOfAccountCode = blog.ChartOfAccountCode
                    //DepRate = blog.DepRate,
                    //DepMethod = blog.DepMethod,
                    //MDCode = blog.MDCode,
                    //Show = blog.Show
                }).ToList();
                response.data.ChartAccountList = chartaccountlist;
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

        public async Task<APIResponse> GetAllAccountLevel()
        {
            APIResponse response = new APIResponse();
            try
            {
                var accountlevellist = (from a in await _uow.AccountLevelRepository.GetAllAsyn()
                                        orderby a.AccountLevelId ascending
                                        select new AccountLevelModel
                                        {
                                            AccountLevelId = a.AccountLevelId,
                                            AccountLevelName = a.AccountLevelName
                                        }).ToList();
                response.data.AccountLevelList = accountlevellist;
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

        public async Task<APIResponse> GetAllAccountTypeByCategory(int id)
        {
            APIResponse response = new APIResponse();
            try
            {
                List<AccountType> accounttypelist = await _uow.GetDbContext().AccountType
                                                              .Where(x => !x.IsDeleted.Value && x.AccountCategory == id)
                                                              .OrderBy(x => x.CreatedDate)
                                                              .ToListAsync();
                response.data.AccountTypeList = accounttypelist;
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

        public async Task<APIResponse> AddAccountType(AccountTypeModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                bool sameAccount = await _uow.GetDbContext().AccountType.AnyAsync(x => x.AccountTypeName.ToLower() == model.AccountTypeName.ToLower());
                if (!sameAccount)
                {
                    AccountType obj = new AccountType();

                    obj.AccountCategory = model.AccountCategory;
                    obj.AccountHeadTypeId = model.AccountHeadTypeId.Value;
                    obj.AccountTypeName = model.AccountTypeName;
                    obj.CreatedById = model.CreatedById;
                    obj.CreatedDate = model.CreatedDate;
                    obj.IsDeleted = false;

                    await _uow.AccountTypeRepository.AddAsyn(obj);

                    response.CommonId.Id = obj.AccountTypeId;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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


        public async Task<APIResponse> EditAccountType(AccountTypeModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var accountTypeDetail = await _uow.GetDbContext().AccountType.FirstOrDefaultAsync(x => x.AccountTypeId == model.AccountTypeId);

                bool sameAccountName = await _uow.GetDbContext().AccountType.AnyAsync(x =>
                                                                                x.AccountTypeName.ToLower() == model.AccountTypeName.ToLower() &&
                                                                                x.AccountTypeId != model.AccountTypeId
                                                                                );
                if (!sameAccountName)
                {
                    accountTypeDetail.AccountTypeName = model.AccountTypeName;
                    await _uow.AccountTypeRepository.UpdateAsyn(accountTypeDetail);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.NameExist;
                }

               
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }



        public async Task<APIResponse> AddChartAccountDetail(ChartAccountDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var accountCodeExists = await _uow.ChartAccountDetailRepository.FindAsync(x => x.ChartOfAccountCode == model.ChartOfAccountCode);
                if (accountCodeExists == null)
                {
                    if (model.AccountName == null || model.AccountName == "")
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Please enter Account Name.";
                        return response;
                    }
                    ChartAccountDetail obj = _mapper.Map<ChartAccountDetail>(model);
                    obj.CreatedById = model.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    await _uow.ChartAccountDetailRepository.AddAsyn(obj);
                    await _uow.SaveAsync();

                    //var insertedaccountcode = obj.AccountCode;
                    //if (model.AccountLevelId == 1)
                    //{
                    //	obj.ParentID = obj.AccountCode;
                    //	obj.ChartOfAccountCode = obj.AccountCode;
                    //	await _uow.ChartAccountDetailRepository.UpdateAsyn(obj);
                    //}
                    //else
                    //{
                    //	var existrecord = await _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == model.ParentID);
                    //	if (existrecord != null)
                    //	{
                    //		obj.ChartOfAccountCode = Convert.ToInt64(existrecord.ChartOfAccountCode + "" + insertedaccountcode);
                    //		await _uow.ChartAccountDetailRepository.UpdateAsyn(obj);
                    //	}
                    //}

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.AccountAlreadyExistsCode;
                    response.Message = StaticResource.AccountAlreadyExists;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditChartAccountDetail(ChartAccountDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var chartaccountInfo = await _uow.ChartAccountDetailRepository.FindAsync(c => c.AccountCode == model.AccountCode);
                if (chartaccountInfo != null)
                {
                    //chartaccountInfo.ChartOfAccountCode = model.ChartOfAccountCode;
                    //chartaccountInfo.ParentID = model.ParentID;
                    chartaccountInfo.AccountName = model.AccountName;
                    chartaccountInfo.AccountTypeId = model.AccountTypeId;
                    //chartaccountInfo.Show = model.Show;
                    //chartaccountInfo.MDCode = model.MDCode;
                    //chartaccountInfo.DepMethod = model.DepMethod;
                    //chartaccountInfo.DepRate = model.DepRate;
                    chartaccountInfo.ModifiedById = model.ModifiedById;
                    chartaccountInfo.ModifiedDate = model.ModifiedDate;
                    await _uow.ChartAccountDetailRepository.UpdateAsyn(chartaccountInfo);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
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
