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
    public class GetEmployeeAccumulatedSalaryHeadQueryHandler : IRequestHandler<GetEmployeeAccumulatedSalaryHeadQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAccumulatedSalaryHeadQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetEmployeeAccumulatedSalaryHeadQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                var employeeSalaryHeads = await _dbContext.EmployeePayroll
                                                          .Where(x=> x.IsDeleted == false && x.EmployeeID == request.EmployeeId)
                                                          .ToListAsync();

                var employeePayrollHeads = await _dbContext.EmployeePayrollAccountHead
                                                          .Where(x=> x.IsDeleted == false && x.EmployeeId == request.EmployeeId)
                                                          .ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}