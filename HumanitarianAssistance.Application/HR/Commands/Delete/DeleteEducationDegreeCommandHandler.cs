using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEducationDegreeCommandHandler : IRequestHandler<DeleteEducationDegreeCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public DeleteEducationDegreeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteEducationDegreeCommand request, CancellationToken cancellationToken)
        {
           bool success = false;

           try
           {
               EducationDegreeMaster questionMaster = new EducationDegreeMaster { Id = request.Id, IsDeleted = true };
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