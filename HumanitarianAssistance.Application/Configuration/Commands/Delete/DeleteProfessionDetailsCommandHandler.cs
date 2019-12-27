using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteProfessionDetailsCommandHandler: IRequestHandler<DeleteProfessionDetailsCommand, object>
    {
    private HumanitarianAssistanceDbContext _dbContext;
        public DeleteProfessionDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }
         public async Task<object> Handle(DeleteProfessionDetailsCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                ProfessionDetails questionMaster = new ProfessionDetails { ProfessionId = request.Id, IsDeleted = true };
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