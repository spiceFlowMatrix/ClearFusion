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
    public class DeletePolicyTimeScheduleCommandHandler : IRequestHandler<DeletePolicyTimeScheduleCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeletePolicyTimeScheduleCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(DeletePolicyTimeScheduleCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var policyInfo = await _dbContext.PolicyTimeSchedules.FirstOrDefaultAsync(c => c.Id == request.Id);
                policyInfo.IsDeleted = true;
                policyInfo.ModifiedById = request.ModifiedById;
                policyInfo.ModifiedDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Policy Schedule Deleted Successfully";
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
