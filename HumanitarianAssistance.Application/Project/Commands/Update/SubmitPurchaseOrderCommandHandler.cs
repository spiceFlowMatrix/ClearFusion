using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class SubmitPurchaseOrderCommandHandler : IRequestHandler<SubmitPurchaseOrderCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public SubmitPurchaseOrderCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(SubmitPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var _logisticReq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.LogisticRequestsId == request.RequestId);
                if (_logisticReq != null) {
                    _logisticReq.PurchaseDate = request.PurchaseDate;
                    _logisticReq.ModifiedDate = request.ModifiedDate;
                    _logisticReq.ModifiedById = request.ModifiedById;
                    _logisticReq.Status = (int)LogisticRequestStatus.CompletePurchase;
                }
                var _reqItems = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted == false && request.ItemModel.Select(y=>y.Id).Contains(x.LogisticItemId)).ToListAsync();
                if(_reqItems.Any()) {
                    foreach(var items in _reqItems) {
                        var updateItem = request.ItemModel.FirstOrDefault(x=>x.Id == items.LogisticItemId);
                        if(updateItem!=null) {
                            items.FinalCost = updateItem.FinalCost;
                            items.PurchaseSubmitted = true;
                        }
                    }
                }
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
