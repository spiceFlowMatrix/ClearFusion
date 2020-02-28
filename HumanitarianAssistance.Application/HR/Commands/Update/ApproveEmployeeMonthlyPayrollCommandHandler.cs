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
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveEmployeeMonthlyPayrollCommandHandler : IRequestHandler<ApproveEmployeeMonthlyPayrollCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public ApproveEmployeeMonthlyPayrollCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(ApproveEmployeeMonthlyPayrollCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    EmployeePayrollInfoDetail payroll = new EmployeePayrollInfoDetail();

                    payroll.IsSalaryApproved = true;
                    payroll.GrossSalary = request.GrossSalary;
                    payroll.NetSalary = request.NetSalary;
                    payroll.Month = request.Month;
                    payroll.Year = DateTime.UtcNow.Year;
                    payroll.EmployeeId = request.EmployeeId;
                    payroll.HourlyRate= request.HourlyRate;

                    _dbContext.EmployeePayrollInfoDetail.Add(payroll);
                    await _dbContext.SaveChangesAsync();

                    List<AccumulatedSalaryHeadDetail> salaryHead = new List<AccumulatedSalaryHeadDetail>();

                    foreach (var item in request.SalaryHeadList)
                    {
                        AccumulatedSalaryHeadDetail obj = new AccumulatedSalaryHeadDetail
                        {
                            CreatedById = request.CreatedById,
                            CreatedDate = DateTime.UtcNow,
                            IsDeleted = false,
                            Month = request.Month,
                            Year = DateTime.UtcNow.Year,
                            EmployeeId = request.EmployeeId,
                            SalaryAllowance = item.SalaryAllowance,
                            SalaryDeduction = item.SalaryDeduction,
                            SalaryComponentId = item.Id
                        };
                        salaryHead.Add(obj);
                    }

                    await _dbContext.AccumulatedSalaryHeadDetail.AddRangeAsync(salaryHead);
                    await _dbContext.SaveChangesAsync();

                    AdvanceHistoryDetail advanceHistory = await _dbContext.AdvanceHistoryDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId &&
                                             x.PaymentDate.Month == request.Month);

                    if (advanceHistory != null)
                    {
                        advanceHistory.IsApproved = true;
                        await _dbContext.SaveChangesAsync();

                        var advanceRecord = await _dbContext.Advances
                                                 .FirstOrDefaultAsync(x => x.AdvancesId == advanceHistory.AdvanceId);
                        
                         double recoveredAmount = _dbContext.AdvanceHistoryDetail.Where(x => x.IsDeleted == false && x.AdvanceId == advanceHistory.AdvanceId && x.PaymentDate.Month <= request.Month)
                                                      .Select(x => x.InstallmentPaid).DefaultIfEmpty(0).Sum();

                        if (advanceRecord.AdvanceAmount == recoveredAmount)
                        {
                            advanceRecord.IsDeducted = true;
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    tran.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return success;
        }
    }
}