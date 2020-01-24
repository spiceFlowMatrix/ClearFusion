using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeBasicPayAndCurrencyQueryHandler : IRequestHandler<GetEmployeeBasicPayAndCurrencyQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeBasicPayAndCurrencyQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeBasicPayAndCurrencyQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                var basicPayDetail = await _dbContext.EmployeePayroll
                                                     .Include(x => x.CurrencyDetails)
                                                     .Where(x => x.IsDeleted == false &&
                                                                    x.EmployeeID == request.EmployeeId &&
                                                                    x.HeadTypeId == (int)SalaryHeadType.GENERAL)
                                                                    .Select(x => new
                                                                    {
                                                                       PayrollId= x.PayrollId,
                                                                       CurrencyId= x.CurrencyId,
                                                                       MonthlyAmount= x.MonthlyAmount,
                                                                       CurrencyName = x.CurrencyDetails.CurrencyName
                                                                    }).FirstOrDefaultAsync();

                response.Add("EmployeeCurrencyAmount", basicPayDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}