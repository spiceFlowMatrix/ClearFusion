using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteJobGradeDetailCommandHandler : IRequestHandler<DeleteJobGradeDetailCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public DeleteJobGradeDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteJobGradeDetailCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                JobGrade questionMaster = new JobGrade { GradeId  = request.Id, IsDeleted = true };
                _dbContext.Entry(questionMaster).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                success = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return success;
        }
    }
}