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

namespace HumanitarianAssistance.Application.Project.Queries
{

        public class GetProjectIndicatorDetailByIdQueryHandler : IRequestHandler<GetProjectIndicatorDetailByIdQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetProjectIndicatorDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetProjectIndicatorDetailByIdQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            ProjectIndicatorModel projectIndicators = new ProjectIndicatorModel();

            try
            {
                if (request.indicatorId == 0)
                {
                    throw new Exception("Project Indicator Id Cannot be 0");
                }

                var indicators = await _dbContext.ProjectIndicators.Include(x => x.ProjectIndicatorQuestions)
                                                .Select(x => new EditIndicatorModel
                                                {
                                                    IndicatorCode = x.IndicatorCode,
                                                    IndicatorName = x.IndicatorName,
                                                    IndicatorId = x.ProjectIndicatorId,
                                                    IsDeleted = x.IsDeleted,
                                                    IndicatorQuestions = x.ProjectIndicatorQuestions.Select(y => new IndicatorQuestions
                                                    {
                                                        QuestionId = y.IndicatorQuestionId,
                                                        QuestionText = y.IndicatorQuestion,
                                                        IsDeleted = y.IsDeleted
                                                    }).Where(y => y.IsDeleted == false).ToList()
                                                })
                                               .FirstOrDefaultAsync(x => x.IsDeleted == false && x.IndicatorId == request.indicatorId);

                if (indicators != null)
                {
                    response.data.IndicatorModel = indicators;
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
