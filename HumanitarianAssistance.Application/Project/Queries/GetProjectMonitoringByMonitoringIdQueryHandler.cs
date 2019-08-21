using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectMonitoringByMonitoringIdQueryHandler : IRequestHandler<GetProjectMonitoringByMonitoringIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProjectMonitoringByMonitoringIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectMonitoringByMonitoringIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                var monitoring = await _dbContext.ProjectMonitoringReviewDetail
                                                .Include(y => y.ProjectMonitoringIndicatorDetail)
                                                .ThenInclude(x => x.ProjectMonitoringIndicatorQuestions)
                                                .ThenInclude(x => x.ProjectIndicatorQuestions)
                                                .Include(x => x.ProjectMonitoringIndicatorDetail)
                                                .ThenInclude(y => y.ProjectIndicators)
                                                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectMonitoringReviewId == request.Id);
                
                ProjectMonitoringViewModel obj = new ProjectMonitoringViewModel();

                obj.ActivityId = monitoring.ActivityId;
                obj.MonitoringDate = monitoring.MonitoringDate;
                obj.NegativePoints = monitoring.NegativePoints;
                obj.PositivePoints = monitoring.PostivePoints;
                obj.ProjectId = monitoring.ProjectId;
                obj.ProjectMonitoringReviewId = monitoring.ProjectMonitoringReviewId;
                obj.Recommendations = monitoring.Recommendations;
                obj.Remarks = monitoring.Remarks;

                if (monitoring.ProjectMonitoringIndicatorDetail.Any())
                {
                    foreach (var item in monitoring.ProjectMonitoringIndicatorDetail)
                    {
                        ProjectMonitoringReviewModel model = new ProjectMonitoringReviewModel();
                        model.IndicatorName = item.ProjectIndicators.IndicatorName;
                        model.ProjectIndicatorId = item.ProjectIndicatorId;
                        model.MonitoringIndicatorId = item.MonitoringIndicatorId;

                        if (item.ProjectMonitoringIndicatorQuestions.Any())
                        {
                            foreach (var question in item.ProjectMonitoringIndicatorQuestions)
                            {
                                ProjectMonitoringQuestionModel questions = new ProjectMonitoringQuestionModel
                                {
                                    MonitoringIndicatorQuestionId = question.Id,
                                    Question = question.ProjectIndicatorQuestions.IndicatorQuestion,
                                    QuestionId = question.QuestionId,
                                    Score = question.Score,
                                    Verification = question.Verification,
                                    VerificationId = question.VerificationId
                                };
                                model.IndicatorQuestions.Add(questions);
                            }
                        }
                        obj.MonitoringReviewModel.Add(model);
                    }
                }

                response.data.ProjectMonitoringModel = obj;
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
