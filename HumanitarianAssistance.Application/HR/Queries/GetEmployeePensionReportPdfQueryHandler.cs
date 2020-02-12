using System;
using System.Linq;
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

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePensionReportPdfQueryHandler : IRequestHandler<GetEmployeePensionReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        private IMapper _mapper;
        private readonly IActionLogService _actionLog;
        public GetEmployeePensionReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IPdfExportService pdfExportService, IHostingEnvironment env,IActionLogService actionLog)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
            _mapper = mapper;
            _actionLog = actionLog;
        }
        public async Task<byte[]> Handle(GetEmployeePensionReportPdfQuery request, CancellationToken cancellationToken)
        {

            EmployeePensionReportPdfMdel model = new EmployeePensionReportPdfMdel();
            try
            {
                if (request != null)
                {
                    //  model = _mapper.Map<EmployeePensionReportPdfMdel>(request);
                    model.EmployeeId = request.EmployeeId;
                    model.EmployeeName = request.EmployeeName;
                    model.Currency = request.Currency;
                    model.LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;
                    model.PensionDeductionTotal= request.PensionDeductionTotal;
                    model.PensionProfitTotal = request.PensionProfitTotal;
                    model.Total = request.Total;
                    model.PensionReportListModel= request.PensionReportModel.Select(x=> new PensionReportListModel
                    {
                        Year = x.Year,
                        PensionList = x.PensionReportList.Select(y=> new PensionList
                        {
                            Date = Convert.ToDateTime(y.Date).Date.Month.ToString() ,
                            GrossSalary = y.GrossSalary,
                            PensionRate = y.PensionRate,
                            PensionDeduction = y.PensionDeduction,
                            Profit = y.Profit,
                            Total = y.Total
                        }).ToList()
                    }
                     ).ToList();

                }

                    AuditLogModel logs = new AuditLogModel () {
                    EmployeeId = (int) request.EmployeeId,
                    TypeOfEntity = (int) TypeOfEntity.TaxAndPension,
                    EntityId = null,
                    ActionTypeId = (int) ActionType.Download,
                    ActionDescription = (TypeOfEntity.TaxAndPension).GetDescription (),
                };
                bool isLoged = await _actionLog.AuditLog (logs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/EmployeePensionReport.cshtml");

        }
    }
}