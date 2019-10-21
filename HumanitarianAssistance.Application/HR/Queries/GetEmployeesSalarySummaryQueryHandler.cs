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
    public class GetEmployeesSalarySummaryQueryHandler: IRequestHandler<GetEmployeesSalarySummaryQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeesSalarySummaryQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeesSalarySummaryQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                double? TotalGrossSalary = 0.0;
                double? TotalAllowances = 0.0;
                double? TotalDeductions = 0.0;
                
                var empList = await _dbContext.EmployeeDetail
                        .Include(o => o.EmployeeSalaryDetails)
                        .Include(c => c.EmployeeSalaryDetails.CurrencyDetails)
                        .Include(x => x.EmployeePayrollList)
                        .Include(p => p.EmployeeProfessionalDetail)
                        .Where(x => x.IsDeleted == false && x.EmployeeProfessionalDetail.OfficeId == request.OfficeId && x.EmployeeTypeId == request.EmployeeTypeId)
                        .ToListAsync();

                var empCountList = await _dbContext.EmployeeProfessionalDetail.Where(x => x.EmployeeTypeId == request.EmployeeTypeId && x.OfficeId == request.OfficeId).ToListAsync();
                
                response.data.TotalEmployees = empCountList.Count;

                var userdetailslist = (from ept in _dbContext.EmployeePaymentTypes
                                       join emp in _dbContext.EmployeeMonthlyPayroll on ept.EmployeeID equals emp.EmployeeID
                                       join es in _dbContext.SalaryHeadDetails on emp.SalaryHeadId equals es.SalaryHeadId
                                       join emd in _dbContext.EmployeeDetail on ept.EmployeeID equals emd.EmployeeID
                                       where ept.OfficeId == request.OfficeId && ept.FinancialYearDate.Value.Date.Year == request.Year && emd.EmployeeTypeId == request.EmployeeTypeId && ept.IsApproved == true
                                       select new EmployeeMonthlyPayrollModel
                                       {
                                           EmployeeId = ept.EmployeeID.Value,
                                           EmployeeName = ept.EmployeeName,
                                           PaymentType = ept.PaymentType.Value,
                                           WorkingDays = ept.WorkingDays.Value,
                                           PresentDays = ept.PresentDays.Value,
                                           AbsentDays = ept.AbsentDays.Value,
                                           LeaveDays = ept.LeaveDays.Value,
                                           TotalWorkHours = ept.TotalWorkHours.Value,
                                           HourlyRate = ept.HourlyRate,
                                           TotalGeneralAmount = ept.TotalGeneralAmount,
                                           TotalAllowance = ept.TotalAllowance,
                                           TotalDeduction = ept.TotalDeduction,
                                           GrossSalary = ept.GrossSalary,
                                           OverTimeHours = ept.OverTimeHours,
                                           SalaryHeadId = emp.SalaryHeadId,
                                           MonthlyAmount = emp.MonthlyAmount,
                                           CurrencyId = emp.CurrencyId,
                                           SalaryHead = es.HeadName,
                                           HeadTypeId = es.HeadTypeId,
                                           SalaryHeadType = es.HeadTypeId == (int)SalaryHeadType.ALLOWANCE ? "Allowance" : es.HeadTypeId == (int)SalaryHeadType.DEDUCTION ? "Deduction" : es.HeadTypeId == (int)SalaryHeadType.GENERAL ? "General" : "",
                                           IsApproved = ept.IsApproved.Value,
                                           Date = ept.FinancialYearDate.Value
                                       }).ToList();
                var allowanceList = userdetailslist;
                var deductionList = userdetailslist;
                
                if (request.Month != null)
                {
                    userdetailslist = userdetailslist.Where(x => x.Date?.Month == request.Month).ToList();
                }
                if (request.AllowanceId != null)
                {
                    allowanceList = userdetailslist.Where(x => x.SalaryHeadId == request.AllowanceId && x.HeadTypeId == (int)SalaryHeadType.ALLOWANCE).ToList();
                }
                if (request.DeductionId != null)
                {
                    deductionList = userdetailslist.Where(x => x.SalaryHeadId == request.DeductionId && x.HeadTypeId == (int)SalaryHeadType.DEDUCTION).ToList();
                }

                // Record Type - Single Currency

                if (request.RecordType == (int)RECORDTYPE.SINGLE)
                {
                    var empList1 = userdetailslist.GroupBy(x => x.CurrencyId).Select(group => new EmployeeSummaryDetailModel
                    {
                        Currency = group.FirstOrDefault().CurrencyId,
                        TotalGrossSalary = group.FirstOrDefault().GrossSalary,
                        TotalAllowance = request.AllowanceId == null ? group.FirstOrDefault().TotalAllowance : allowanceList.Where(x => x.SalaryHeadId == request.AllowanceId).Sum(x => x.MonthlyAmount),
                        TotalDeduction = request.DeductionId == null ? group.FirstOrDefault().TotalDeduction : deductionList.Where(x => x.SalaryHeadId == request.DeductionId).Sum(x => x.MonthlyAmount)
                    }).ToList();
                    response.data.EmployeeSummaryDetailsList = empList1;
                }

                // Record Type - Consolidate Currency

                if (request.RecordType == (int)RECORDTYPE.CONSOLIDATE)
                {
                    var empList1 = userdetailslist.GroupBy(x => x.CurrencyId).Select(group => new
                    {
                        Currency = group.FirstOrDefault().CurrencyId,
                        TotalGrossSalary = group.FirstOrDefault().GrossSalary,
                        TotalAllowances = group.FirstOrDefault().TotalAllowance,
                        TotalDeductions = group.FirstOrDefault().TotalDeduction
                    }).ToList();

                    foreach (var item in empList1)
                    {
                        var exchangeRateList = await _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.FromCurrency == item.Currency && x.ToCurrency == request.CurrencyId);
                        TotalGrossSalary += item.TotalGrossSalary * (double)exchangeRateList.Rate;
                        TotalAllowances += item.TotalAllowances * (double)exchangeRateList.Rate;
                        if (request.AllowanceId != null)
                        {
                            var allowanceTotal = allowanceList.Where(x => x.SalaryHeadId == request.AllowanceId).Sum(x => x.MonthlyAmount);
                            TotalAllowances = allowanceTotal * (double)exchangeRateList.Rate;
                        }
                        TotalDeductions += item.TotalDeductions * (double)exchangeRateList.Rate;
                        if (request.DeductionId != null)
                        {
                            var deductionTotal = deductionList.Where(x => x.SalaryHeadId == request.DeductionId).Sum(x => x.MonthlyAmount);
                            TotalDeductions = deductionTotal * (double)exchangeRateList.Rate;
                        }
                    }
                    response.data.TotalGrossSalary = TotalGrossSalary;
                    response.data.TotalAllowances = TotalAllowances;
                    response.data.TotalDeductions = TotalDeductions;
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