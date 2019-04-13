using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.ViewModels.Models.Project;
using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class SchedulerService : ISchedulerService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public SchedulerService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public string getScheduleCode(string ScheduleId)
        {
            string code = string.Empty;
            if (ScheduleId.Length == 1)
                return code = "0000" + ScheduleId;
            else if (ScheduleId.Length == 2)
                return code = "000" + ScheduleId;
            else if (ScheduleId.Length == 3)
                return code = "00" + ScheduleId;
            else if (ScheduleId.Length == 4)
                return code = "0" + ScheduleId;
            else
                return code = "" + ScheduleId;
        }

        public async Task<List<JobDetailsModel>> GetJobsList()
        {
            var JobList = await (from j in _uow.GetDbContext().JobDetails
                                 join jp in _uow.GetDbContext().JobPriceDetails on j.JobId equals jp.JobId
                                 where !j.IsDeleted.Value && !jp.IsDeleted.Value
                                 select (new JobDetailsModel
                                 {
                                     JobId = j.JobId,
                                     JobCode = j.JobCode,
                                     JobName = j.JobName,
                                     Description = j.Description,
                                     JobPhaseId = j.JobPhaseId,
                                     StartDate = j.StartDate,
                                     EndDate = j.EndDate,
                                     IsActive = j.IsActive,
                                     IsApproved = j.IsApproved,
                                     UnitRate = jp.UnitRate,
                                     Units = jp.Units,
                                     FinalRate = jp.FinalRate,
                                     FinalPrice = jp.FinalPrice,
                                     TotalPrice = jp.TotalPrice,
                                     CreatedDate = j.CreatedDate,
                                     IsInvoiceApproved = jp.IsInvoiceApproved
                                 })).ToListAsync();
            return JobList;
        }

        public async Task<List<ScheduleDetailModel>> GetPolicyScheduleList()
        {
            var policyScheduleList = await (from j in _uow.GetDbContext().PolicyTimeSchedules
                                            join mc in _uow.GetDbContext().PolicyDetails on j.PolicyId equals mc.PolicyId
                                            join pd in _uow.GetDbContext().PolicyDaySchedules on j.PolicyId equals pd.PolicyId
                                            join po in _uow.GetDbContext().PolicyOrderSchedules on j.PolicyId equals po.PolicyId
                                            where !j.IsDeleted.Value && !mc.IsDeleted.Value
                                            && !pd.IsDeleted.Value && !po.IsDeleted.Value
                                             && po.StartDate<=DateTime.UtcNow.Date && DateTime.UtcNow.Date<=po.EndDate
                                            //&& !jp.IsDeleted.Value && !me.IsDeleted.Value
                                            //&& !mc.IsDeleted.Value
                                            select (new ScheduleDetailModel
                                            {
                                                PolicyId = mc.PolicyId,
                                                PolicyDayId = pd.Id,
                                                PolicyOrderId = po.Id,
                                                PolicyTimeId = j.Id,
                                                Name = mc.PolicyName,
                                                StartTime = j.StartTime.ToString(@"hh\:mm"),
                                                EndTime = j.EndTime.ToString(@"hh\:mm"),
                                                StartDate = po.StartDate,
                                                EndDate = po.EndDate,
                                                Sunday = pd.Sunday,
                                                Monday = pd.Monday,
                                                Tuesday = pd.Tuesday,
                                                Wednesday = pd.Wednesday,
                                                Thursday = pd.Thursday,
                                                Friday = pd.Friday,
                                                Saturday = pd.Saturday
                                            })).ToListAsync();
            return policyScheduleList;
        }

        public async Task<List<PolicyModel>> PolicyList()
        {
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
                                      })).OrderByDescending(x => x.CreatedDate).ToListAsync();
            return policyDetail;
        }

        public async Task<List<ProjectDetailNewModel>> GetProjectList()
        {
            var ProjectList = await _uow.GetDbContext().ProjectDetail
                                         .Where(x => x.IsDeleted == false && x.ProjectName != "")
                                         .OrderByDescending(x => x.ProjectId).Select(x => new ProjectDetailNewModel
                                         {
                                             ProjectId = x.ProjectId,
                                             ProjectCode = x.ProjectCode,
                                             ProjectName = x.ProjectName,
                                             ProjectDescription = x.ProjectDescription
                                         }).ToListAsync();
            return ProjectList;
        }
        public async Task<APIResponse> GetAllPolicyScheduleList()
        {
            APIResponse response = new APIResponse();
            try
            {
                List<JobDetailsModel> JobList = await GetJobsList();
                List<ScheduleDetailModel> policyScheduleList = await GetPolicyScheduleList();
                List<PolicyModel> policyDetail = await PolicyList();
                List<ProjectDetailNewModel> ProjectList = await GetProjectList();
                response.data.ProjectDetailModel = ProjectList;
                response.data.policyList = policyDetail;
                response.data.JobDetailsModel = JobList;
                response.data.scheduleDetailsList = policyScheduleList;
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

        public async Task<APIResponse> GetScheduleDetailsById(int model)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != 0)
                {
                    var scheduleById = await _uow.GetDbContext().ScheduleDetails
                                          .Include(p => p.ProjectDetail)
                                          .Include(e => e.PolicyDetails)
                                          .Include(o => o.JobDetails)
                                          .Where(v => v.IsDeleted == false && v.JobDetails.JobId == v.JobId)
                                          .Where(v => v.ProjectDetail.ProjectId == v.ProjectId)
                                          .Where(v => v.PolicyDetails.PolicyId == v.PolicyId)
                                          .Where(v => v.ScheduleId == model)
                                          .FirstOrDefaultAsync();
                    SchedulerModel ScheduleList = new SchedulerModel
                    {
                        PolicyId = scheduleById.PolicyId,
                        ScheduleType = scheduleById.ScheduleType,
                        MediumId = scheduleById.MediumId,
                        ChannelId = scheduleById.ChannelId,
                        Name = scheduleById.PolicyDetails != null ? scheduleById.PolicyDetails.PolicyName : scheduleById.ProjectDetail != null ? scheduleById.ProjectDetail.ProjectName : scheduleById.JobDetails != null ? scheduleById.JobDetails.JobName : null,
                        StartTime = scheduleById.StartTime.ToString(@"hh\:mm"),
                        EndTime = scheduleById.EndTime.ToString(@"hh\:mm"),
                        StartDate = scheduleById.StartDate.ToString(),
                        EndDate = scheduleById.EndDate.ToString(),
                        ProjectId = scheduleById.ProjectId,
                        JobId = scheduleById.JobId,
                        ScheduleCode = scheduleById.ScheduleCode,
                        ScheduleId = scheduleById.ScheduleId,
                        ScheduleName = scheduleById.ScheduleName,
                        Monday = scheduleById.Monday,
                        Tuesday = scheduleById.Tuesday,
                        Wednesday = scheduleById.Wednesday,
                        Thursday = scheduleById.Thursday,
                        Friday = scheduleById.Friday,
                        Saturday = scheduleById.Saturday,
                        Sunday = scheduleById.Sunday
                    };

                    if (scheduleById != null)
                    {
                        List<string> repeatDays = new List<string>();
                        if (ScheduleList != null)
                        {
                            if (ScheduleList.Monday == true)
                            {
                                repeatDays.Add("MON");
                            }
                            if (ScheduleList.Tuesday == true)
                            {
                                repeatDays.Add("TUE");
                            }
                            if (ScheduleList.Wednesday == true)
                            {
                                repeatDays.Add("WED");
                            }
                            if (ScheduleList.Thursday == true)
                            {
                                repeatDays.Add("THU");
                            }
                            if (ScheduleList.Friday == true)
                            {
                                repeatDays.Add("FRI");
                            }
                            if (ScheduleList.Saturday == true)
                            {
                                repeatDays.Add("SAT");
                            }
                            if (ScheduleList.Sunday == true)
                            {
                                repeatDays.Add("SUN");
                            }
                        }
                        response.data.RepeatDays = repeatDays;
                        response.data.scheduleDetailsModel = ScheduleList;
                        response.StatusCode = 200;
                        response.Message = "Success";
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

        public bool CheckRepeatDays(ScheduleDetails data, SchedulerModel model)
        {
            bool save = false;
            bool status = false;
            DateTime startDate = DateTime.Parse(DateTime.Parse(model.StartDate).ToShortDateString());
            DateTime dataStartDate = DateTime.Parse(data.StartDate.ToShortDateString());
            DateTime endDate = DateTime.Parse(DateTime.Parse(model.EndDate).ToShortDateString());
            DateTime dataEndDate = DateTime.Parse(data.EndDate.ToShortDateString());
            if (startDate >= dataStartDate)
            {
                if (endDate >= dataStartDate)
                {
                    if (model.RepeatDays != null && model.RepeatDays.Count > 0)
                    {
                        foreach (var items in model.RepeatDays)
                        {
                            if (items.Value == "MON")
                            {
                                if (data.Monday)
                                {
                                    status = true;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else if (items.Value == "TUE")
                            {
                                if (data.Tuesday)
                                {
                                    status = true;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else if (items.Value == "WED")
                            {
                                if (data.Wednesday)
                                {
                                    status = true;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else if (items.Value == "THU")
                            {
                                if (data.Thursday)
                                {
                                    status = true;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else if (items.Value == "FRI")
                            {
                                if (data.Friday)
                                {
                                    status = true;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else if (items.Value == "SAT")
                            {
                                if (data.Saturday)
                                {
                                    status = true;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                            else if (items.Value == "SUN")
                            {
                                if (data.Sunday)
                                {
                                    status = true;
                                }
                                else
                                {
                                    status = false;
                                }
                            }
                        }
                    }
                    if (status == true)
                    {
                        save = false;
                    }
                    else
                    {
                        save = true;
                    }
                }
            }
            return save;
        }

        public async Task<APIResponse> AddEditSchedule(SchedulerModel model, string userId)
        {
            long LatestScheduleId = 0;
            var scheduleCode = string.Empty;
            if(model.ProjectId == 0)
            {
                model.ProjectId = null;
            }
            if (model.PolicyId == 0)
            {
                model.PolicyId = null;
            }
            if (model.JobId == 0)
            {
                model.JobId = null;
            }
            APIResponse response = new APIResponse();
            try
            {
                var record = await _uow.ScheduleDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ChannelId == model.ChannelId && x.StartTime.ToString(@"hh\:mm") == model.StartTime && x.EndTime.ToString(@"hh\:mm") == model.EndTime);
                if (record != null)
                {
                    CheckRepeatDays(record, model);
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Schedule already exists for the time";
                }
                else
                {
                    if (model.ScheduleId == 0 || model.ScheduleId == null)
                    {
                        //var schedule = _uow.GetDbContext().ScheduleDetails.Where(x => x.ScheduleName == model.ScheduleName && x.IsDeleted == false).FirstOrDefault();

                        var scheduleDetail = _uow.GetDbContext().ScheduleDetails.OrderByDescending(x => x.ScheduleId)
                                                                                       .FirstOrDefault();
                        if (scheduleDetail == null)
                        {
                            LatestScheduleId = 1;
                            scheduleCode = getScheduleCode(LatestScheduleId.ToString());
                        }
                        else
                        {
                            LatestScheduleId = Convert.ToInt32(scheduleDetail.ScheduleId) + 1;
                            scheduleCode = getScheduleCode(LatestScheduleId.ToString());
                        }
                        ScheduleDetails obj = _mapper.Map<SchedulerModel, ScheduleDetails>(model);
                        obj.CreatedById = userId;
                        if(model.ProjectId != null)
                        {
                            obj.ProjectId = model.ProjectId ?? null;
                            obj.ScheduleType = "Project";
                        }
                        if (model.PolicyId != null)
                        {
                            obj.PolicyId = model.PolicyId ?? null;
                            obj.ScheduleType = "Policy";
                        }
                        if (model.JobId != null)
                        {
                            obj.JobId = model.JobId ?? null;
                            obj.ScheduleType = "Job";
                        }


                        obj.StartTime = TimeSpan.Parse(model.StartTime);
                        obj.EndTime = TimeSpan.Parse(model.EndTime);
                        obj.ScheduleName = model.ScheduleName;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.ScheduleCode = scheduleCode;
                        obj.MediumId = model.MediumId;
                        obj.ChannelId = model.ChannelId;
                        obj.Description = model.Description;
                        if (model.RepeatDays != null && model.RepeatDays.Count > 0)
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
                        await _uow.ScheduleDetailsRepository.AddAsyn(obj);
                        APIResponse response1 = await GetScheduleDetailsById(Convert.ToInt32(obj.ScheduleId));
                        response.data.scheduleDetailsModel = response1.data.scheduleDetailsModel;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Schedule created successfully";
                    }
                    else
                    {
                        var existRecord = await _uow.ScheduleDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ScheduleId == model.ScheduleId);
                        if (existRecord != null)
                        {

                            bool status = false;
                            var dateTime = DateTime.Now;
                            var today = dateTime.DayOfWeek.ToString();
                            var currentTime = dateTime.Hour + ":" + dateTime.Minute;
                            if (TimeSpan.Parse(existRecord.StartTime.ToString(@"hh\:mm")) < TimeSpan.Parse(currentTime))
                            {
                                if (TimeSpan.Parse(currentTime) < TimeSpan.Parse(existRecord.EndTime.ToString(@"hh\:mm")))
                                {
                                    if (existRecord.Monday == true)
                                    {
                                        if (today == "Monday")
                                        {
                                            status = true;
                                        }
                                        else
                                        {
                                            status = false;
                                        }
                                    }
                                    if (existRecord.Tuesday == true)
                                    {
                                        if (today == "Tuesday")
                                        {
                                            status = true;
                                        }
                                        else
                                        {
                                            status = false;
                                        }
                                    }
                                    if (existRecord.Wednesday == true)
                                    {
                                        if (today == "Wednesday")
                                        {
                                            status = true;
                                        }
                                        else
                                        {
                                            status = false;
                                        }
                                    }
                                    if (existRecord.Thursday == true)
                                    {
                                        if (today == "Thursday")
                                        {
                                            status = true;
                                        }
                                        else
                                        {
                                            status = false;
                                        }
                                    }
                                    if (existRecord.Friday == true)
                                    {
                                        if (today == "Friday")
                                        {
                                            status = true;
                                        }
                                        else
                                        {
                                            status = false;
                                        }
                                    }
                                    if (existRecord.Saturday == true)
                                    {
                                        if (today == "Saturday")
                                        {
                                            status = true;
                                        }
                                        else
                                        {
                                            status = false;
                                        }
                                    }
                                    if (existRecord.Sunday == true)
                                    {
                                        if (today == "Sunday")
                                        {
                                            status = true;
                                        }
                                        else
                                        {
                                            status = false;
                                        }
                                    }
                                }
                            }
                            if (status == true)
                            {
                                response.data.schedulerDetails = existRecord;
                                response.StatusCode = StaticResource.failStatusCode;
                                response.Message = "Schedule is running, can not be updated.";
                            }
                            else
                            {
                                _mapper.Map(model, existRecord);
                                if (model.RepeatDays != null && model.RepeatDays.Count > 0)
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
                                if (model.ProjectId != null)
                                {
                                    existRecord.ProjectId = model.ProjectId ?? null;
                                    existRecord.ScheduleType = "Project";
                                }
                                if (model.PolicyId != null)
                                {
                                    existRecord.PolicyId = model.PolicyId ?? null;
                                    existRecord.ScheduleType = "Policy";
                                }
                                if (model.JobId != null)
                                {
                                    existRecord.JobId = model.JobId ?? null;
                                    existRecord.ScheduleType = "Job";
                                }
                                existRecord.ScheduleCode = existRecord.ScheduleCode;
                                existRecord.IsDeleted = false;
                                existRecord.ModifiedById = userId;
                                existRecord.ModifiedDate = DateTime.Now;
                                existRecord.MediumId = model.MediumId;
                                existRecord.ChannelId = model.ChannelId;
                                existRecord.StartTime = TimeSpan.Parse(model.StartTime);
                                existRecord.EndTime = TimeSpan.Parse(model.EndTime);
                                //existRecord.ScheduleName = model.ScheduleName;
                                existRecord.CreatedDate = DateTime.Now;
                                //existRecord.Description = model.Description;

                                await _uow.ScheduleDetailsRepository.UpdateAsyn(existRecord);
                                response.data.schedulerDetails = existRecord;
                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Schedule Updated";
                            }

                        }
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

        public async Task<APIResponse> GetAllScheduleList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var dateTime = DateTime.Now;
                string today = dateTime.DayOfWeek.ToString();
                var activityList = await _uow.GetDbContext().ScheduleDetails
                                          .Include(p => p.ProjectDetail)
                                          .Include(e => e.PolicyDetails)
                                          .Include(o => o.JobDetails)
                                          .Where(v => v.IsDeleted == false && (v.JobDetails.JobId == v.JobId || v.ProjectDetail.ProjectId == v.ProjectId || v.PolicyDetails.PolicyId == v.PolicyId) && v.StartDate <= DateTime.UtcNow && DateTime.UtcNow.Date <= v.EndDate)
                                          .ToListAsync();
                var ScheduleList = activityList.Select(b => new SchedulerModel
                {
                    PolicyId = b.PolicyId,
                    Name = b.PolicyDetails != null ? b.PolicyDetails.PolicyName : b.ProjectDetail != null ? b.ProjectDetail.ProjectName : b.JobDetails != null? b.JobDetails.JobName : null,
                    StartTime = b.StartTime.ToString(@"hh\:mm"),
                    EndTime = b.EndTime.ToString(@"hh\:mm"),
                    StartDate = b.StartDate.ToShortDateString(),
                    EndDate = b.EndDate.ToShortDateString(),
                    ProjectId = b.ProjectId,
                    JobId = b.JobId,
                    ChannelId = b.ChannelId,
                    MediumId = b.MediumId,
                    ScheduleType = b.ScheduleType,
                    ScheduleId = b.ScheduleId,
                    Monday = b.Monday,
                    Tuesday = b.Tuesday,
                    Wednesday = b.Wednesday,
                    Thursday = b.Thursday,
                    Friday = b.Friday,
                    Saturday = b.Saturday,
                    Sunday = b.Sunday
                }).ToList();
                ScheduleList = FilterListByDays(ScheduleList);
                //var ScheduleList = await (from j in _uow.GetDbContext().ScheduleDetails
                //                                join mc in _uow.GetDbContext().PolicyDetails on j.PolicyId equals mc.PolicyId
                //                                join pd in _uow.GetDbContext().JobDetails on j.JobId equals pd.JobId
                //                                join po in _uow.GetDbContext().ProjectDetails on j.ProjectId equals po.ProjectId
                //                                where !j.IsDeleted.Value
                //                               select (new SchedulerModel
                //                                {
                //                                    PolicyId = j.PolicyId,
                //                                    StartTime = j.StartTime.ToString(@"hh\:mm"),
                //                                    EndTime = j.EndTime.ToString(@"hh\:mm"),
                //                                    StartDate = j.StartDate.ToShortDateString(),
                //                                    EndDate = j.EndDate.ToShortDateString(),
                //                                    ProjectId = j.ProjectId,
                //                                    JobId = j.JobId,
                //                                    ChannelId = j.ChannelId,
                //                                    MediumId = j.MediumId,
                //                                    ScheduleType = j.ScheduleType,
                //                                    ScheduleId = j.ScheduleId
                //                                })).ToListAsync();
                response.data.SchedulerList = ScheduleList;
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
        public async Task<APIResponse> DeleteSchedule(int id, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var scheduleInfo = await _uow.ScheduleDetailsRepository.FindAsync(c => c.ScheduleId == id);
                scheduleInfo.IsDeleted = true;
                scheduleInfo.ModifiedById = userId;
                scheduleInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.ScheduleDetailsRepository.UpdateAsyn(scheduleInfo, scheduleInfo.ScheduleId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Scehdule Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        public async Task<APIResponse> FilterScheduleList(FilterSchedulerModel model)
        {
            var dateTime = DateTime.Now;
            var currentTime = dateTime.Hour + ":" + dateTime.Minute;
            string today = dateTime.DayOfWeek.ToString();
            APIResponse response = new APIResponse();
            try
            {
                var activityList = await _uow.GetDbContext().ScheduleDetails
                                      .Include(p => p.Mediums)
                                      .Include(p => p.ProjectDetail)
                                      .Include(e => e.PolicyDetails)
                                      .Include(o => o.JobDetails)
                                      .Where(v => v.IsDeleted == false)
                                      .Where(v => v.JobDetails != null ? v.JobDetails.JobId == v.JobId: true)
                                      .Where(v => v.ProjectDetail != null ? v.ProjectDetail.ProjectId == v.ProjectId: true)
                                      .Where(v => v.PolicyDetails != null ? v.PolicyDetails.PolicyId == v.PolicyId : true)
                                      .Where(v => v.StartDate <= DateTime.UtcNow && DateTime.UtcNow.Date <= v.EndDate)
                                      .ToListAsync();
                var ScheduleList = activityList.Select(b => new SchedulerModel
                {
                    PolicyId = b.PolicyId,
                    Name = b.PolicyDetails != null ? b.PolicyDetails.PolicyName : b.ProjectDetail != null ? b.ProjectDetail.ProjectName : b.JobDetails != null ? b.JobDetails.JobName : null,
                    StartTime = b.StartTime.ToString(@"hh\:mm"),
                    EndTime = b.EndTime.ToString(@"hh\:mm"),
                    StartDate = b.StartDate.ToShortDateString(),
                    EndDate = b.EndDate.ToShortDateString(),
                    ProjectId = b.ProjectId,
                    JobId = b.JobId,
                    MediumId = b.MediumId,
                    ChannelId = b.ChannelId,
                    ScheduleType = b.ScheduleType,
                    ScheduleId = b.ScheduleId,
                    Monday = b.Monday,
                    Tuesday = b.Tuesday,
                    Wednesday = b.Wednesday,
                    Thursday = b.Thursday,
                    Friday = b.Friday,
                    Saturday = b.Saturday,
                    Sunday = b.Sunday
                }).ToList();
                if (ScheduleList != null)
                {
                    ScheduleList = FilterListByDays(ScheduleList);


                    if (model.StartDate != null)
                    {
                        ScheduleList = ScheduleList.Where(x => x.MediumId == model.MediumId && x.ChannelId == model.ChannelId && x.StartDate == model.StartDate.ToString()).ToList();
                    }
                    else
                    {
                        ScheduleList = ScheduleList.Where(x => x.MediumId == model.MediumId && x.ChannelId == model.ChannelId).ToList();
                    }

                }
                response.data.SchedulerList = ScheduleList;
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
        public List<SchedulerModel> FilterListByDays(List<SchedulerModel> model)
        {
            var dateTime = DateTime.Now;
            var currentTime = dateTime.Hour + ":" + dateTime.Minute;
            string today = dateTime.DayOfWeek.ToString();
            switch (today)
            {
                case Weekdays.Monday:
                    model = model.Where(p => p.Monday == true).ToList();
                    break;
                case Weekdays.Tuesday:
                    model = model.Where(p => p.Tuesday == true).ToList();
                    break;
                case Weekdays.Wednesday:
                    model = model.Where(p => p.Wednesday == true).ToList();
                    break;
                case Weekdays.Thursday:
                    model = model.Where(p => p.Thursday == true).ToList();
                    break;
                case Weekdays.Friday:
                    model = model.Where(p => p.Friday == true).ToList();
                    break;
                case Weekdays.Saturday:
                    model = model.Where(p => p.Saturday == true).ToList();
                    break;
                case Weekdays.Sunday:
                    model = model.Where(p => p.Sunday == true).ToList();
                    break;
                default:
                    break;
            }
            return model;
        }
        public async Task<APIResponse> AddPlayoutMinutes(PlayoutMinutesModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                PlayoutMinutes obj = _mapper.Map<PlayoutMinutesModel, PlayoutMinutes>(model);
                obj.TotalMinutes = model.TotalMinutes;
                obj.DroppedMinutes = model.DroppedMinutes;
                obj.PolicyId = model.PolicyId;
                obj.CreatedById = userId;
                obj.CreatedById = userId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.CreatedDate = DateTime.Now;
                await _uow.PlayoutMinutesRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

    }
}
