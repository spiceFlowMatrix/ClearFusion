using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeSalaryTaxDetailQueryHandler : IRequestHandler<GetEmployeeSalaryTaxDetailQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeSalaryTaxDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeSalaryTaxDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                List<FinancialYearDetail> financialYear = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted == false && request.FinancialYearId.Contains(x.FinancialYearId)).ToListAsync();

                if (financialYear.Any())
                {

                    EmployeeBasicSalaryDetail payrollDetail = await _dbContext.EmployeeBasicSalaryDetail
                                                                    .Include(x => x.CurrencyDetails)
                                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                      x.EmployeeId == request.EmployeeId);
                    if (payrollDetail == null)
                    {
                        throw new Exception(StaticResource.EmployeePayrollCurrencyNotSet);
                    }
                    else if(payrollDetail.CurrencyDetails == null)
                    {
                        throw new Exception(StaticResource.EmployeePayrollCurrencyNotSet);
                    }
                    // take distinct startyear and endyear else records may repeat itself
                    var distinctFinancialYears = financialYear.Select(x => new { StartYear = x.StartDate.Year, EndYear = x.EndDate.Year }).Distinct();

                    List<SalaryTaxReportModel> salaryTaxReportListFinal = new List<SalaryTaxReportModel>();
                    var employeeData = await _dbContext.EmployeeDetail.Include(x => x.EmployeeProfessionalDetail)
                                                                .ThenInclude(x => x.OfficeDetail)
                                                                .FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeId);


                    foreach (var financialYearDetail in distinctFinancialYears)
                    {
                        List<SalaryTaxReportModel> salaryTaxReportList = await _dbContext.AccumulatedSalaryHeadDetail
                                                                                   .Where(x => x.IsDeleted == false &&
                                                                                              x.SalaryComponentId == (int)AccumulatedSalaryHead.SalaryTax &&
                                                                                               x.EmployeeId == request.EmployeeId &&
                                                                                               x.Year == financialYearDetail.StartYear)
                                                                                    .Select(x => new SalaryTaxReportModel
                                                                                    {
                                                                                        Currency = payrollDetail.CurrencyDetails.CurrencyName,
                                                                                        CurrencyId = payrollDetail.CurrencyId,
                                                                                        Office = employeeData.EmployeeProfessionalDetail.OfficeDetail.OfficeName,
                                                                                        Date = new DateTime(x.Year, x.Month, 1),
                                                                                        TotalTax = x.SalaryDeduction
                                                                                    }).OrderBy(x => x.Date).ToListAsync();

                        foreach (SalaryTaxReportModel item in salaryTaxReportList)
                        {
                            ExchangeRateDetail exchangeRate = await _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date)
                                                                                                 .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                                           x.FromCurrency == item.CurrencyId &&
                                                                                                                           x.ToCurrency == request.CurrencyId);

                            if (item.CurrencyId != request.CurrencyId)
                            {
                                item.TotalTax = item.TotalTax * (double)exchangeRate.Rate;
                            }
                        }

                        if (salaryTaxReportList.Any())
                        {
                            salaryTaxReportListFinal.AddRange(salaryTaxReportList);
                        }
                    }

                    response.data.SalaryTaxReportModelList = salaryTaxReportListFinal;
                }
                else
                    response.data.SalaryTaxReportModelList = null;
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