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
    public class GetAllRatingBasedCriteriaQuestionsByOfficeIdQueryHandler : IRequestHandler<GetAllRatingBasedCriteriaQuestionsByOfficeIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllRatingBasedCriteriaQuestionsByOfficeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllRatingBasedCriteriaQuestionsByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                response.data.RatingBasedCriteriaQuestionList = await (from r in _dbContext.RatingBasedCriteriaQuestions.AsNoTracking()
                                                                       where r.OfficeId == request.OfficeId && r.IsDeleted==false
                                                                       select new RatingBasedCriteriaQuestionsModel
                                                                       {
                                                                           OfficeId = r.OfficeId,
                                                                           QuestionsId = r.QuestionsId,
                                                                           Question = r.Question
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
