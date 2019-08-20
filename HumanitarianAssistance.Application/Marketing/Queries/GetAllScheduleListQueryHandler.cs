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
    public class GetAllScheduleListQueryHandler : IRequestHandler<GetAllScheduleListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllScheduleListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllScheduleListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var dateTime = DateTime.Now;
                string today = dateTime.DayOfWeek.ToString();
                var activityList = await _dbContext.ScheduleDetails
                                          .Include(p => p.ProjectDetail)
                                          .Include(e => e.PolicyDetails)
                                          .Include(o => o.JobDetails)
                                          .Where(v => v.IsDeleted == false && v.IsActive == true && (v.JobDetails.JobId == v.JobId || v.ProjectDetail.ProjectId == v.ProjectId || v.PolicyDetails.PolicyId == v.PolicyId))
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
                //ScheduleList = FilterListByDays(ScheduleList);
                //var ScheduleList = await (from j in _uow.GetDbContext().ScheduleDetails
                //                                join mc in _uow.GetDbContext().PolicyDetails on j.PolicyId equals mc.PolicyId
                //                                join pd in _uow.GetDbContext().JobDetails on j.JobId equals pd.JobId
                //                                join po in _uow.GetDbContext().ProjectDetails on j.ProjectId equals po.ProjectId
                //                                where !j.IsDeleted
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
    }
}
