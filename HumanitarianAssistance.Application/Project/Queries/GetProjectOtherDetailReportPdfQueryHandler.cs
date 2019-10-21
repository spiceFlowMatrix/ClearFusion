using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetProjectOtherDetailReportPdfQueryHandler : IRequestHandler<GetProjectOtherDetailReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;
        private readonly IMapper _mapper;
        public GetProjectOtherDetailReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService,IHostingEnvironment env,IMapper mapper)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env=env;
            _mapper=mapper;
        }

        public async Task<byte[]> Handle(GetProjectOtherDetailReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var projectdetails=await _dbContext.LoadStoredProc("get_project_other_detail_pdf")
                                        .WithSqlParam("projectid", request.ProjectId)                           
                                        .ExecuteStoredProc<ProjectOtherDetailPdfModel>();
                var detail=projectdetails.FirstOrDefault();
                var newdetail=_mapper.Map<ProjectOtherDetailNewPdfModel>(detail);
                var flags=_mapper.Map<ProjectPdfFlags>(request);
                newdetail.flags=flags;
                return await _pdfExportService.ExportToPdf(newdetail, "Pages/PdfTemplates/ProjectOtherDetailReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}