using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class PolicyService : IPolicyService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public PolicyService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public string getPolicyCode(string PolicyId)
        {
            string code = string.Empty;
            if (PolicyId.Length == 1)
                return code = "0000" + PolicyId;
            else if (PolicyId.Length == 2)
                return code = "000" + PolicyId;
            else if (PolicyId.Length == 3)
                return code = "00" + PolicyId;
            else if (PolicyId.Length == 4)
                return code = "0" + PolicyId;
            else
                return code = "" + PolicyId;
        }
        public async Task<APIResponse> AddEditPolicy(PolicyModel model, string UserId)
        {
            long LatestPolicyId = 0;
            var policyCode = string.Empty;
            APIResponse response = new APIResponse();
            try
            {
                if (model.PolicyId == 0 || model.PolicyId == null)
                {
                    var policy = _uow.GetDbContext().PolicyDetails.Where(x => x.PolicyName == model.PolicyName && x.IsDeleted == false).FirstOrDefault();
                    if (policy == null)
                    {
                        var policyDetail = _uow.GetDbContext().PolicyDetails.OrderByDescending(x => x.PolicyId)
                                                                                       .FirstOrDefault();
                        if (policyDetail == null)
                        {
                            LatestPolicyId = 1;
                            policyCode = getPolicyCode(LatestPolicyId.ToString());
                        }
                        else
                        {
                            LatestPolicyId = Convert.ToInt32(policyDetail.PolicyId) + 1;
                            policyCode = getPolicyCode(LatestPolicyId.ToString());
                        }
                        PolicyDetail obj = _mapper.Map<PolicyModel, PolicyDetail>(model);
                        obj.CreatedById = UserId;
                        obj.MediumId = model.MediumId;
                        obj.MediaCategoryId = model.MediaCategoryId;
                        obj.PolicyName = model.PolicyName;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.PolicyCode = policyCode;
                        obj.Description = model.Description;
                        await _uow.PolicyRepository.AddAsyn(obj);
                        await _uow.SaveAsync();
                        response.data.policyDetails = obj;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Policy created successfully";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Policy Name already exists. Please try again with other policy name.";
                    }
                }
                else
                {
                    //ActivityType obj = await _uow.ActivityTypeRepository.FindAsync(x => x.ActivityTypeId == model.ActivityTypeId);
                    //obj.ModifiedById = UserId;
                    //obj.ModifiedDate = DateTime.Now;
                    //_mapper.Map(model, obj);
                    //await _uow.ActivityTypeRepository.UpdateAsyn(obj);
                    //await _uow.SaveAsync();
                    //response.data.activityById = obj;
                    //response.StatusCode = StaticResource.successStatusCode;
                    //response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> DeletePolicy(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var policyInfo = await _uow.PolicyRepository.FindAsync(c => c.PolicyId == model);
                policyInfo.IsDeleted = true;
                policyInfo.ModifiedById = UserId;
                policyInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.PolicyRepository.UpdateAsyn(policyInfo, policyInfo.PolicyId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Policy Deleted Successfully";
                //response.data.jobListTotalCount = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllPolicyList()
        {
            APIResponse response = new APIResponse();
            try
            {
                //int count = await _uow.GetDbContext().PolicyDetails.CountAsync(x => x.IsDeleted == false);
                var policyDetail = await (from j in _uow.GetDbContext().PolicyDetails
                                     join jp in _uow.GetDbContext().LanguageDetail on j.LanguageId equals jp.LanguageId
                                     join me in _uow.GetDbContext().Mediums on j.MediumId equals me.MediumId
                                     join mc in _uow.GetDbContext().MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                     where !j.IsDeleted.Value && !jp.IsDeleted.Value && !me.IsDeleted.Value
                                     && !mc.IsDeleted.Value
                                     select (new PolicyModel
                                     {
                                         PolicyId = j.PolicyId,
                                         PolicyName = j.PolicyName,
                                         PolicyCode = j.PolicyCode,
                                         Description = j.Description,
                                         LanguageId = jp.LanguageId,
                                         LanguageName = jp.LanguageName,
                                         MediumId = me.MediumId,
                                         MediumName = me.MediumName,
                                         MediaCategoryId = mc.MediaCategoryId,
                                         MediaCategoryName = mc.CategoryName
                                     })).Take(10).Skip(0).OrderByDescending(x => x.CreatedDate).ToListAsync();
                
                response.data.policyList = policyDetail;
                //response.data.jobListTotalCount = count;
                response.StatusCode = 200;
                response.Message = "Success";
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
