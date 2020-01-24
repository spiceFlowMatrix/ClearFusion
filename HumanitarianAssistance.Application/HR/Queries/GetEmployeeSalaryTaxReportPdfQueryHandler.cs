using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeSalaryTaxReportPdfQueryHandler : IRequestHandler<GetEmployeeSalaryTaxReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        private IMapper _mapper;

        public GetEmployeeSalaryTaxReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
            _mapper = mapper;

        }
        public async Task<byte[]> Handle(GetEmployeeSalaryTaxReportPdfQuery request, CancellationToken cancellationToken)
        {

            EmployeeTaxReportModel model = new EmployeeTaxReportModel();
            try
            {
                if (request != null)
                {
                    model.TaxPayerIdentificationNumber = request.TaxPayerIdentificationNumber;
                    model.NameOfBusiness = request.NameOfBusiness;
                    model.AddressOfBusiness = request.AddressOfBusiness;
                    model.LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/taxlogo.jpg")?.PhysicalPath;
                    model.TelephoneNumber = request.TelephoneNumber;
                    model.EmailAddressEmployer = request.EmailAddressEmployer;
                    model.EmployeeName = request.EmployeeName;
                    model.EmployeeTaxpayerIdentification = request.EmployeeTaxpayerIdentification;
                    model.EmployeeAddress = request.EmployeeAddress;
                    model.TelephoneNumberEmployee = request.TelephoneNumberEmployee;
                    model.EmailAddressEmployee = request.EmailAddressEmployee;
                    model.AnnualTaxPeriod = request.AnnualTaxPeriod;
                    model.DatesOfEmployeement = request.DatesOfEmployeement;
                    model.TotalWages = request.TotalWages;
                    model.TotalTax = request.TotalTax;
                    model.OfficerName = request.OfficerName;
                    model.Position = request.Position;
                    model.ReportDate = request.Date.ToShortDateString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/EmployeeSalaryTaxReport.cshtml");

        }
    }
}