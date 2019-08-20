using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveAdvanceCommandHandler : IRequestHandler<ApproveAdvanceCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public ApproveAdvanceCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<ApiResponse> Handle(ApproveAdvanceCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var record = await _dbContext.Advances.FirstOrDefaultAsync(x => x.AdvancesId == request.AdvancesId && x.IsApproved == false && x.IsDeleted == false);
                
                if (record != null)
                {
                    record.IsApproved = true;
                    record.ModifiedById = request.ModifiedById;
                    record.ModifiedDate = DateTime.Now;
                    record.AppraisalApprovedDate = DateTime.Now;
                    _dbContext.Advances.Update(record);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Approval Failed";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}