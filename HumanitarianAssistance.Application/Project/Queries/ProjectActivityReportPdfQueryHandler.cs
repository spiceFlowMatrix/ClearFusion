using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class ProjectActivityReportPdfQueryHandler : IRequestHandler<ProjectActivityReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;

        public ProjectActivityReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
        }

        public async Task<byte[]> Handle(ProjectActivityReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // model logic here


                List<ProjectActivityReportPdfModel> summary = new List<ProjectActivityReportPdfModel>();

                summary = (from pad in _dbContext.ProjectActivityDetail
                           where pad.ProjectId == request.ProjectId && pad.IsDeleted == false
                           join pmrd in _dbContext.ProjectMonitoringReviewDetail
                           on pad.ProjectId equals pmrd.ProjectId
                           join pmiq in _dbContext.ProjectMonitoringIndicatorDetail
                           on pmrd.ProjectMonitoringReviewId equals pmiq.ProjectMonitoringReviewId
                           join piq in _dbContext.ProjectIndicatorQuestions
                           on pmiq.ProjectIndicatorId equals piq.ProjectIndicatorId
                        //    join pd in _dbContext.ProjectDetail
                        //    on pad.ProjectId equals pd.ProjectId     
                        //    join pod in _dbContext.ProjectOtherDetail
                        //    on pad.ProjectId equals pod.ProjectId
                        //    join cd in _dbContext.CountryDetails
                        //    on pad.CountryId equals cd.CountryId
                        //    join prd in _dbContext.ProvinceDetails
                        //    on cd.CountryId equals prd.CountryId
                        //    join dd in _dbContext.DistrictDetail
                        //    on prd.ProvinceId equals dd.ProvinceID
                           select new ProjectActivityReportPdfModel
                           {
                               //ProjectCode = pd.ProjectCode,
                               //ProjectName = pd.ProjectName,
                               //ProjectGoal = pod.projectGoal,                               
                               MainActivity = pad.ActivityName,
                               //Recommendations = pmrd.Recommendations,
                               Start= pad.PlannedStartDate,
                               End= pad.PlannedEndDate,
                               //Country = cd.CountryName,
                               //Province = prd.ProvinceName,
                               //District = dd.District,
                               ActualStartDate=pad.ActualStartDate,
                               ActualEndDate=pad.ActualEndDate,                               
                           }).ToList();

                var activityDetail = await _dbContext.ProjectActivityDetail
                                                             .Include(x => x.ProjectDetail)
                                                             .Include(x => x.ProjectActivityProvinceDetail)
                                                             .Include(x=> x.ProjectMonitoringReviewDetail)
                                                             .ThenInclude(x=> x.ProjectMonitoringIndicatorDetail)
                                                             .ThenInclude (x=> x.ProjectMonitoringIndicatorQuestions)
                                                             .FirstOrDefaultAsync(v => v.IsDeleted == false &&
                                                                                 v.ParentId == null &&
                                                                                 v.ProjectId == request.ProjectId
                                                             );


                return await _pdfExportService.ExportToPdf(summary, "Pages/PdfTemplates/ProjectActivityReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
