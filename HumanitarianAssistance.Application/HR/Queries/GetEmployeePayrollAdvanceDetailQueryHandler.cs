using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePayrollAdvanceDetailQueryHandler: IRequestHandler<GetEmployeePayrollAdvanceDetailQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeePayrollAdvanceDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeePayrollAdvanceDetailQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            AdvanceDetailModel model = new AdvanceDetailModel();

            try
            {
                Advances advance = await _dbContext.Advances.OrderBy(x=> x.AdvanceDate).FirstOrDefaultAsync(x=> x.IsDeleted == false && x.IsApproved == true 
                                        && x.EmployeeId == request.EmployeeId && x.IsDeducted == false);

                int installmentPaidCount = _dbContext.AdvanceHistoryDetail.Where(x=> x.IsDeleted == false && x.AdvanceId == advance.AdvancesId && x.PaymentDate.Month < request.Month).Count();

                AdvanceHistoryDetail advanceHistory = await _dbContext.AdvanceHistoryDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                      x.AdvanceId == advance.AdvancesId && x.PaymentDate.Month == request.Month);
                
                if(advance == null)
                {
                    model.AdvanceId= 0;
                    model.AdvanceAmount = 0;
                    model.BalanceAmount = 0;
                }
                else
                {
                    double recoveredAmount = _dbContext.AdvanceHistoryDetail.Where(x=> x.IsDeleted == false && x.AdvanceId == advance.AdvancesId &&
                                                        x.PaymentDate.Month < request.Month)
                                                       .Select(x=> x.InstallmentPaid).DefaultIfEmpty(0).Sum();

                    model.AdvanceId = advance.AdvancesId;
                    model.AdvanceAmount = advance.AdvanceAmount;
                    model.BalanceAmount = advance.AdvanceAmount - recoveredAmount;

                    if(advanceHistory == null)
                    {
                        model.InstallmentToBePaid = (model.BalanceAmount /(advance.NumberOfInstallments.Value - installmentPaidCount));
                    }
                    else
                    {
                        model.InstallmentToBePaid = advanceHistory.InstallmentPaid;
                    }
                }

                response.Add("Advance", model);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}