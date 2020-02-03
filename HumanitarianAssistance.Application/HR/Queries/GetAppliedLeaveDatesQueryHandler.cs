using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAppliedLeaveDatesQueryHandler: IRequestHandler<GetAppliedLeaveDatesQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAppliedLeaveDatesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAppliedLeaveDatesQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                var appliedLeaves = await _dbContext.EmployeeApplyLeave.Where(x=> x.IsDeleted == false &&
                                    x.LeaveReasonId == request.LeaveReasonId && x.EmployeeId == request.EmployeeId)
                                              .Select(x=> new 
                                              {
                                                  ToDate = x.ToDate.ToShortDateString(),
                                                  FromDate= x.FromDate.ToShortDateString()
                                              }).ToListAsync();
                response.Add("LeaveDates", appliedLeaves);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}