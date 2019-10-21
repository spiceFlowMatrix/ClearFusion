using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditRatingBasedCriteriaQuestionsCommandHandler : IRequestHandler<EditRatingBasedCriteriaQuestionsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public EditRatingBasedCriteriaQuestionsCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(EditRatingBasedCriteriaQuestionsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var obj = await _dbContext.RatingBasedCriteriaQuestions.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.QuestionsId == request.QuestionsId);

                    obj.Question = request.Question;
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;

                    await _dbContext.SaveChangesAsync();
                }
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
