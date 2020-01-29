using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RevokeEmployeePayrollCommandHandler : IRequestHandler<RevokeEmployeePayrollCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public RevokeEmployeePayrollCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(RevokeEmployeePayrollCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                EmployeePayrollInfoDetail payroll = await _dbContext.EmployeePayrollInfoDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId
                                                                                                               && x.Month == request.Month && x.Year == DateTime.UtcNow.Year);

                if (payroll == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                // AdvanceHistoryDetail advanceHistory = await _dbContext.AdvanceHistoryDetail
                //                                                       .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                //                                                       x.EmployeeId == request.EmployeeId && x.PaymentDate.Month == request.Month);

                // if(advanceHistory != null)
                // {
                //     Advances advance = await _dbContext.Advances.FirstOrDefaultAsync(x=> x.AdvancesId == advanceHistory.AdvanceId);
                // }

                payroll.IsSalaryApproved = false;
                payroll.IsDeleted = true;

                List<AccumulatedSalaryHeadDetail> list = await _dbContext.AccumulatedSalaryHeadDetail.Where(x => x.IsDeleted == false &&
                                                                x.EmployeeId == request.EmployeeId && x.Month == request.Month &&
                                                                x.Year == DateTime.UtcNow.Year).ToListAsync();

                if (list.Any())
                {
                    list.ForEach(x => x.IsDeleted = true);
                }

                await _dbContext.SaveChangesAsync();

                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }
    }
}