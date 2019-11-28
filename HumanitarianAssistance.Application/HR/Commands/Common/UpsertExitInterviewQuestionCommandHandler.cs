using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Common
{
    public class UpsertExitInterviewQuestionCommandHandler: IRequestHandler<UpsertExitInterviewQuestionCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public UpsertExitInterviewQuestionCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(UpsertExitInterviewQuestionCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                ExitInterviewQuestionsMaster exitQuestions;

                if(request.Id.HasValue)
                {
                   exitQuestions = await _dbContext.ExitInterviewQuestionsMaster
                                                   .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                                        x.Id == request.Id);

                    exitQuestions.ModifiedById = request.ModifiedById;
                    exitQuestions.ModifiedDate = request.ModifiedDate;

                    // check if result exist on request sequenceno
                     var result = await _dbContext.ExitInterviewQuestionsMaster
                                       .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                       x.QuestionType == request.QuestionType && x.SequencePosition == request.SequencePosition);
                    
                    if(result != null)
                    {
                        //get largest sequence no present on question type
                       var largestSequence = await _dbContext.ExitInterviewQuestionsMaster
                                                             .Where(x=> x.IsDeleted == false &&
                                                            x.QuestionType == request.QuestionType).OrderByDescending(x=> x.SequencePosition)
                                                            .FirstOrDefaultAsync(); 

                        result.SequencePosition= ++largestSequence.SequencePosition;
                    }
                }
                else
                {
                    exitQuestions = new ExitInterviewQuestionsMaster();
                    exitQuestions.CreatedById = request.CreatedById;
                    exitQuestions.CreatedDate = request.CreatedDate;
                    _dbContext.ExitInterviewQuestionsMaster.Add(exitQuestions);
                }

                exitQuestions.QuestionText = request.QuestionText;
                exitQuestions.QuestionType = request.QuestionType;
                exitQuestions.SequencePosition =  request.SequencePosition;

                await _dbContext.SaveChangesAsync(); 
                success = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}