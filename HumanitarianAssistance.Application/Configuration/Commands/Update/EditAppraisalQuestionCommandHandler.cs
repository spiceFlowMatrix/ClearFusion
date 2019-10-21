using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditAppraisalQuestionCommandHandler : IRequestHandler<EditAppraisalQuestionCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditAppraisalQuestionCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditAppraisalQuestionCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var record = await _dbContext.AppraisalGeneralQuestions.FirstOrDefaultAsync(x => x.AppraisalGeneralQuestionsId == request.AppraisalGeneralQuestionsId);
                record.SequenceNo = request.SequenceNo;
                record.Question = request.Question;
                record.DariQuestion = request.DariQuestion;
                record.ModifiedById = request.ModifiedById;
                record.ModifiedDate = DateTime.Now;
                record.OfficeId = request.OfficeId;

                _dbContext.AppraisalGeneralQuestions.Update(record);
                await _dbContext.SaveChangesAsync();
                
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