using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteMurtipleEmployeesByIdCommandHandler : IRequestHandler<DeleteMurtipleEmployeesByIdCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteMurtipleEmployeesByIdCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteMurtipleEmployeesByIdCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                var employees = await _dbContext.EmployeeDetail.Where(x=>x.IsDeleted == false && request.EmpIds.Contains(x.EmployeeID)).ToListAsync();
                
                foreach(var employee in employees) {
                    employee.IsDeleted = true;
                    employee.ModifiedById = request.ModifiedById;
                    employee.ModifiedDate = request.ModifiedDate;
                }
                await _dbContext.SaveChangesAsync();
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }
    }
}