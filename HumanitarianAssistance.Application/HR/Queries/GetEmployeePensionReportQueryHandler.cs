using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePensionReportQueryHandler : IRequestHandler<GetEmployeePensionReportQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeePensionReportQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeePensionReportQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            EmployeePensionRate pensionRateDetail = _dbContext.EmployeePensionRate.FirstOrDefault(x => x.IsDefault == true && x.IsDeleted == false);

            double pensionRate = pensionRateDetail != null ? (double)pensionRateDetail.PensionRate : 4.5;

            try
            {
                EmployeePensionModel epm = new EmployeePensionModel();
                List<EmployeePensionReportModel> lst = new List<EmployeePensionReportModel>();
                var financialYearList = await _dbContext.FinancialYearDetail.Where(x => request.FinancialYearId.Contains(x.FinancialYearId)).ToListAsync();
                var empList = await _dbContext.EmployeePaymentTypes
                                              .Where(x => financialYearList
                                              .Select(y => y.StartDate.Year)
                                              .Contains(x.PayrollYear.Value) &&
                                              x.OfficeId == request.OfficeId &&
                                              x.EmployeeID == request.EmployeeId &&
                                              x.IsDeleted == false).ToListAsync();

                EmployeeDetail xEmployeeDetail = await _dbContext.EmployeeDetail
                                                                 .Include(x => x.EmployeePayrollList)
                                                                 .FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeId &&
                                                                 x.IsDeleted == false);
                //Changes for Opening pension of an employee
                if (xEmployeeDetail == null)
                {
                    throw new Exception(StaticResource.EmployeeRecordNotFound);
                }

                if (!xEmployeeDetail.EmployeePayrollList.Any())
                {
                    throw new Exception(StaticResource.PayrollNotSet + xEmployeeDetail.EmployeeCode);
                }

                if (xEmployeeDetail.EmployeePayrollList.FirstOrDefault().CurrencyId == null)
                {
                    throw new Exception(StaticResource.EmployeePayrollCurrencyNotSet);
                }

                if (xEmployeeDetail.OpeningPension > 0)
                {

                    CalculatePreviousPension(xEmployeeDetail.EmployeePayrollList.FirstOrDefault().CurrencyId, request.CurrencyId, ref epm, pensionRate, xEmployeeDetail.OpeningPension);
                }

                var previousPensionList = await _dbContext.EmployeePaymentTypes.Where(x => x.PayrollYear < financialYearList.OrderByDescending(y => y.StartDate).FirstOrDefault().StartDate.Year && x.IsDeleted == false).ToListAsync();

                epm.PreviousPensionRate = previousPensionList.Average(x => x.PensionRate);
                // to get currency id of employee 
                EmployeePayroll currencyDetail = await _dbContext.EmployeePayroll.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId);
                // to get currency value 
                if (currencyDetail == null)
                {
                    throw new Exception(StaticResource.EmployeePayrollCurrencyNotSet);
                }

                foreach (var item in previousPensionList)
                {

                    CalculatePreviousPension(currencyDetail.CurrencyId, request.CurrencyId, ref epm, item.PensionRate, item.PensionAmount);

                }

                ExchangeRateDetail exchangeRate = await _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == currencyDetail.CurrencyId && x.ToCurrency == request.CurrencyId);
                if (exchangeRate != null)
                {

                    foreach (var item in empList)
                    {
                        EmployeePensionReportModel obj = new EmployeePensionReportModel();
                        obj.CurrencyId = currencyDetail.CurrencyId.Value;
                        obj.Date = new DateTime(item.PayrollYear.Value, item.PayrollMonth.Value, 1);
                        obj.GrossSalary = Math.Round(Convert.ToDouble(item.GrossSalary), 2) * (double)exchangeRate.Rate;
                        obj.PensionRate = pensionRate;
                        obj.PensionDeduction = Math.Round(Convert.ToDouble((item.GrossSalary * pensionRate) / 100), 2) * (double)exchangeRate.Rate;
                        obj.Profit = Math.Round(Convert.ToDouble((obj.PensionDeduction * pensionRate)) / 100, 2);
                        obj.Total = obj.Profit + obj.PensionDeduction;
                        lst.Add(obj);
                    }
                }
                epm.EmployeePensionReportList = lst.OrderBy(x => x.Date.Date).ToList();
                epm.PensionTotal = lst.Sum(x => x.Total);
                epm.PensionProfitTotal = lst.Sum(x => x.Profit);
                epm.PensionDeductionTotal = Math.Round(Convert.ToDouble(lst.Sum(x => x.GrossSalary * pensionRate / 100)), 2);
                epm.EmployeeCode = xEmployeeDetail.EmployeeCode;
                response.data.EmployeePensionModel = epm;
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

        public void CalculatePreviousPension(int? employeeSalaryCurrency, int selectedCurrency, ref EmployeePensionModel epm, double? pensionRate, double? pensionAmount)
        {

            if (employeeSalaryCurrency == selectedCurrency)
            {
                if (epm.PreviousPensionDeduction == null || epm.PreviousProfit == null || epm.PreviousTotal == null)
                {
                    epm.PreviousPensionDeduction = 0;
                    epm.PreviousProfit = 0;
                    epm.PreviousTotal = 0;
                }
                epm.PreviousPensionDeduction += pensionAmount;
                epm.PreviousProfit += Math.Round(Convert.ToDouble(pensionAmount * pensionRate), 2);
                epm.PreviousTotal += Math.Round(Convert.ToDouble((pensionAmount * pensionRate) + pensionAmount), 2);
            }
            else
            {
                ExchangeRateDetail exchangeRate = _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefault(x => x.IsDeleted == false && x.FromCurrency == employeeSalaryCurrency && x.ToCurrency == selectedCurrency);
                epm.PreviousPensionDeduction += pensionAmount * (double)exchangeRate.Rate;
                epm.PreviousProfit += Math.Round(Convert.ToDouble(pensionAmount * pensionRate), 2) * (double)exchangeRate.Rate;
                epm.PreviousTotal += Math.Round(Convert.ToDouble((pensionAmount * pensionRate) + pensionAmount), 2) * (double)exchangeRate.Rate;
            }
        }
    }
}