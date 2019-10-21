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
    public class EditInterviewTechnicalQuestionsCommandHandler : IRequestHandler<EditInterviewTechnicalQuestionsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public EditInterviewTechnicalQuestionsCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(EditInterviewTechnicalQuestionsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var obj = await _dbContext.InterviewTechnicalQuestions.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.InterviewTechnicalQuestionsId == request.InterviewTechnicalQuestionsId);
                    
                    obj.Question = request.Question;
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    obj.IsDeleted = false;

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
