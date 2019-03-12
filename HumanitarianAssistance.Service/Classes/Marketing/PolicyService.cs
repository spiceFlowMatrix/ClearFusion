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
                    var schedule = _uow.GetDbContext().PolicySchedules.Where(x => x.Title == model.Title && x.IsDeleted == false).FirstOrDefault();
                    if (schedule == null)
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
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Schedule created successfully.";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Schedule already exists. Please try again with other Title.";
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

        public async Task<APIResponse> GetAllSchedule(string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                int count = await _uow.GetDbContext().PolicySchedules.CountAsync(x => x.IsDeleted == false);
                var policyScheduleList = await (from j in _uow.GetDbContext().PolicySchedules
                                                    //join jp in _uow.GetDbContext().LanguageDetail on j.LanguageId equals jp.LanguageId
                                                    //join me in _uow.GetDbContext().Mediums on j.MediumId equals me.MediumId
                                                    //join mc in _uow.GetDbContext().MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                                where !j.IsDeleted.Value
                                                //&& !jp.IsDeleted.Value && !me.IsDeleted.Value
                                                //&& !mc.IsDeleted.Value
                                                select (new PolicyScheduleModel
                                                {
                                                    PolicyId = j.PolicyId,
                                                    Title = j.Title,
                                                    ScheduleCode = j.ScheduleCode,
                                                    Description = j.Description,
                                                    ByDay = j.ByDay,
                                                    ByMonth = j.ByMonth,
                                                    ByWeek = j.ByWeek,
                                                    EndDate = j.EndDate,
                                                    EndTime = j.EndTime,
                                                    Frequency = j.Frequency,
                                                    isActive = j.isActive,
                                                    PolicyScheduleId = j.PolicyScheduleId,
                                                    RepeatDays = j.RepeatDays,
                                                    StartDate = j.StartDate,
                                                    StartTime = j.StartTime
                                                    //LanguageId = jp.LanguageId,
                                                    //LanguageName = jp.LanguageName,
                                                    //MediumId = me.MediumId,
                                                    //MediumName = me.MediumName,
                                                    //MediaCategoryId = mc.MediaCategoryId,
                                                    //MediaCategoryName = mc.CategoryName
                                                }))
                                          //.Take(10).Skip(0).OrderByDescending(x => x.)
                                          .ToListAsync();

                response.data.policySchedulesByDateList = policyScheduleList;
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
        public async Task<APIResponse> GetScheduleByDate(string model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                int count = await _uow.GetDbContext().PolicySchedules.Where(x=>x.StartDate==DateTime.Parse(model)).CountAsync(x => x.IsDeleted == false);
                var policyScheduleList = await (from j in _uow.GetDbContext().PolicySchedules
                                                    //join jp in _uow.GetDbContext().LanguageDetail on j.LanguageId equals jp.LanguageId
                                                    //join me in _uow.GetDbContext().Mediums on j.MediumId equals me.MediumId
                                                    //join mc in _uow.GetDbContext().MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                                where !j.IsDeleted.Value && j.StartDate == DateTime.Parse(model)
                                                //&& !jp.IsDeleted.Value && !me.IsDeleted.Value
                                                //&& !mc.IsDeleted.Value
                                                select (new PolicyScheduleModel
                                                {
                                                    PolicyId = j.PolicyId,
                                                    Title = j.Title,
                                                    ScheduleCode = j.ScheduleCode,
                                                    Description = j.Description,
                                                    ByDay = j.ByDay,
                                                    ByMonth = j.ByMonth,
                                                    ByWeek = j.ByWeek,
                                                    EndDate = j.EndDate,
                                                    EndTime = j.EndTime,
                                                    Frequency = j.Frequency,
                                                    isActive = j.isActive,
                                                    PolicyScheduleId = j.PolicyScheduleId,
                                                    RepeatDays = j.RepeatDays,
                                                    StartDate = j.StartDate,
                                                    StartTime = j.StartTime
                                                })).ToListAsync();
                response.data.TotalCount = count;
                response.StatusCode = 200;
                response.Message = "Success";
                response.data.policySchedulesByDateList = policyScheduleList;
            }
            catch(Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> AddEditPolicyTimeSchedule(PolicyTimeScheduleModel model, string UserId)
        {
            long LatestId = 0;
            var Code = string.Empty;
            APIResponse response = new APIResponse();
            try
            {
                var ifExists = await _uow.GetDbContext().PolicyTimeSchedules.Where(x => x.PolicyId == model.PolicyId && x.StartTime == model.StartTime && x.EndTime == model.EndTime && x.IsDeleted == false).FirstOrDefaultAsync();
                if (ifExists != null)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Time slot for the policy already exists";
                }
                else
                {
                    if (model.Id == 0)
                    {
                        var detail = _uow.GetDbContext().PolicyTimeSchedules.OrderByDescending(x => x.Id)
                                                                                       .FirstOrDefault();
                        if (detail == null)
                        {
                            LatestId = 1;
                            Code = getPolicyCode(LatestId.ToString());
                        }
                        else
                        {
                            LatestId = Convert.ToInt32(detail.Id) + 1;
                            Code = getPolicyCode(LatestId.ToString());
                        }

                        PolicyTimeSchedule obj = _mapper.Map<PolicyTimeScheduleModel, PolicyTimeSchedule>(model);
                        obj.StartTime = model.StartTime;
                        obj.EndTime = model.EndTime;
                        obj.TimeScheduleCode = Code;
                        obj.PolicyId = model.PolicyId;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        await _uow.PolicyTimeScheduleRepository.AddAsyn(obj);
                        await _uow.SaveAsync();
                        response.StatusCode = 200;
                        response.data.policyTimeScheduleDetails = obj;
                        response.Message = "Time slot created successfully";

                    }
                    else
                    {
                        var existRecord = await _uow.PolicyTimeScheduleRepository.FindAsync(x => x.IsDeleted == false && x.Id == model.Id);
                        if (existRecord != null)
                        {
                            _mapper.Map(model, existRecord);
                            existRecord.IsDeleted = false;
                            existRecord.StartTime = model.StartTime;
                            existRecord.ModifiedById = UserId;
                            existRecord.ModifiedDate = DateTime.Now;
                            existRecord.EndTime = model.EndTime;
                            await _uow.PolicyTimeScheduleRepository.UpdateAsyn(existRecord);
                            response.data.policyTimeScheduleDetails = existRecord;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Time slot updated successfully";
                        }
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

        public async Task<APIResponse> GetPolicyTimeScheduleList(int id, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                int count = await _uow.GetDbContext().PolicyTimeSchedules.CountAsync(x => x.IsDeleted == false);
                var policyScheduleList = await (from j in _uow.GetDbContext().PolicyTimeSchedules
                                                where !j.IsDeleted.Value && j.PolicyId == id
                                                select (new PolicyTimeScheduleModel
                                                {
                                                    PolicyId = j.PolicyId,
                                                    StartTime = j.StartTime,
                                                    EndTime = j.EndTime,
                                                    Id = j.Id
                                                }))
                                          .ToListAsync();

                response.data.policySchedulesByTimeList = policyScheduleList;
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

        public async Task<APIResponse> DeletePolicyTimeSchedule(int id, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var policyInfo = await _uow.PolicyTimeScheduleRepository.FindAsync(c => c.Id == id);
                policyInfo.IsDeleted = true;
                policyInfo.ModifiedById = UserId;
                policyInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.PolicyTimeScheduleRepository.UpdateAsyn(policyInfo, policyInfo.Id);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Policy Schedule Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetPolicyTimeScheduleById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var policyList = await _uow.GetDbContext().PolicyTimeSchedules
                                       .Where(v => v.IsDeleted == false && v.Id == model).Select(x => new PolicyTimeScheduleModel
                                       {
                                           Id = x.Id,
                                           PolicyId = x.PolicyId,
                                           StartTime = x.StartTime,
                                           EndTime = x.EndTime
                                       }).AsNoTracking().FirstOrDefaultAsync();
                //response.data.TotalCount = totalCount;
                response.data.policyTimeDetailsById = policyList;
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

        public async Task<APIResponse> AddPolicyRepeatDays(PolicyTimeModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.Id == 0)
                {
                    var detail = _uow.GetDbContext().PolicyDaySchedules.OrderByDescending(x => x.Id).FirstOrDefault();
                    PolicyDaySchedule obj = _mapper.Map<PolicyTimeModel, PolicyDaySchedule>(model);
                    if (model.RepeatDays != null && model.RepeatDays.Count>0)
                    {
                        foreach (var items in model.RepeatDays)
                        {
                            if (items.Value == "MON")
                            {
                                obj.Monday = items.status;
                            }
                            if (items.Value == "TUE")
                            {
                                obj.Tuesday = items.status; 
                            }
                            if (items.Value == "WED")
                            {
                                obj.Wednesday = items.status;
                            }
                            if (items.Value == "THU")
                            {
                                obj.Thursday = items.status;
                            }
                            if (items.Value == "FRI")
                            {
                                obj.Friday = items.status;
                            }
                            if (items.Value == "SAT")
                            {
                                obj.Saturday = items.status;
                            }
                            if (items.Value == "SUN")
                            {
                                obj.Sunday = items.status;
                            }
                        }
                    }
                    obj.PolicyId = model.PolicyId;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsDeleted = false;
                    await _uow.PolicyDayScheduleRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = 200;
                    response.data.policyDayScheduleDetails = obj;
                    response.Message = "Repeat Days updated successfully";
                }
                else
                {
                    var existRecord = await _uow.PolicyDayScheduleRepository.FindAsync(x => x.IsDeleted == false && x.Id == model.Id);
                    if (existRecord != null)
                    {
                        _mapper.Map(model, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        if (model.RepeatDays != null && model.RepeatDays.Count>0)
                        {
                            foreach (var items in model.RepeatDays)
                            {
                                if (items.Value == "MON")
                                {
                                    existRecord.Monday = items.status;
                                }
                                if (items.Value == "TUE")
                                {
                                    existRecord.Tuesday = items.status;
                                }
                                if (items.Value == "WED")
                                {
                                    existRecord.Wednesday = items.status;
                                }
                                if (items.Value == "THU")
                                {
                                    existRecord.Thursday = items.status;
                                }
                                if (items.Value == "FRI")
                                {
                                    existRecord.Friday = items.status;
                                }
                                if (items.Value == "SAT")
                                {
                                    existRecord.Saturday = items.status;
                                }
                                if (items.Value == "SUN")
                                {
                                    existRecord.Sunday = items.status;
                                }
                            }
                        }
                        await _uow.PolicyDayScheduleRepository.UpdateAsyn(existRecord);
                        response.data.policyDayScheduleDetails = existRecord;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Repeat Days updated successfully";
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
        public async Task<APIResponse> GetDayScheduleByPolicyId(int id, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var policyList = await _uow.GetDbContext().PolicyDaySchedules
                                       .Where(v => v.IsDeleted == false && v.PolicyId == id).Select(x => new PolicyTimeScheduleModel
                                       {
                                           Id = x.Id,
                                           PolicyId = x.PolicyId,
                                           Monday = x.Monday,
                                           Tuesday = x.Tuesday,
                                           Wednesday = x.Wednesday,
                                           Thursday = x.Thursday,
                                           Friday = x.Friday,
                                           Saturday = x.Saturday,
                                           Sunday = x.Sunday
                                       }).AsNoTracking().FirstOrDefaultAsync();
                //response.data.TotalCount = totalCount;
                List<string> repeatDays = new List<string>();
                if(policyList != null)
                {
                    if (policyList.Monday == true)
                    {
                        repeatDays.Add("MON");
                    }
                    if (policyList.Tuesday == true)
                    {
                        repeatDays.Add("TUE");
                    }
                    if (policyList.Wednesday == true)
                    {
                        repeatDays.Add("WED");
                    }
                    if (policyList.Thursday == true)
                    {
                        repeatDays.Add("THU");
                    }
                    if (policyList.Friday == true)
                    {
                        repeatDays.Add("FRI");
                    }
                    if (policyList.Saturday == true)
                    {
                        repeatDays.Add("SAT");
                    }
                    if (policyList.Sunday == true)
                    {
                        repeatDays.Add("SUN");
                    }
                    policyList.repeatDays = repeatDays;
                }
                response.data.policyTimeDetailsById = policyList;
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
        public async Task<APIResponse> AddEditPolicyOrderSchedule(PolicyOrderScheduleModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().PolicyOrderSchedules.Where(x => x.IsDeleted == false && x.PolicyId == model.PolicyId && ((x.StartDate <= model.StartDate && x.EndDate >= model.EndDate) || (x.StartDate <= model.StartDate && x.EndDate >= model.StartDate) || (x.StartDate <= model.EndDate && x.EndDate >= model.EndDate))).ToListAsync();
                try
                {
                    if (list.Count != 0)
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Order schedule could not be requested as the dates are already taken.";
                        //foreach (var items in list)
                        //{
                        //    if (model.StartDate >= items.StartDate && model.StartDate <= items.EndDate && model.EndDate >= items.StartDate && model.EndDate <= items.EndDate)
                        //    {
                        //      flag=true;

                        //    }

                        //}

                    }

                    else
                    {
                        if (model.Id == 0)
                        {
                            PolicyOrderSchedule obj = _mapper.Map<PolicyOrderScheduleModel, PolicyOrderSchedule>(model);
                            obj.PolicyId = model.PolicyId;
                            obj.CreatedDate = DateTime.UtcNow;
                            obj.StartDate = model.StartDate;
                            obj.EndDate = model.EndDate;
                            obj.IsDeleted = false;
                            obj.RequestSchedule = true;
                            await _uow.PolicyOrderScheduleRepository.AddAsyn(obj);
                            await _uow.SaveAsync();
                            response.StatusCode = 200;
                            //response.data.policyTimeScheduleDetails = obj;
                            response.Message = "Schedule order requested successfully";
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
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
