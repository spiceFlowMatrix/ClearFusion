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
    public class GetDayScheduleByPolicyIdQueryHandler : IRequestHandler<GetDayScheduleByPolicyIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetDayScheduleByPolicyIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetDayScheduleByPolicyIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var policyList = await _dbContext.PolicyDaySchedules
                                       .Where(v => v.IsDeleted == false && v.PolicyId == request.Id).Select(x => new PolicyTimeScheduleModel
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
                if (policyList != null)
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
    }
}
