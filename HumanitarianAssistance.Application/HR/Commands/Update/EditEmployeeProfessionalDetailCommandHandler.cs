using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeProfessionalDetailCommandHandler : IRequestHandler<EditEmployeeProfessionalDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public EditEmployeeProfessionalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(EditEmployeeProfessionalDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeProfessionalDetail existrecord = await _dbContext.EmployeeProfessionalDetail.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                                              x.EmployeeId == request.EmployeeId);
                if (existrecord == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }
                existrecord.EmployeeTypeId = request.EmployeeTypeId;
                existrecord.OfficeId = request.OfficeId;
                existrecord.DepartmentId = request.DepartmentId;
                existrecord.DesignationId = request.DesignationId;
                existrecord.EmployeeContractTypeId = request.EmployeeContractTypeId;
                existrecord.HiredOn = request.HiredOn;
                existrecord.FiredOn = request.FiredOn;
                existrecord.FiredReason = request.FiredReason;
                existrecord.ResignationOn = request.ResignationOn;
                existrecord.TrainingBenefits = request.TrainingBenefits;
                existrecord.JobDescription = request.JobDescription;
                existrecord.ResignationReason = request.ResignationReason;
                existrecord.AttendanceGroupId = request.AttendanceGroupId;
                existrecord.MembershipSupportInPoliticalParty = request.MembershipSupportInPoliticalParty;
                existrecord.ModifiedById = request.ModifiedById;
                existrecord.ModifiedDate = request.ModifiedDate;
                await _dbContext.SaveChangesAsync();

                var employeeinfo = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeId && x.IsDeleted == false);
                if (employeeinfo != null)
                {
                    employeeinfo.EmployeeTypeId = request.EmployeeTypeId;
                    await _dbContext.SaveChangesAsync(); ;
                }

                //when employee is active
                if (request.EmployeeTypeId == (int)EmployeeTypeStatus.Active)
                {
                    bool employeeSalaryHeadAlreadyExists = _dbContext.EmployeePayroll.Any(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId);

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
                            employeePayroll.EmployeeID = request.EmployeeId.Value;
                            EmployeePayrollList.Add(employeePayroll);
                        }

                        await _dbContext.EmployeePayroll.AddRangeAsync(EmployeePayrollList);
                        await _dbContext.SaveChangesAsync();


                        //Get Default payrollaccountheads and save it to the newly created employee
                        List<PayrollAccountHead> payrollAccountHeads = await _dbContext.PayrollAccountHead.Where(x => x.IsDeleted == false).ToListAsync();

                        List<EmployeePayrollAccountHead> employeePayrollAccountHeads = new List<EmployeePayrollAccountHead>();

                        foreach (var employeePayrollAccount in payrollAccountHeads)
                        {
                            EmployeePayrollAccountHead employeePayrollAccountHead = new EmployeePayrollAccountHead();

                            employeePayrollAccountHead.IsDeleted = false;
                            employeePayrollAccountHead.AccountNo = employeePayrollAccount.AccountNo;
                            employeePayrollAccountHead.Description = employeePayrollAccount.Description;
                            employeePayrollAccountHead.EmployeeId = request.EmployeeId.Value;
                            employeePayrollAccountHead.PayrollHeadId = employeePayrollAccount.PayrollHeadId;
                            employeePayrollAccountHead.PayrollHeadName = employeePayrollAccount.PayrollHeadName;
                            employeePayrollAccountHead.PayrollHeadTypeId = employeePayrollAccount.PayrollHeadTypeId;
                            employeePayrollAccountHead.TransactionTypeId = employeePayrollAccount.TransactionTypeId;

                            employeePayrollAccountHeads.Add(employeePayrollAccountHead);
                        }

                        await _dbContext.EmployeePayrollAccountHead.AddRangeAsync(employeePayrollAccountHeads);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}