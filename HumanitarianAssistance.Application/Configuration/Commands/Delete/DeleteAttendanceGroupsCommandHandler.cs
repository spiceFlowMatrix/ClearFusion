using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteAttendanceGroupsCommandHandler : IRequestHandler<DeleteAttendanceGroupsCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public DeleteAttendanceGroupsCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteAttendanceGroupsCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                AttendanceGroupMaster obj = new AttendanceGroupMaster { AttendanceGroupId = request.Id, IsDeleted = true };
                _dbContext.Entry(obj).State = EntityState.Modified;
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