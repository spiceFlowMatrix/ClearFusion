using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePayrollCurrencyQueryHandler : IRequestHandler<GetEmployeePayrollCurrencyQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeePayrollCurrencyQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetEmployeePayrollCurrencyQuery request, CancellationToken cancellationToken)
        {
           string currencyName= string.Empty; 

            try
            {
                EmployeeBasicSalaryDetail payrollCurrency = await _dbContext.EmployeeBasicSalaryDetail
                                                                            .Include(x=> x.CurrencyDetails)
                                                                            .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                                            x.EmployeeId == request.EmployeeId);

                if(payrollCurrency == null)
                {
                    throw new Exception(StaticResource.EmployeePayrollCurrencyNotSet);
                }
                else if(payrollCurrency.CurrencyDetails == null)
                {
                    throw new Exception(StaticResource.EmployeePayrollCurrencyNotSet);                   
                }

                var previousAdvance = _dbContext.Advances.Where(x=> x.IsDeleted == false &&
                                        x.EmployeeId == request.EmployeeId &&
                                        x.IsDeducted == false && (x.IsApproved == true || x.IsApproved == null));

                if(previousAdvance.Any())
                {
                    EmployeeDetail employee = await _dbContext.EmployeeDetail
                                                        .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.EmployeeID == request.EmployeeId);
                    
                    throw new Exception(string.Format(StaticResource.CannotAddAdvance,
                                        employee.EmployeeCode,
                                        employee.EmployeeName));
                }

                currencyName = payrollCurrency.CurrencyDetails.CurrencyName;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return currencyName;
        }
    }
}