using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeBonusFineSalaryHeadCommandHandler: IRequestHandler<DeleteEmployeeBonusFineSalaryHeadCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public DeleteEmployeeBonusFineSalaryHeadCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteEmployeeBonusFineSalaryHeadCommand request, CancellationToken cancellationToken)
        {
            bool success = false; 

            try
            {
                EmployeeBonusFineSalaryHead obj = await _dbContext.EmployeeBonusFineSalaryHead
                                                                  .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.Id == request.Id);

                if(obj == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                obj.IsDeleted = true;
                obj.ModifiedById = request.ModifiedById;
                obj.ModifiedDate = DateTime.UtcNow;

                _dbContext.EmployeeBonusFineSalaryHead.Update(obj);
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