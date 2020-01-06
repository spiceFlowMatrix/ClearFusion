using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetSelectedBidDetailQueryHandler: IRequestHandler<GetSelectedBidDetailQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        
        public GetSelectedBidDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(GetSelectedBidDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var bid = await _dbContext.TenderBidSubmission.FirstOrDefaultAsync(x =>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId && x.IsBidSelected == true);
                
                if(bid == null) {
                    throw new Exception("No Bid Selected for the Request!");
                }
                var user = await _dbContext.UserDetails.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.AspNetUserId == bid.ModifiedById);
                SelectedBidDetailModel obj = new SelectedBidDetailModel{
                    ContactName = bid.Name,
                    SelectedBy = user.FirstName + ' ' + user.LastName
                };

                response.data.SelectedBidDetail = obj;
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