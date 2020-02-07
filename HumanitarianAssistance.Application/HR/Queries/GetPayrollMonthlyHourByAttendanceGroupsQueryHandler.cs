using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollMonthlyHourByAttendanceGroupsQueryHandler : IRequestHandler<GetPayrollMonthlyHourByAttendanceGroupsQuery, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetPayrollMonthlyHourByAttendanceGroupsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetPayrollMonthlyHourByAttendanceGroupsQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();
            try
            {
                var payrollinfo = await _dbContext.PayrollMonthlyHourDetail
                                    .Where(x => x.IsDeleted == false &&
                                    x.AttendanceGroupId == request.AttendanceGroupId).ToListAsync();

                result.Add("PayrollList", payrollinfo);
            }
            catch(Exception ex)
            { 
                throw ex;
            }
            return result;
        }
    }
}