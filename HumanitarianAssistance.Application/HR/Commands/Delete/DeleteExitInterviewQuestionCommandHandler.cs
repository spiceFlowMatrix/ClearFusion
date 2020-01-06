using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteExitInterviewQuestionCommandHandler : IRequestHandler<DeleteExitInterviewQuestionCommand, object>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteExitInterviewQuestionCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteExitInterviewQuestionCommand request, CancellationToken cancellationToken)
        {
           bool success = false;

           try
           {
               ExitInterviewQuestionsMaster questionMaster = new ExitInterviewQuestionsMaster { Id = request.Id, IsDeleted = true };
                _dbContext.Entry(questionMaster).State = EntityState.Modified;
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