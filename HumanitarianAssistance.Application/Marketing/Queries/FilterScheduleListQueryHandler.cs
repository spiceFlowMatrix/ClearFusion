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
    public class FilterScheduleListQueryHandler : IRequestHandler<FilterScheduleListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public FilterScheduleListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(FilterScheduleListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var activityList = await _dbContext.ScheduleDetails
                                          .Include(p => p.ProjectDetail)
                                          .Include(e => e.PolicyDetails)
                                          .Include(o => o.JobDetails)
                                          .Where(v => v.IsDeleted == false && v.IsActive == true && v.StartDate >= request.StartDate && v.ChannelId == request.ChannelId && v.MediumId == request.MediumId
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
    }
}
