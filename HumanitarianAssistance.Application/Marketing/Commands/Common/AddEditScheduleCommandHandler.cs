using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditScheduleCommandHandler : IRequestHandler<AddEditScheduleCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditScheduleCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEditScheduleCommand request, CancellationToken cancellationToken)
        {
            var scheduleCode = string.Empty;
            request.ProjectId = request.ProjectId == 0 ? null : request.ProjectId;
            request.PolicyId = request.PolicyId == 0 ? null : request.PolicyId;
            request.JobId = request.JobId == 0 ? null : request.JobId;
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.ScheduleId!=0)
                {
                    var existRecord = await _dbContext.ScheduleDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ScheduleId == request.ScheduleId);
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
                            _mapper.Map(request, existRecord);
                            if (request.RepeatDays != null && request.RepeatDays.Count > 0)
                            {
                                foreach (var items in request.RepeatDays)
                                {
                                    switch (items.Value)
                                    {
                                        case "MON":
                                            existRecord.Monday = items.status;
                                            break;
                                        case "TUE":
                                            existRecord.Tuesday = items.status;
                                            break;
                                        case "WED":
                                            existRecord.Wednesday = items.status;
                                            break;
                                        case "THU":
                                            existRecord.Thursday = items.status;
                                            break;
                                        case "FRI":
                                            existRecord.Friday = items.status;
                                            break;
                                        case "SAT":
                                            existRecord.Saturday = items.status;
                                            break;
                                        case "SUN":
                                            existRecord.Sunday = items.status;
                                            break;
                                    }
                                }
                            }
                            if (request.ProjectId != null)
                            {
                                existRecord.ProjectId = request.ProjectId ?? null;
                                existRecord.ScheduleType = "Project";
                            }
                            if (request.PolicyId != null)
                            {
                                existRecord.PolicyId = request.PolicyId ?? null;
                                existRecord.ScheduleType = "Policy";
                            }
                            if (request.JobId != null)
                            {
                                existRecord.JobId = request.JobId ?? null;
                                existRecord.ScheduleType = "Job";
                            }
                            existRecord.ScheduleCode = existRecord.ScheduleCode;
                            existRecord.IsDeleted = false;
                            existRecord.ModifiedById = request.ModifiedById;
                            existRecord.ModifiedDate = DateTime.Now;
                            existRecord.MediumId = request.MediumId;
                            existRecord.IsActive = true;
                            existRecord.ChannelId = request.ChannelId;
                            existRecord.StartTime = TimeSpan.Parse(request.StartTime);
                            existRecord.EndTime = TimeSpan.Parse(request.EndTime);
                            //existRecord.ScheduleName = model.ScheduleName;
                            existRecord.CreatedDate = DateTime.Now;
                            //existRecord.Description = model.Description;

                            await _dbContext.SaveChangesAsync();
                            response.data.schedulerDetails = existRecord;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Schedule Updated";
                        }

                    }

                }
                else
                {
                    var record = _dbContext.ScheduleDetails.AsQueryable().Where(x => x.IsDeleted == false && x.ChannelId == request.ChannelId && x.JobId == (request.JobId == 0 ? null : request.JobId) && x.PolicyId == (request.PolicyId == 0 ? null : request.PolicyId) && x.ProjectId == (request.ProjectId == 0 ? null : request.ProjectId) && x.StartTime.ToString(@"hh\:mm") == request.StartTime && x.EndTime.ToString(@"hh\:mm") == request.EndTime && DateTime.UtcNow.Date <= x.EndDate.Date);
                    if (record.Count() > 0)
                    {
                        bool stat = CheckRepeatDays(record.FirstOrDefault(), request);
                        if (stat == false)
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = "Schedule already exists for the time";
                        }
                        else
                        {
                            ApiResponse response2 = await SaveSchedule(request, request.CreatedById);
                            if (response2.StatusCode == 200)
                            {
                                ApiResponse response1 = await GetScheduleDetailsById(Convert.ToInt32(response2.data.scheduleDetails.ScheduleId));
                                response.data.scheduleDetailsModel = response1.data.scheduleDetailsModel;
                                response.data.scheduleDetails = response1.data.scheduleDetails;
                                response.data.SchedulerList = response1.data.SchedulerList;
                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Schedule created successfully";
                            }
                            else
                            {
                                response.StatusCode = response2.StatusCode;
                                response.Message = response2.Message;
                            }

                        }

                    }
                    else
                    {
                        ApiResponse response1 = await SaveSchedule(request, request.CreatedById);
                        if (response1.StatusCode == 200)
                        {
                            ApiResponse response2 = await GetScheduleDetailsById(Convert.ToInt32(response1.data.scheduleDetails.ScheduleId));
                            response.data.scheduleDetailsModel = response2.data.scheduleDetailsModel;
                            response.data.SchedulerList = response1.data.SchedulerList;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.data.scheduleDetails = response1.data.scheduleDetails;
                            response.Message = "Schedule created successfully";
                        }
                        else
                        {
                            response.StatusCode = response1.StatusCode;
                            response.Message = response1.Message;
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

        public bool CheckRepeatDays(ScheduleDetails data, AddEditScheduleCommand model)
        {
            var status1 = false;
            DateTime startDate = model.StartDate;
            DateTime dataStartDate = data.StartDate;
            DateTime endDate = model.EndDate;
            DateTime dataEndDate = data.EndDate;
            if (startDate >= dataStartDate)
            {
                if (endDate >= dataStartDate)
                {
                    var istrueDays = model.RepeatDays.Where(d => d.status == true).ToList();
                    if (istrueDays.Count() == 0)
                    {
                        status1 = true;
                    }
                    else
                    {
                        foreach (var item in istrueDays)
                        {

                            switch (item.Value)
                            {
                                case "MON":
                                    status1 = data.Monday == true ? false : true;
                                    break;
                                case "TUE":
                                    status1 = data.Tuesday == true ? false : true;
                                    break;
                                case "WED":
                                    status1 = data.Wednesday == true ? false : true;
                                    break;
                                case "THU":
                                    status1 = data.Thursday == true ? false : true;
                                    break;
                                case "FRI":
                                    status1 = data.Friday == true ? false : true;
                                    break;
                                case "SAT":
                                    status1 = data.Saturday == true ? false : true;
                                    break;
                                case "SUN":
                                    status1 = data.Sunday == true ? false : true;
                                    break;
                            }
                        }
                    }
                }
            }
            return status1;
        }

        public async Task<ApiResponse> SaveSchedule(AddEditScheduleCommand model, string userId)
        {
            long LatestScheduleId = 0;
            var scheduleCode = string.Empty;
            ApiResponse response = new ApiResponse();           

            var scheduleDetail = _dbContext.ScheduleDetails.OrderByDescending(x => x.ScheduleId)
                                                                           .FirstOrDefault();
            if (scheduleDetail == null)
            {
                LatestScheduleId = 1;
                scheduleCode = LatestScheduleId.ToString().getScheduleCode();
            }
            else
            {
                LatestScheduleId = Convert.ToInt32(scheduleDetail.ScheduleId) + 1;
                scheduleCode = LatestScheduleId.ToString().getScheduleCode();
            }
            ScheduleDetails obj = _mapper.Map<AddEditScheduleCommand, ScheduleDetails>(model);
          
            obj.CreatedById = userId;
            if (model.ProjectId != null)
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
            bool status = false;
            if (obj.JobId != null)
            {
                var jobDetails = _dbContext.JobDetails.AsQueryable().Where(x => x.JobId == obj.JobId && x.IsDeleted == false).FirstOrDefault();
                if (jobDetails.EndDate.Date <= DateTime.UtcNow.Date)
                {
                    status = true;
                }
            }
            if (status == true)
            {
                response.Message = "End Date for job has already reached. No new schedule can be added";
                response.StatusCode = StaticResource.failStatusCode;
            }
            else
            {
                obj.StartTime = TimeSpan.Parse(model.StartTime);
                obj.EndTime = TimeSpan.Parse(model.EndTime);
                obj.ScheduleName = model.ScheduleName;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.ScheduleCode = scheduleCode;
                obj.MediumId = model.MediumId;
                obj.ChannelId = model.ChannelId;
                obj.IsActive = true;
                obj.Description = model.Description;
                if (model.RepeatDays != null && model.RepeatDays.Count > 0)
                {
                    foreach (var items in model.RepeatDays)
                    {
                        switch (items.Value)
                        {
                            case "MON":
                                obj.Monday = items.status;
                                break;
                            case "TUE":
                                obj.Tuesday = items.status;
                                break;
                            case "WED":
                                obj.Wednesday = items.status;
                                break;
                            case "THU":
                                obj.Thursday = items.status;
                                break;
                            case "FRI":
                                obj.Friday = items.status;
                                break;
                            case "SAT":
                                obj.Saturday = items.status;
                                break;
                            case "SUN":
                                obj.Sunday = items.status;
                                break;
                        }
                    }
                }            
                await _dbContext.ScheduleDetails.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                FilterSchedulerModel data = new FilterSchedulerModel();
                data.ChannelId = obj.ChannelId;
                data.MediumId = obj.MediumId;
                data.StartDate = obj.StartDate;
                ApiResponse responseData = await FilterScheduleList(data);
                response.data.SchedulerList = responseData.data.SchedulerList;
                response.data.scheduleDetails = obj;
                response.StatusCode = StaticResource.successStatusCode;
            }

            return response;
        }

        public async Task<ApiResponse> FilterScheduleList(FilterSchedulerModel model)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var activityList = await _dbContext.ScheduleDetails
                                          .Include(p => p.ProjectDetail)
                                          .Include(e => e.PolicyDetails)
                                          .Include(o => o.JobDetails)
                                          .Where(v => v.IsDeleted == false && v.IsActive == true && v.StartDate >= model.StartDate && v.ChannelId == model.ChannelId && v.MediumId == model.MediumId
                                          && (v.JobDetails.JobId == v.JobId || v.ProjectDetail.ProjectId == v.ProjectId || v.PolicyDetails.PolicyId == v.PolicyId))
                                          .ToListAsync();
                var ScheduleList = activityList.Select(b => new SchedulerModel
                {
                    PolicyId = b.PolicyId,
                    Name = b.PolicyDetails != null ? b.PolicyDetails.PolicyName : b.ProjectDetail != null ? b.ProjectDetail.ProjectName : b.JobDetails != null ? b.JobDetails.JobName : null,
                    StartTime = b.StartTime.ToString(@"hh\:mm"),
                    EndTime = b.EndTime.ToString(@"hh\:mm"),
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
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

        public async Task<ApiResponse> GetScheduleDetailsById(int model)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (model != 0)
                {
                    var scheduleById = await _dbContext.ScheduleDetails
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
                        StartDate = scheduleById.StartDate,
                        EndDate = scheduleById.EndDate,
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
    }
}
