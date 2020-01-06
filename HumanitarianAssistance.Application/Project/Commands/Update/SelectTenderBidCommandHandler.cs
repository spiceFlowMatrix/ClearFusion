using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class SelectTenderBidCommandHandler : IRequestHandler<SelectTenderBidCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public SelectTenderBidCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(SelectTenderBidCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var _tenderBid = await _dbContext.TenderBidSubmission.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.BidId == request.BidId);
                if(_tenderBid == null) {
                    throw new Exception("Bid doesn't Exist!");
                }
                _tenderBid.IsBidSelected = true;
                _tenderBid.ModifiedById = request.ModifiedById;
                _tenderBid.ModifiedDate = request.ModifiedDate;

                var _logisticReq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.LogisticRequestsId == _tenderBid.LogisticRequestsId);
                if(_logisticReq == null) {
                    throw new Exception("Request doesn't Exist!");
                }
                _logisticReq.TenderStatus = (int)LogisticTenderStatus.BidSelected;
                _logisticReq.Status = (int)LogisticRequestStatus.IssuePurchaseOrder;
                
                await _dbContext.SaveChangesAsync();
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
