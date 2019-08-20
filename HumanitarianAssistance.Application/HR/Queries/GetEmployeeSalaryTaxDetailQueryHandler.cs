using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeSalaryTaxDetailQueryHandler: IRequestHandler<GetEmployeeSalaryTaxDetailQuery, ApiResponse>
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

                var financialYear = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted == false && request.FinancialYearId.Contains(x.FinancialYearId)).ToListAsync();

                if (financialYear.Any())
                {
                    List<SalaryTaxReportModel> salaryTaxReportListFinal = new List<SalaryTaxReportModel>();

                    foreach (FinancialYearDetail financialYearDetail in financialYear)
                    {
                        List<SalaryTaxReportModel> salaryTaxReportList = _dbContext.EmployeePaymentTypes.Where(x => x.IsDeleted == false && x.IsApproved == true && x.OfficeId == request.OfficeId && x.EmployeeID == request.EmployeeId && x.PayrollYear == financialYearDetail.StartDate.Date.Year)
                        .Select(x => new SalaryTaxReportModel
                        {
                            Currency = _dbContext.CurrencyDetails.Where(o => o.CurrencyId == x.CurrencyId).FirstOrDefault().CurrencyName,
                            CurrencyId = x.CurrencyId,
                            Office = _dbContext.OfficeDetail.Where(o => o.OfficeId == x.OfficeId).FirstOrDefault().OfficeName,
                            Date = new DateTime(x.PayrollYear.Value, x.PayrollMonth.Value, 1),
                            TotalTax = x.SalaryTax
                        }).OrderBy(x => x.Date).ToList();

                        foreach (SalaryTaxReportModel item in salaryTaxReportList)
                        {
                            ExchangeRateDetail exchangeRate = await _dbContext.ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == item.CurrencyId && x.ToCurrency == request.CurrencyId);

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