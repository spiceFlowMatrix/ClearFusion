using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddInterviewTechnicalQuestionsCommandHandler : IRequestHandler<AddInterviewTechnicalQuestionsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        
        public AddInterviewTechnicalQuestionsCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(AddInterviewTechnicalQuestionsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    InterviewTechnicalQuestions obj = new InterviewTechnicalQuestions {
                        Question = request.Question,
                        OfficeId = request.OfficeId,
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false
                    };

                    await _dbContext.InterviewTechnicalQuestions.AddAsync(obj);
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
