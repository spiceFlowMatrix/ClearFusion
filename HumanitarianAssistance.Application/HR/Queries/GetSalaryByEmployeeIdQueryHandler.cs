using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetSalaryByEmployeeIdQueryHandler: BaseModel, IRequestHandler<GetSalaryByEmployeeIdQuery,ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetSalaryByEmployeeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetSalaryByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            bool isSalaryHeadSaved = false;
            bool isPayrollHeadSaved = false;

            try
            {

                var existrecord = await _dbContext.EmployeePayroll.Where(x => x.EmployeeID == request.EmployeeId).ToListAsync();;

                var employeepayrolllist = (from salaryhead in _dbContext.SalaryHeadDetails
                                           join emppayroll in _dbContext.EmployeePayroll on salaryhead.SalaryHeadId equals emppayroll.SalaryHeadId
                                           into employeepayrollinfo
                                           from employeepayrolls in employeepayrollinfo.DefaultIfEmpty()
                                           where (salaryhead.IsDeleted == false && employeepayrolls.EmployeeID == request.EmployeeId)
                                           select new EmployeePayrollModel
                                           {
                                               PayrollId =  employeepayrolls.PayrollId,
                                               SalaryHeadType = salaryhead.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : salaryhead.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : salaryhead.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
                                               HeadTypeId = salaryhead.HeadTypeId,
                                               SalaryHeadId = salaryhead.SalaryHeadId,
                                               SalaryHead = salaryhead.HeadName,
                                               MonthlyAmount = employeepayrolls.MonthlyAmount ?? 0,
                                               CurrencyId = employeepayrolls.CurrencyId ?? 0,
                                               PaymentType = employeepayrolls.PaymentType ?? 0,
                                               PensionRate = employeepayrolls.PensionRate ?? 0,
                                               AccountNo = employeepayrolls.AccountNo==null? salaryhead.AccountNo: employeepayrolls.AccountNo,
                                               TransactionTypeId = salaryhead.TransactionTypeId
                                           }).ToList();

                if (existrecord.Any())
                {
                    isSalaryHeadSaved = existrecord.Count == employeepayrolllist.Count ? true : false;
                }


                var employeeAccountHeadPayroll = (from payrollHead in await _dbContext.PayrollAccountHead.Where(x => x.IsDeleted == false).ToListAsync()
                                                  join payrollChild in await _dbContext.EmployeePayrollAccountHead.Where(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false).ToListAsync() on payrollHead.PayrollHeadId equals payrollChild.PayrollHeadId
                                                  into employeepayrollinfo
                                                  from employeepayrolls in employeepayrollinfo.DefaultIfEmpty()
                                                  select new EmployeePayrollAccountModel
                                                  {
                                                      PayrollHeadId = payrollHead.PayrollHeadId,
                                                      SalaryHeadType = payrollHead.PayrollHeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : payrollHead.PayrollHeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : payrollHead.PayrollHeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
                                                      PayrollHeadTypeId = payrollHead.PayrollHeadTypeId,
                                                      PayrollHeadName = payrollHead.PayrollHeadName,
                                                      AccountNo = employeepayrolls?.AccountNo ?? payrollHead.AccountNo,
                                                      TransactionTypeId = employeepayrolls?.TransactionTypeId ?? payrollHead.TransactionTypeId,
                                                      EmployeeId = request.EmployeeId
                                                  }).ToList();

                var existrecordPayrollHead = await _dbContext.EmployeePayrollAccountHead.Where(x => x.EmployeeId == request.EmployeeId).ToListAsync();

                if (existrecordPayrollHead.Any())
                {
                    isPayrollHeadSaved = existrecordPayrollHead.Count == employeeAccountHeadPayroll.Count ? true : false;
                }

                response.data.EmployeePayrollList = employeepayrolllist.OrderBy(x=> x.TransactionTypeId).ThenBy(x=> x.SalaryHeadType).ToList();
                response.data.EmployeePayrollAccountHeadList = employeeAccountHeadPayroll;
                response.data.isSalaryHeadSaved = isSalaryHeadSaved;
                response.data.isPayrollHeadSaved = isPayrollHeadSaved;

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

    }
}