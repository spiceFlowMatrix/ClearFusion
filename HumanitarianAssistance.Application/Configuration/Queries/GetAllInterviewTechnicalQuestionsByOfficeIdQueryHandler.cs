using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllInterviewTechnicalQuestionsByOfficeIdQueryHandler : IRequestHandler<GetAllInterviewTechnicalQuestionsByOfficeIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllInterviewTechnicalQuestionsByOfficeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllInterviewTechnicalQuestionsByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                response.data.InterviewTechnicalQuestionsList = await (from i in _dbContext.InterviewTechnicalQuestions.AsNoTracking()
                                                                       where i.OfficeId == request.OfficeId
                                                                       select new InterviewTechnicalQuestionsModel
                                                                       {
                                                                           OfficeId=i.OfficeId,
                                                                           InterviewTechnicalQuestionsId=i.InterviewTechnicalQuestionsId,
                                                                           Question=i.Question
                                                                       }).ToListAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
