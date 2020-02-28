using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace HumanitarianAssistance.Application.HR.Queries {
    public class GetEmployeeSalaryTaxReportPdfQueryHandler : IRequestHandler<GetEmployeeSalaryTaxReportPdfQuery, byte[]> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        private IMapper _mapper;
        private readonly IActionLogService _actionLog;
        public GetEmployeeSalaryTaxReportPdfQueryHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper, IPdfExportService pdfExportService, IHostingEnvironment env, IActionLogService actionLog) {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
            _mapper = mapper;
            _actionLog = actionLog;
        }
        public async Task<byte[]> Handle (GetEmployeeSalaryTaxReportPdfQuery request, CancellationToken cancellationToken) {

            EmployeeTaxReportModel model = new EmployeeTaxReportModel ();
            try {
                if (request != null) {
                    model.TaxPayerIdentificationNumber = request.TaxPayerIdentificationNumber;
                    model.NameOfBusiness = request.NameOfBusiness;
                    model.AddressOfBusiness = request.AddressOfBusiness;
                    model.LogoPath = _env.WebRootFileProvider.GetFileInfo ("ReportLogo/taxlogo.jpg")?.PhysicalPath;
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
                    model.ReportDate = request.Date.ToShortDateString ();
                    model.Image1Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image1.jpg")?.PhysicalPath;
                    model.Image2Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image2.jpg")?.PhysicalPath;
                    model.Image3Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image3.jpg")?.PhysicalPath;
                    model.Image4Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image4.jpg")?.PhysicalPath;
                    model.Image5Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image5.jpg")?.PhysicalPath;
                    model.Image6Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image6.jpg")?.PhysicalPath;
                    model.Image7Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image7.jpg")?.PhysicalPath;
                    model.Image8Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image8.jpg")?.PhysicalPath;
                    model.Image9Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image9.jpg")?.PhysicalPath;
                    model.Image10Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image10.jpg")?.PhysicalPath;
                    model.Image11Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image11.jpg")?.PhysicalPath;
                    model.Image12Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image12.jpg")?.PhysicalPath;
                    model.Image13Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image13.jpg")?.PhysicalPath;
                    model.Image14Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image14.jpg")?.PhysicalPath;
                    model.Image15Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image15.jpg")?.PhysicalPath;
                    model.Image16Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image16.jpg")?.PhysicalPath;
                    model.Image17Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image17.jpg")?.PhysicalPath;
                    model.Image18Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image18.jpg")?.PhysicalPath;
                    model.Image19Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image19.jpg")?.PhysicalPath;
                    model.Image20Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image20.jpg")?.PhysicalPath;
                    model.Image21Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image21.jpg")?.PhysicalPath;
                    model.Image22Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image22.jpg")?.PhysicalPath;
                    model.Image23Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image23.jpg")?.PhysicalPath;
                    model.Image24Path = _env.WebRootFileProvider.GetFileInfo ("EmployeeSalarySlipImages/image24.jpg")?.PhysicalPath;
                }
                AuditLogModel logs = new AuditLogModel () {
                    EmployeeId = (int) request.EmployeeId,
                    TypeOfEntity = (int) TypeOfEntity.TaxAndPension,
                    EntityId = null,
                    ActionTypeId = (int) ActionType.Download,
                    ActionDescription = (TypeOfEntity.TaxAndPension).GetDescription (),
                };
                bool isLoged = await _actionLog.AuditLog (logs);
            } catch (Exception ex) {
                throw ex;
            }
            return await _pdfExportService.ExportToPdf (model, "Pages/PdfTemplates/EmployeeSalaryTaxReport.cshtml");

        }
    }
}