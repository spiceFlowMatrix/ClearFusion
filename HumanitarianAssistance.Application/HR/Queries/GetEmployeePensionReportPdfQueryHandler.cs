using System;
using System.Linq;
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
    public class GetEmployeePensionReportPdfQueryHandler : IRequestHandler<GetEmployeePensionReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        private IMapper _mapper;

        public GetEmployeePensionReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
            _mapper = mapper;

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

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/EmployeePensionReport.cshtml");

        }
    }
}