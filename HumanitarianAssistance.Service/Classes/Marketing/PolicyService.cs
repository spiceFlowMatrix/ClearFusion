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
                        obj.ProducerId = model.ProducerId;
                        obj.MediaCategoryId = model.MediaCategoryId;
                        obj.PolicyName = model.PolicyName;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.PolicyCode = policyCode;
                        obj.Description = model.Description;
                        await _uow.PolicyRepository.AddAsyn(obj);
                        await _uow.SaveAsync();
                        int totalCount = await _uow.GetDbContext().PolicyDetails
                                      .Where(v => v.IsDeleted == false)
                                     .AsNoTracking()
                                     .CountAsync();
                        response.data.policyDetails = obj;
                        response.data.TotalCount = totalCount;
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
                    var existRecord = await _uow.PolicyRepository.FindAsync(x => x.IsDeleted == false && x.PolicyId == model.PolicyId);
                    if (existRecord != null)
                    {
                        _mapper.Map(model, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.Description = model.Description;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        existRecord.LanguageId = model.LanguageId;
                        existRecord.MediaCategoryId = model.MediaCategoryId;
                        existRecord.MediumId = model.MediumId;
                        existRecord.PolicyName = model.PolicyName;
                        existRecord.ProducerId = model.ProducerId;
                        await _uow.PolicyRepository.UpdateAsyn(existRecord);
                        response.data.policyDetails = existRecord;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Policy updated successfully";
                    }
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
                int totalCount = await _uow.GetDbContext().PolicyDetails
                                      .Where(v => v.IsDeleted == false)
                                     .AsNoTracking()
                                     .CountAsync();
                response.data.TotalCount = totalCount;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetPolicyPaginatedList(PolicyPaginationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                int totalCount = await _uow.GetDbContext().PolicyDetails
                                       .Where(v => v.IsDeleted == false)
                                      .AsNoTracking()
                                      .CountAsync();

                var policyList = await _uow.GetDbContext().PolicyDetails
                                       .Where(v => v.IsDeleted == false).Select(x => new PolicyModel
                                       {
                                           PolicyId = x.PolicyId,
                                           PolicyCode = x.PolicyCode,
                                           PolicyName = x.PolicyName,
                                           MediumName = x.Mediums.MediumName,
                                           MediumId = x.MediumId
                                       }).Skip((model.pageSize * model.pageIndex)).Take(model.pageSize).OrderByDescending(x => x.CreatedDate).AsNoTracking()
                                    .ToListAsync();
                response.data.TotalCount = totalCount;
                response.data.policyFilterList = policyList;
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

        public async Task<APIResponse> GetAllPolicyList()
        {
            APIResponse response = new APIResponse();
            try
            {
                int count = await _uow.GetDbContext().PolicyDetails.CountAsync(x => x.IsDeleted == false);
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
                response.data.TotalCount = count;
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

        public async Task<APIResponse> FilterPolicyList(PolicyFilterModel model, string userId)
        {
            string policyIdValue = null;
            string policyNameValue = null;
            string mediumValue = null;

            if (!string.IsNullOrEmpty(model.Value))
            {
                policyIdValue = model.PolicyId ? model.Value.ToLower().Trim() : null;
                policyNameValue = model.PolicyName ? model.Value.ToLower().Trim() : null;
                mediumValue = model.Medium ? model.Value.ToLower().Trim() : null;
            }

            APIResponse response = new APIResponse();
            try
            {
                var policyList = await _uow.GetDbContext().PolicyDetails
                                    .Where(v => v.IsDeleted == false &&
                                          (!string.IsNullOrEmpty(model.Value) ?
                                             (
                                               v.PolicyId.ToString().Trim().ToLower().Contains(policyIdValue) ||
                                               v.PolicyName.Trim().ToLower().Contains(policyNameValue) ||
                                               v.Mediums.MediumName.Trim().ToLower().Contains(mediumValue)
                                              ) : true
                                           )
                                     )
                                    .OrderByDescending(x => x.CreatedDate)
                                    .Select(x => new PolicyModel
                                    {
                                        PolicyId = x.PolicyId,
                                        PolicyName = x.PolicyName,
                                        MediumName = x.Mediums.MediumName,
                                        MediumId = x.MediumId,
                                        PolicyCode = x.PolicyCode
                                    })
                                    .AsNoTracking()
                                    .ToListAsync();
                // response.data.jobListTotalCount = voucherList.Count();
                response.data.PolicyFilteredList = policyList;
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

        public async Task<APIResponse> GetPolicyById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var policyList = await _uow.GetDbContext().PolicyDetails
                                       .Where(v => v.IsDeleted == false && v.PolicyId == model).Select(x => new PolicyModel
                                       {
                                           PolicyId = x.PolicyId,
                                           PolicyCode = x.PolicyCode,
                                           PolicyName = x.PolicyName,
                                           Description = x.Description,
                                           MediumName = x.Mediums.MediumName,
                                           MediumId = x.MediumId,
                                           ProducerId = x.ProducerId,
                                           ProducerName = x.Producers.ProducerName,
                                           MediaCategoryId = x.MediaCategoryId,
                                           MediaCategoryName = x.MediaCategories.CategoryName,
                                           LanguageId = x.LanguageId,
                                           LanguageName = x.Languages.LanguageName
                                       }).AsNoTracking().FirstOrDefaultAsync();
                //response.data.TotalCount = totalCount;
                response.data.policyDetailsById = policyList;
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

        public async Task<APIResponse> AddEditPolicySchedules(ScheduleDetailsModel model, string UserId)
        {
            PolicyScheduleModel mdl = new PolicyScheduleModel();
            mdl.ByDay = model.ByDay;
            mdl.ByMonth = model.ByMonth;
            mdl.ByWeek = model.ByWeek;
            mdl.Description = model.Description;
            mdl.EndTime = TimeSpan.Parse(model.EndTime);
            mdl.StartTime = TimeSpan.Parse(model.StartTime);
            mdl.Title = model.Title;
            mdl.RepeatDays = string.Join(",", model.RepeatDays);
            mdl.Frequency = model.Frequency;
            mdl.PolicyId = model.PolicyId;
            mdl.PolicyScheduleId = mdl.PolicyScheduleId;
            mdl.StartDate = DateTime.Parse(model.StartDate);
            mdl.EndDate = DateTime.Parse(model.EndDate);
            long LatestScheduleId = 0;
            var scheduleCode = string.Empty;
            APIResponse response = new APIResponse();
            try
            {
                if (model.PolicyScheduleId == 0)
                {
                    //var policy = _uow.GetDbContext().PolicyDetails.Where(x => x.PolicyName == model.PolicyName && x.IsDeleted == false).FirstOrDefault();
                    //if (policy == null)
                    {
                        var policyDetail = _uow.GetDbContext().PolicySchedules.OrderByDescending(x => x.PolicyScheduleId)
                                                                                       .FirstOrDefault();
                        if (policyDetail == null)
                        {
                            LatestScheduleId = 1;
                            scheduleCode = getPolicyCode(LatestScheduleId.ToString());
                        }
                        else
                        {
                            LatestScheduleId = Convert.ToInt32(policyDetail.PolicyId) + 1;
                            scheduleCode = getPolicyCode(LatestScheduleId.ToString());
                        }


                        PolicySchedule obj = _mapper.Map<PolicyScheduleModel, PolicySchedule>(mdl);
                        obj.CreatedById = UserId;
                        obj.ScheduleCode = scheduleCode;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.isActive = true;
                        obj.Description = mdl.Description;
                        obj.ByDay = mdl.ByDay;
                        obj.ByMonth = mdl.ByMonth;
                        obj.ByWeek = mdl.ByWeek;
                        obj.EndDate = mdl.EndDate;
                        obj.EndTime = mdl.EndTime;
                        obj.Frequency = mdl.Frequency;
                        obj.PolicyId = mdl.PolicyId;
                        //obj.RepeatDays = mdl.RepeatDays;
                        obj.StartDate = mdl.StartDate;
                        obj.StartTime = mdl.StartTime;
                        obj.Title = mdl.Title;
                        await _uow.PolicyScheduleRepository.AddAsyn(obj);
                        await _uow.SaveAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetPolicyScheduleById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            return response;
        }
    }
}
