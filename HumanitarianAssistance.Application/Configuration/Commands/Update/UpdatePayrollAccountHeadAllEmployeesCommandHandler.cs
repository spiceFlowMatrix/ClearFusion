using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class UpdatePayrollAccountHeadAllEmployeesCommandHandler : IRequestHandler<UpdatePayrollAccountHeadAllEmployeesCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public UpdatePayrollAccountHeadAllEmployeesCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdatePayrollAccountHeadAllEmployeesCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //NOTE: Do not remove code for saving payroll heads for employees that do not exist in EmployeePayrollAccountHead table
                //List<EmployeeProfessionalDetail> employeesNotInPayrollAccountHeadTable = new List<EmployeeProfessionalDetail>();
                //List<EmployeeProfessionalDetail> employeeList = await _uow.GetDbContext().EmployeeProfessionalDetail.Where(x => x.IsDeleted == false).ToListAsync();

                IEnumerable<int> employeeIds = _dbContext.EmployeeDetail
                                              .Where(x => x.IsDeleted == false).Select(x => x.EmployeeID).ToList();

                IEnumerable<int> employeeWithNoPayrollHead;
                IEnumerable<int> employeeWithPayrollHead;

                List<EmployeePayrollAccountHead> xEmployeePayrollAccountHead = await _dbContext.EmployeePayrollAccountHead.Where(x => x.IsDeleted == false).ToListAsync();

                employeeWithPayrollHead = xEmployeePayrollAccountHead.Select(x => x.EmployeeId).Distinct().ToList();

                employeeWithNoPayrollHead = employeeIds.Except(employeeWithPayrollHead);

                foreach (PayrollHeadModel payrollHead in request.PayrollHead)
                {
                    PayrollAccountHead xPayrollAccountHead = _dbContext.PayrollAccountHead.FirstOrDefault(x => x.PayrollHeadId == payrollHead.PayrollHeadId);

                    xPayrollAccountHead.AccountNo = payrollHead.AccountNo;
                    xPayrollAccountHead.Description = payrollHead.Description;
                    xPayrollAccountHead.PayrollHeadId = payrollHead.PayrollHeadId;
                    xPayrollAccountHead.PayrollHeadName = payrollHead.PayrollHeadName;
                    xPayrollAccountHead.PayrollHeadTypeId = payrollHead.PayrollHeadTypeId;
                    xPayrollAccountHead.TransactionTypeId = payrollHead.TransactionTypeId;
                    xPayrollAccountHead.ModifiedById = request.ModifiedById;
                    xPayrollAccountHead.ModifiedDate = request.ModifiedDate;
                    xPayrollAccountHead.IsDeleted = false;

                    await _dbContext.SaveChangesAsync();

                    List<EmployeePayrollAccountHead> xEmployeePayrollAccount = xEmployeePayrollAccountHead.Where(x => x.IsDeleted == false && x.PayrollHeadId == payrollHead.PayrollHeadId).ToList();

                    if (xEmployeePayrollAccount.Any())
                    {
                        xEmployeePayrollAccount.ForEach(x =>
                        {
                            x.AccountNo = payrollHead.AccountNo; x.Description = payrollHead.Description;
                            x.PayrollHeadTypeId = payrollHead.PayrollHeadTypeId; x.TransactionTypeId = payrollHead.TransactionTypeId;
                        });
                        _dbContext.EmployeePayrollAccountHead.UpdateRange(xEmployeePayrollAccountHead);
                        await _dbContext.SaveChangesAsync();
                    }

                    List<EmployeePayrollAccountHead> employeePayrollHeads = new List<EmployeePayrollAccountHead>();

                    //Adding New Payroll Heads for Employees not having Payroll Head already saved
                    foreach (int employeeId in employeeWithNoPayrollHead)
                    {
                        EmployeePayrollAccountHead employeePayrollAccountHead = new EmployeePayrollAccountHead();
                        employeePayrollAccountHead.AccountNo = payrollHead.AccountNo;
                        employeePayrollAccountHead.CreatedById = request.ModifiedById;
                        employeePayrollAccountHead.Description = payrollHead.Description;
                        employeePayrollAccountHead.EmployeeId = employeeId;
                        employeePayrollAccountHead.IsDeleted = false;
                        employeePayrollAccountHead.PayrollHeadId = payrollHead.PayrollHeadId;
                        employeePayrollAccountHead.PayrollHeadName = payrollHead.PayrollHeadName;
                        employeePayrollAccountHead.PayrollHeadTypeId = payrollHead.PayrollHeadTypeId;
                        employeePayrollAccountHead.TransactionTypeId = payrollHead.TransactionTypeId;
                        employeePayrollHeads.Add(employeePayrollAccountHead);
                    }

                    _dbContext.EmployeePayrollAccountHead.AddRange(employeePayrollHeads);
                    await _dbContext.SaveChangesAsync();

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
