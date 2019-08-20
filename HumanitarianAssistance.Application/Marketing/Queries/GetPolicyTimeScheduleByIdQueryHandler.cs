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
    public class GetPolicyTimeScheduleByIdQueryHandler : IRequestHandler<GetPolicyTimeScheduleByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetPolicyTimeScheduleByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetPolicyTimeScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var policyList = await _dbContext.PolicyTimeSchedules
                                       .Where(v => v.IsDeleted == false && v.Id == request.Id).Select(x => new PolicyTimeScheduleModel
                                       {
                                           Id = x.Id,
                                           PolicyId = x.PolicyId,
                                           StartTime = x.StartTime.ToString(@"hh\:mm"),
                                           EndTime = x.EndTime.ToString(@"hh\:mm")
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
    }
}
