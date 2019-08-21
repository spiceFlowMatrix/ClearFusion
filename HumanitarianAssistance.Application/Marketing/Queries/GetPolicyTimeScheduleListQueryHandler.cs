using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetPolicyTimeScheduleListQueryHandler : IRequestHandler<GetPolicyTimeScheduleListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetPolicyTimeScheduleListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetPolicyTimeScheduleListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int count = await _dbContext.PolicyTimeSchedules.CountAsync(x => x.IsDeleted == false);
                var policyScheduleList = await (from j in _dbContext.PolicyTimeSchedules
                                                where !j.IsDeleted && j.PolicyId == request.Id
                                                select (new PolicyTimeScheduleModel
                                                {
                                                    PolicyId = j.PolicyId,
                                                    StartTime = j.StartTime.ToString(@"hh\:mm"),
                                                    EndTime = j.EndTime.ToString(@"hh\:mm"),
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
    }
}
