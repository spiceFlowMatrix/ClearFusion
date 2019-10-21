using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class SaveEmployeePayrollPaymentCommandHandler: IRequestHandler<SaveEmployeePayrollPaymentCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public SaveEmployeePayrollPaymentCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(SaveEmployeePayrollPaymentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                foreach (EmployeeMonthlyPayrollModel employeePayroll in request.EmployeeMonthlyPayroll)
                {
                    EmployeeMonthlyAttendance employeeMonthlyAttendance = await _dbContext.EmployeeMonthlyAttendance.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeId == employeePayroll.EmployeeId && x.Year == employeePayroll.Year && x.Month == employeePayroll.Month && x.OfficeId == employeePayroll.OfficeId);

                    employeeMonthlyAttendance.HourlyRate = employeePayroll.HourlyRate;
                    employeeMonthlyAttendance.GrossSalary = employeePayroll.GrossSalary;
                    employeeMonthlyAttendance.NetSalary = employeePayroll.NetSalary;
                    employeeMonthlyAttendance.PaymentType = employeePayroll.PaymentType;
                    employeeMonthlyAttendance.PensionAmount = employeePayroll.PensionAmount;
                    employeeMonthlyAttendance.PensionRate = employeePayroll.PensionRate;
                    employeeMonthlyAttendance.SalaryTax = employeePayroll.SalaryTax;
                    employeeMonthlyAttendance.TotalAllowance = employeePayroll.TotalAllowance;
                    employeeMonthlyAttendance.TotalDeduction = employeePayroll.TotalDeduction;
                    employeeMonthlyAttendance.IsAdvanceRecovery = employeePayroll.IsAdvanceRecovery;
                    employeeMonthlyAttendance.IsAdvanceApproved = employeePayroll.IsAdvanceApproved;
                    employeeMonthlyAttendance.AdvanceAmount = employeePayroll.AdvanceAmount == null ? 0 : employeePayroll.AdvanceAmount.Value;
                    employeeMonthlyAttendance.AdvanceRecoveryAmount = employeePayroll.AdvanceRecoveryAmount == null ? 0 : employeePayroll.AdvanceRecoveryAmount.Value;

                    _dbContext.EmployeeMonthlyAttendance.Update(employeeMonthlyAttendance);
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