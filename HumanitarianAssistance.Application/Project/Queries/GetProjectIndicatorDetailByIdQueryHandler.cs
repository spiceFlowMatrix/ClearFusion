using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Queries
{

        public class GetProjectIndicatorDetailByIdQueryHandler : IRequestHandler<GetIndicatorQuestionDetailByIdQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetProjectIndicatorDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetIndicatorQuestionDetailByIdQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            ProjectIndicatorModel projectIndicators = new ProjectIndicatorModel();

            try
            {
                if (request.indicatorId == 0)
                {
                    throw new Exception("Project Indicator Id Cannot be 0");
                }

                var indicators = await _dbContext.ProjectIndicatorQuestions
                                                .Include(x => x.VerificationSources)
                                                .Where(x => x.IsDeleted == false && x.ProjectIndicatorId == request.indicatorId)
                                                .Select(x => new ProjectIndicatorQuestionsModel
                                                {
                                                    IndicatorQuestionId = x.IndicatorQuestionId,
                                                    IndicatorQuestion = x.IndicatorQuestion,
                                                    QuestionType = x.QuestionType,
                                                    QuestionTypeName= x.QuestionType == (int)QuestionType.Qualitative ? "Qualitative" : x.QuestionType == (int)QuestionType.Quantitative ? "Quantitative": null,
                                                    ProjectIndicatorId =x.ProjectIndicatorId,
                                                    IsDeleted = x.IsDeleted,
                                                    VerificationSources = x.VerificationSources.Select(y => new VerificationSourcesModel
                                                    {
                                                        VerificationSourceId = y.VerificationSourceId,
                                                        VerificationSourceName= y.VerificationSourceName,
                                                        IsDeleted = y.IsDeleted
                                                    }).Where(y => y.IsDeleted == false).ToList()
                                                })
                                               .ToListAsync();

                if (indicators != null)
                {
                    response.ResponseData = indicators;
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }

            return response;
        }
    }
}
