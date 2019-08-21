using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetScheduleDetailsByIdQueryHandler : IRequestHandler<GetScheduleDetailsByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetScheduleDetailsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetScheduleDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.model != 0)
                {
                    var scheduleById = await _dbContext.ScheduleDetails
                                          .Include(p => p.ProjectDetail)
                                          .Include(e => e.PolicyDetails)
                                          .Include(o => o.JobDetails)
                                          .Where(v => v.IsDeleted == false && v.JobDetails.JobId == v.JobId)
                                          .Where(v => v.ProjectDetail.ProjectId == v.ProjectId)
                                          .Where(v => v.PolicyDetails.PolicyId == v.PolicyId)
                                          .Where(v => v.ScheduleId == request.model)
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
