using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class HRService : IHRService
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public HRService(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddEmployeePayrollDetails(int? EmployeeId)
        {
            bool payrollSaved = false;

            try
            {
                bool employeeSalaryHeadAlreadyExists = _dbContext.EmployeePayroll.Any(x => x.IsDeleted == false && x.EmployeeID == EmployeeId);

                // add salary head and payroll heads only if it does not already exists for an employee
                if (!employeeSalaryHeadAlreadyExists)
                {
                    //Get Default payrollaccountheads and save it to the newly created employee
                    List<SalaryHeadDetails> salaryHeadDetails = await _dbContext.SalaryHeadDetails.Where(x => x.IsDeleted == false).ToListAsync();
                    List<EmployeePayroll> EmployeePayrollList = new List<EmployeePayroll>();

                    foreach (SalaryHeadDetails salaryHead in salaryHeadDetails)
                    {
                        EmployeePayroll employeePayroll = new EmployeePayroll();
                        employeePayroll.AccountNo = salaryHead.AccountNo;
                        employeePayroll.HeadTypeId = salaryHead.HeadTypeId;
                        employeePayroll.SalaryHeadId = salaryHead.SalaryHeadId;
                        employeePayroll.IsDeleted = false;
                        employeePayroll.TransactionTypeId = salaryHead.TransactionTypeId;
                        employeePayroll.EmployeeID = EmployeeId.Value;
                        EmployeePayrollList.Add(employeePayroll);
                    }

                    List<EmployeePayrollAccountHead> payrollAccountHeadList = await _dbContext.EmployeePayrollAccountHead
                                                                                          .Where(x => !x.IsDeleted && x.EmployeeId == EmployeeId)
                                                                                          .ToListAsync();

                    List<EmployeePayrollAccountHead> employeePayrollAccountHeads = new List<EmployeePayrollAccountHead>();

                    if (!payrollAccountHeadList.Any())
                    {
                        //Get Default payrollaccountheads and save it to the newly created employee
                        List<PayrollAccountHead> payrollAccountHeads = await _dbContext.PayrollAccountHead.Where(x => x.IsDeleted == false).ToListAsync();

                        foreach (var employeePayrollAccount in payrollAccountHeads)
                        {
                            EmployeePayrollAccountHead employeePayrollAccountHead = new EmployeePayrollAccountHead();

                            employeePayrollAccountHead.IsDeleted = false;
                            employeePayrollAccountHead.AccountNo = employeePayrollAccount.AccountNo;
                            employeePayrollAccountHead.Description = employeePayrollAccount.Description;
                            employeePayrollAccountHead.EmployeeId = EmployeeId.Value;
                            employeePayrollAccountHead.PayrollHeadId = employeePayrollAccount.PayrollHeadId;
                            employeePayrollAccountHead.PayrollHeadName = employeePayrollAccount.PayrollHeadName;
                            employeePayrollAccountHead.PayrollHeadTypeId = employeePayrollAccount.PayrollHeadTypeId;
                            employeePayrollAccountHead.TransactionTypeId = employeePayrollAccount.TransactionTypeId;

                            employeePayrollAccountHeads.Add(employeePayrollAccountHead);
                        }
                    }

                    await _dbContext.EmployeePayrollAccountHead.AddRangeAsync(employeePayrollAccountHeads);
                    await _dbContext.EmployeePayroll.AddRangeAsync(EmployeePayrollList);
                    await _dbContext.SaveChangesAsync();

                    payrollSaved = true;
                }
                else
                {
                    payrollSaved = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return payrollSaved;
            }
            return payrollSaved;
        }
    }
}