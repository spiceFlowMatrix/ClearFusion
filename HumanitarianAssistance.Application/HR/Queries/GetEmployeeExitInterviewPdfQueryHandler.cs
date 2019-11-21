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
    public class GetEmployeeExitInterviewPdfQueryHandler : IRequestHandler<GetEmployeeExitInterviewPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        private IMapper _mapper;

        public GetEmployeeExitInterviewPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
            _mapper = mapper;
        }

        public async Task<byte[]> Handle(GetEmployeeExitInterviewPdfQuery request, CancellationToken cancellationToken)
        {

            EmployeeExitInteviewPdfModel model = new EmployeeExitInteviewPdfModel();
            try
            {
                if (request != null)
                {
                    model = _mapper.Map<EmployeeExitInteviewPdfModel>(request);
                    model.CheckRadioPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/radio-checked.png")?.PhysicalPath;
                    model.UncheckRadioPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/radio-unchecked.png")?.PhysicalPath;
                    model.LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg")?.PhysicalPath;
                    model.CheckedIconPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/check-box.png")?.PhysicalPath;
                    model.UnCheckedIconPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/uncheck-blank.png")?.PhysicalPath;
                    model.PersianChaName = _env.WebRootFileProvider.GetFileInfo("ReportLogo/PersianText.png")?.PhysicalPath;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await _pdfExportService.ExportToPdf(model, "Pages/PdfTemplates/EmployeeExitInterviewReport.cshtml");

        }
    }
}