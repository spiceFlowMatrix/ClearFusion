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
                var charlist =  await Task.Run(()=>
                   _uow.GetDbContext().ChartAccountDetail.Include(c => c.AccountTypes).Where(a => a.IsDeleted == false).ToList()
                    );
                var chartaccountlist = charlist.Select(blog => new ChartAccountDetailModel
                    {
                        AccountCode = blog.AccountCode,
                        AccountName = blog.AccountName,
                        AccountLevelId = blog.AccountLevelId,
                        AccountTypeName = blog.AccountTypes.AccountTypeName,
                        AccountTypeId = blog.AccountTypes.AccountTypeId,
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
                var accountlevellist = (from a in await _uow.AccountLevelRepository.GetAllAsyn() orderby a.AccountLevelId ascending
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

        public async Task<APIResponse> GetAllAccountType()
        {
            APIResponse response = new APIResponse();
            try
            {
                var accounttypelist = (from a in await _uow.AccountTypeRepository.GetAllAsyn() orderby a.AccountTypeId ascending
                                        select new AccountTypeModel
                                        {
                                            AccountTypeId = a.AccountTypeId,
                                            AccountTypeName = a.AccountTypeName
                                        }).ToList();
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

        public async Task<APIResponse> AddChartAccountDetail(ChartAccountDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
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
                var insertedaccountcode = obj.AccountCode;

                if (model.AccountLevelId == 1)
                {
                    obj.ParentID = obj.AccountCode;
                    obj.ChartOfAccountCode = obj.AccountCode;
                    await _uow.ChartAccountDetailRepository.UpdateAsyn(obj);
                }
                else
                {
                    var existrecord = await _uow.ChartAccountDetailRepository.FindAsync(x => x.AccountCode == model.ParentID);
                    if (existrecord != null)
                    {
                        obj.ChartOfAccountCode = Convert.ToInt64(existrecord.ChartOfAccountCode + "" + insertedaccountcode);
                        await _uow.ChartAccountDetailRepository.UpdateAsyn(obj);
                    }
                }
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

        public async Task<APIResponse> EditChartAccountDetail(ChartAccountDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var chartaccountInfo = await _uow.ChartAccountDetailRepository.FindAsync(c => c.AccountCode == model.AccountCode);
                if (chartaccountInfo != null)
                {
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
