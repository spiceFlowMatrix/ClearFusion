using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeTaxCalculationQueryHandler: IRequestHandler<GetEmployeeTaxCalculationQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeTaxCalculationQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeTaxCalculationQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                EmployeeTaxReportModel obj = new EmployeeTaxReportModel();
                obj.EmployeeTaxpayerIdentification = "1001157013";
                obj.NameOfBusiness = "Coordination of Humanitarian Assistance (CHA)";
                obj.AddressOfBusiness = "Charah-e-Qambar, 5th District, Kabul, Afghanistan";
                obj.TelephoneNumber = "+93(0)700291722, +93(0)729128401";
                obj.EmailAddressEmployer = "info@cha-net.org";

                var financialYear = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted== false && request.FinancialYearId.Contains(x.FinancialYearId)).ToListAsync();

                //var record = await _dbContext.EmployeePaymentTypes.Include(x => x.EmployeeDetail).Where(x => x.EmployeeID == EmployeeId && x.OfficeId == OfficeId && x.PayrollYear.Value == financialYear.StartDate.Date.Year && x.IsApproved == true).ToListAsync();

                var record = (from ept in _dbContext.EmployeePaymentTypes
                              join ed in _dbContext.EmployeeDetail
                              on ept.EmployeeID equals ed.EmployeeID
                              join epd in _dbContext.EmployeeProfessionalDetail
                              on ed.EmployeeID equals epd.EmployeeId
                              where ept.EmployeeID == request.EmployeeId && ept.IsDeleted == false && financialYear.Select(x=> x.StartDate.Year).Contains(ept.PayrollYear.Value)  && ept.OfficeId == request.OfficeId && ept.IsApproved == true
                              select new
                              {
                                  ed.EmployeeName,
                                  ed.CurrentAddress,
                                  ed.Phone,
                                  ed.Email,
                                  Year = ept.PayrollYear,
                                  ept.NetSalary,
                                  ept.SalaryTax,
                                  EmployeeTaxpayerIdentification = epd.TinNumber
                              }).ToList();

                if (record.Count > 0)
                {
                    obj.EmployeeName = record[0].EmployeeName;
                    obj.EmployeeTaxpayerIdentification = record[0].EmployeeTaxpayerIdentification != null ? record[0].EmployeeTaxpayerIdentification : "Nill";
                    obj.EmployeeAddress = record[0].CurrentAddress;
                    obj.TelephoneNumberEmployee = record[0].Phone;
                    obj.EmailAddressEmployee = record[0].Email;

                    obj.AnnualTaxPeriod = record[0].Year.Value;
                    obj.DatesOfEmployeement = 0;
                    obj.TotalWages = record.Sum(x => x.NetSalary);
                    obj.TotalTax = record.Sum(x => x.SalaryTax);

                    obj.OfficerName = "";
                    obj.Position = "";
                    obj.Date = DateTime.Now;

                }
                response.data.EmployeeTaxReport = obj;
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