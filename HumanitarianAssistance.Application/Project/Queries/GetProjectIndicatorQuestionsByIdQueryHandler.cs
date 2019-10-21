using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectIndicatorQuestionsByIdQueryHandler : IRequestHandler<GetProjectIndicatorQuestionsByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetProjectIndicatorQuestionsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectIndicatorQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<IndicatorQuestions> projectIndicatorQuestions = await _dbContext.ProjectIndicatorQuestions
                                                                               .Where(x => x.IsDeleted == false && x.ProjectIndicatorId == request.indicatorId)
                                                                               .Select(x => new IndicatorQuestions
                                                                               {
                                                                                   QuestionId = x.IndicatorQuestionId,
                                                                                   QuestionText = x.IndicatorQuestion
                                                                               })
                                                                               .ToListAsync();
                response.data.Questions = projectIndicatorQuestions;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + exception.Message;
            }

            return response;
        }
    }
}
