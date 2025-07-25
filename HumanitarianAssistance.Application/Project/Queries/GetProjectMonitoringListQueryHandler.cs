﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectMonitoringListQueryHandler : IRequestHandler<GetProjectMonitoringListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProjectMonitoringListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectMonitoringListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var projectMonitoring = await _dbContext.ProjectMonitoringReviewDetail
                                                                .Include(y => y.ProjectMonitoringIndicatorDetail)
                                                                .ThenInclude(z => z.ProjectMonitoringIndicatorQuestions)
                                                                .ThenInclude(x => x.ProjectIndicatorQuestions)
                                                                .Where(x => x.IsDeleted == false && x.ActivityId == request.activityId)
                                                                .OrderByDescending(x => x.CreatedDate)
                                                                .Select(x => new ProjectMonitoringViewModel
                                                                {
                                                                    ActivityId = x.ActivityId,
                                                                    NegativePoints = x.NegativePoints,
                                                                    PositivePoints = x.PostivePoints,
                                                                    ProjectId = x.ProjectId,
                                                                    MonitoringDate = x.MonitoringDate,
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
                                                                                                                          QuestionTypeName= z.ProjectIndicatorQuestions == null ? null
                                                                                                                                                                                : (z.ProjectIndicatorQuestions.QuestionType == (int)QuestionType.Qualitative) ? "Qualitative" 
                                                                                                                                                                                                                                                              : z.ProjectIndicatorQuestions == null ? null
                                                                                                                                                                                                                                                                                                    : z.ProjectIndicatorQuestions.QuestionType == (int)QuestionType.Quantitative ? "Quantitative"
                                                                                                                                                                                                                                                                                                                                                                                 : null,
                                                                                                                          Score = z.Score,
                                                                                                                          VerificationSourceId = z.VerificationSourceId,
                                                                                                                          VerificationSourceName = z.ProjectIndicatorQuestions == null ? null
                                                                                                                                                                                       : z.ProjectIndicatorQuestions.VerificationSources == null ? null 
                                                                                                                                                                                                                                                 :  z.ProjectIndicatorQuestions.VerificationSources 
                                                                                                                                                                                                                    .FirstOrDefault( v=> v.VerificationSourceId == z.VerificationSourceId).VerificationSourceName 
                                                                                                                                                                                                                                                                ?? null,
                                                                                                                          IndicatorQuestion = z.ProjectIndicatorQuestions.IndicatorQuestion,
                                                                                                                          VerificationSources = z.ProjectIndicatorQuestions == null ? null : z.ProjectIndicatorQuestions.VerificationSources.Select(c=> new VerificationSources {
                                                                                                                              VerificationSourceId= c.VerificationSourceId,
                                                                                                                              VerificationSourceName= c.VerificationSourceName
                                                                                                                          }).ToList()

                                                                                                                      }).ToList()

                                                                                            }).ToList()
                                                                }).ToListAsync();

                if (projectMonitoring.Any())
                {
                    foreach (var item in projectMonitoring)
                    {
                        if (item.MonitoringReviewModel.Any())
                        {
                            item.MonitoringReviewModel.ForEach(x => x.TotalScore = x.IndicatorQuestions.Sum(y => y.Score));
                            item.Rating = Math.Round(((float)item.MonitoringReviewModel.Sum(y => y.TotalScore.Value) / (float)item.MonitoringReviewModel.Count), 2);
                        }
                    }
                }

                response.data.ProjectMonitoring = projectMonitoring;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
