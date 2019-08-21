using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteScheduleCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var scheduleInfo = await _dbContext.ScheduleDetails.FirstOrDefaultAsync(c => c.ScheduleId == request.model);
                scheduleInfo.IsDeleted = true;
                scheduleInfo.ModifiedById = request.ModifiedById;
                scheduleInfo.ModifiedDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Schedule Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
