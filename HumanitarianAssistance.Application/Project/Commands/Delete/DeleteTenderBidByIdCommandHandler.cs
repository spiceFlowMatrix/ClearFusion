using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteTenderBidByIdCommandHandler: IRequestHandler<DeleteTenderBidByIdCommand, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public DeleteTenderBidByIdCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteTenderBidByIdCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var bid = await _dbContext.TenderBidSubmission.FirstOrDefaultAsync(c => c.IsDeleted == false && c.BidId == request.BidId);
                
                if (bid != null) {
                    bid.IsDeleted = true;
                    bid.ModifiedById = request.ModifiedById;
                    bid.ModifiedDate = DateTime.UtcNow;

                    await _dbContext.SaveChangesAsync();
                }
                
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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