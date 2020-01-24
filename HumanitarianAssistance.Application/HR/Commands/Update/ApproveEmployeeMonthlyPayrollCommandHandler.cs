using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

                    EmployeePayrollInfoDetail payroll = await _dbContext.EmployeePayrollInfoDetail.FirstOrDefaultAsync()
                    EmployeeMonthlyAttendance monthlyAttendance = await _dbContext.EmployeeMonthlyAttendance
                                                                           .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                           x.Month == request.Month && x.Year == DateTime.UtcNow.Year &&
                                                                           x.EmployeeId == request.EmployeeId);

                    monthlyAttendance.IsApproved = true;
                    monthlyAttendance.GrossSalary = request.GrossSalary;
                    monthlyAttendance.NetSalary = request.NetSalary;

                    _dbContext.EmployeeMonthlyAttendance.Update(monthlyAttendance);
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
                            Year= DateTime.UtcNow.Year,
                            EmployeeId = request.EmployeeId,
                            SalaryAllowance = item.SalaryAllowance,
                            SalaryDeduction = item.SalaryDeduction,
                            SalaryComponentId = item.Id
                        };
                        salaryHead.Add(obj);
                    }

                    await _dbContext.AccumulatedSalaryHeadDetail.AddRangeAsync(salaryHead);
                    await _dbContext.SaveChangesAsync();

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