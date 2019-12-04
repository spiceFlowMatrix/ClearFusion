using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteLeaveTypeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                LeaveReasonDetail leave = new LeaveReasonDetail { LeaveReasonId = request.Id, IsDeleted = true };
                _dbContext.Entry(leave).State = EntityState.Modified;
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