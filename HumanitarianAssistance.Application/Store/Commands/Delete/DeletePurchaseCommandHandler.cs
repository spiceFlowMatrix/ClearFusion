using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeletePurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var purchaseRecord = await _dbContext.StoreItemPurchases.FirstOrDefaultAsync(x => x.PurchaseId == request.PurchaseId);
                    if (purchaseRecord != null)
                    {
                        var isOrderExist = _dbContext.StorePurchaseOrders.Where(x => x.Purchase == request.PurchaseId && x.IsDeleted == false).Count();

                        if (isOrderExist > 0)
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.DeleteProcurementsFirst;
                            return response;
                        }
                        else
                        {
                            purchaseRecord.IsDeleted = true;
                            purchaseRecord.ModifiedById = request.ModifiedById;
                            purchaseRecord.ModifiedDate = request.ModifiedDate;
                            await _dbContext.SaveChangesAsync();
                            //await _uow.SaveAsync();

                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Success";
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Record cannot be deleted";
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model values are inappropriate";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }
            return response;
        }
    }
}
