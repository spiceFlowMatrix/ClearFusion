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
using Microsoft.AspNetCore.Hosting;
using HumanitarianAssistance.Persistence.Extensions;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class ProjectActivityReportPdfQueryHandler : IRequestHandler<ProjectActivityReportPdfQuery, byte[]>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IPdfExportService _pdfExportService;
        private readonly IHostingEnvironment _env;

        public ProjectActivityReportPdfQueryHandler(HumanitarianAssistanceDbContext dbContext, IPdfExportService pdfExportService, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _pdfExportService = pdfExportService;
            _env = env;
        }

        public async Task<byte[]> Handle(ProjectActivityReportPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // model logic here

                var spActivityList = await _dbContext.LoadStoredProc("get_project_projectactivitylist_filter")
                                       .WithSqlParam("project_id", request.ProjectId)
                                       .WithSqlParam("activity_description", request.ActivityDescription == null ? string.Empty : request.ActivityDescription)
                                       .WithSqlParam("planned_start_date", request.PlannedStartDate == null ? string.Empty : request.PlannedStartDate.Value.ToString())
                                       .WithSqlParam("planned_end_date", request.PlannedEndDate == null ? string.Empty : request.PlannedEndDate.Value.ToString())
                                       .WithSqlParam("actual_start_date", request.ActualStartDate == null ? string.Empty : request.ActualStartDate.Value.ToString())
                                       .WithSqlParam("actual_end_date", request.ActualEndDate == null ? string.Empty : request.ActualEndDate.Value.ToString())

                                       .WithSqlParam("assignee_id", request.AssigneeId)
                                       .WithSqlParam("budget_line_id", request.BudgetLineId)
                                       .WithSqlParam("planning", request.Planning)
                                       .WithSqlParam("implementations", request.Implementation)
                                       .WithSqlParam("completed", request.Completed)

                                       .WithSqlParam("progress_range_min", request.ProgressRangeMin)
                                       .WithSqlParam("progress_range_max", request.ProgressRangeMax)
                                       .WithSqlParam("sleepage_min", request.SleepageMin)
                                       .WithSqlParam("sleepage_max", request.SleepageMax)

                                       .WithSqlParam("duration_min", request.DurationMin)
                                       .WithSqlParam("duration_max", request.DurationMax)

                                       .WithSqlParam("late_start", request.LateStart)
                                       .WithSqlParam("late_end", request.LateEnd)
                                       .WithSqlParam("on_schedule", request.OnSchedule)
                                       .WithSqlParam("country_id", request.CountryId)

                                       .ExecuteStoredProc<SPProjectActivityDetail>();

                var activityDetail = await _dbContext.ProjectDetail
                                                     .Include(x => x.ProjectOtherDetail)
                                                     .Include(c => c.CountryMultiSelectDetails)
                                                     .Include(c => c.ProvinceMultiSelect)
                                                     .Include(c => c.DistrictMultiSelect)
                                                     .Include(d => d.ProjectActivityDetail)
                                                     .ThenInclude(m => m.ProjectMonitoringReviewDetail)
                                                     .ThenInclude(y => y.ProjectMonitoringIndicatorDetail)
                                                     .ThenInclude(z => z.ProjectMonitoringIndicatorQuestions)
                                                     .ThenInclude(i => i.ProjectIndicatorQuestions)
                                                     .Where(v => v.IsDeleted == false && v.ProjectId == request.ProjectId)
                                                     .Select(p => new ProjectActivityReportPdfModel
                                                     {
                                                         ProjectCode = p.ProjectCode,
                                                         ProjectName = p.ProjectName,
                                                         ProjectGoal = p.ProjectOtherDetail.projectGoal,
                                                         ProjectStartDate = p.ProjectOtherDetail.StartDate == null ? "" : p.ProjectOtherDetail.StartDate.Value.ToShortDateString(),
                                                         ProjectEndDate = p.ProjectOtherDetail.EndDate == null ? "" : p.ProjectOtherDetail.EndDate.Value.ToShortDateString(),
                                                         ProjectDuration = p.ProjectOtherDetail == null ? 0 : ((p.ProjectOtherDetail.StartDate == null && p.ProjectOtherDetail.EndDate == null) ? 0 : (p.ProjectOtherDetail.EndDate.Value.Date - p.ProjectOtherDetail.StartDate.Value.Date).TotalDays),
                                                         Country = p.CountryMultiSelectDetails.FirstOrDefault() == null ? null : p.CountryMultiSelectDetails.FirstOrDefault().CountryDetails.CountryName,
                                                         Province = p.ProvinceMultiSelect.Where(e => e.ProjectId == request.ProjectId).Select(e => e.ProvinceDetails.ProvinceName).ToList(),
                                                         District = p.DistrictMultiSelect.Where(e => e.ProjectId == request.ProjectId).Select(e => e.DistrictDetail.District).ToList(),
                                                         LogoPath = _env.WebRootFileProvider.GetFileInfo("ReportLogo/logo.jpg").PhysicalPath,
                                                         ActiivtyListModel = p.ProjectActivityDetail
                                                                                .Where(v => spActivityList.Select(x => x.ActivityId)
                                                                                .Contains(v.ActivityId) && v.IsDeleted == false && v.ParentId == null).OrderBy(x => x.PlannedStartDate)
                                                                                .Select(a => new ActiivtyListModel
                                                                                {
                                                                                    ActivityCode = a.ActivityId,
                                                                                    MainActivity = a.ActivityDescription ,// Note : activity name = description
                                                                                    ActualStartDate = a.ActualStartDate == null ? "" : a.ActualStartDate.Value.ToShortDateString(),
                                                                                    ActualEndDate = a.ActualEndDate == null ? "" : a.ActualEndDate.Value.ToShortDateString(),

                                                                                    ActivityDuration = a.ActualEndDate == null ? 0 : (a.ActualEndDate.Value.Date - a.ActualStartDate.Value.Date).TotalDays,
                                                                                    ProjectMonitoringViewModel = a.ProjectMonitoringReviewDetail
                                                                                        .Where(y => y.IsDeleted == false).OrderByDescending(y => y.CreatedDate)
                                                                                        .Select(x => new ProjectMonitoringViewModel
                                                                                        {
                                                                                            NegativePoints = x.NegativePoints,
                                                                                            PositivePoints = x.PostivePoints,
                                                                                            ProjectId = x.ProjectId,
                                                                                            DisplayMonitoringDate = x.MonitoringDate == null ? "" : x.MonitoringDate.Value.ToShortDateString(),
                                                                                            Recommendations = x.Recommendations,
                                                                                            Remarks = x.Remarks,
                                                                                            ProjectMonitoringReviewId = x.ProjectMonitoringReviewId,
                                                                                            MonitoringReviewModel = x.ProjectMonitoringIndicatorDetail
                                                                                            .Where(y => y.IsDeleted == false)
                                                                                            .Select(y => new ProjectMonitoringReviewModel
                                                                                            {
                                                                                                ProjectIndicatorId = y.ProjectIndicatorId,
                                                                                                MonitoringIndicatorId = y.MonitoringIndicatorId,
                                                                                                IndicatorName = y.ProjectIndicators.IndicatorName,
                                                                                                QuestionTypeId = y.QuestionTypeId,
                                                                                                IndicatorQuestions = y.ProjectMonitoringIndicatorQuestions
                                                                                                                      .Where(z => z.IsDeleted == false)
                                                                                                                      .Select(z => new ProjectMonitoringQuestionModel
                                                                                                                      {
                                                                                                                          MonitoringIndicatorQuestionId = z.Id,
                                                                                                                          IndicatorQuestionId = z.IndicatorQuestionId,
                                                                                                                          QuestionType = z.ProjectIndicatorQuestions.QuestionType,
                                                                                                                          QuestionTypeName = z.ProjectIndicatorQuestions == null ? null
                                                                                                                                                                                : (z.ProjectIndicatorQuestions.QuestionType == (int)QuestionType.Qualitative) ? "Qualitative"
                                                                                                                                                                                                                                                              : z.ProjectIndicatorQuestions == null ? null
                                                                                                                                                                                                                                                                                                    : z.ProjectIndicatorQuestions.QuestionType == (int)QuestionType.Quantitative ? "Quantitative"
                                                                                                                                                                                                                                                                                                                                                                                 : null,
                                                                                                                          Score = z.Score,
                                                                                                                          VerificationSourceId = z.VerificationSourceId,
                                                                                                                          VerificationSourceName = z.ProjectIndicatorQuestions == null ? null
                                                                                                                                                                                       : z.ProjectIndicatorQuestions.VerificationSources == null ? null
                                                                                                                                                                                                                                                 : z.ProjectIndicatorQuestions.VerificationSources
                                                                                                                                                                                                                    .FirstOrDefault(v => v.VerificationSourceId == z.VerificationSourceId).VerificationSourceName
                                                                                                                                                                                                                                                                ?? null,
                                                                                                                          IndicatorQuestion = z.ProjectIndicatorQuestions.IndicatorQuestion,
                                                                                                                          VerificationSources = z.ProjectIndicatorQuestions == null ? null : z.ProjectIndicatorQuestions.VerificationSources.Select(c => new VerificationSources
                                                                                                                          {
                                                                                                                              VerificationSourceId = c.VerificationSourceId,
                                                                                                                              VerificationSourceName = c.VerificationSourceName
                                                                                                                          }).ToList()

                                                                                                                      }).ToList()

                                                                                            }).ToList()
                                                                                        }).ToList()




                                                                                }).ToList()

                                                     }).FirstOrDefaultAsync();



                //  Note : for rating 
                if (activityDetail.ActiivtyListModel.Any())
                {
                    foreach (var item in activityDetail.ActiivtyListModel)
                    {
                        foreach (var i in item.ProjectMonitoringViewModel)
                        {
                            if (i.MonitoringReviewModel.Any())
                            {
                                i.MonitoringReviewModel.ForEach(x => x.TotalScore = x.IndicatorQuestions.Sum(y => y.Score));
                                i.Rating = Math.Round(((float)i.MonitoringReviewModel.Sum(y => y.TotalScore.Value) / (float)i.MonitoringReviewModel.Count), 2);
                            }

                        }
                    }
                }


                return await _pdfExportService.ExportToPdf(activityDetail, "Pages/PdfTemplates/ProjectActivityReport.cshtml");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
