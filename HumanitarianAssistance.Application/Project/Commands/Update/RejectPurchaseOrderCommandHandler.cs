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
        public class RejectPurchaseOrderCommandHandler : IRequestHandler<RejectPurchaseOrderCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public RejectPurchaseOrderCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(RejectPurchaseOrderCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
                {
                    var logisticreq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.LogisticRequestsId == request.requestId);
                    if(logisticreq == null) {
                        throw new Exception("Request doesn't Exist!");
                    }
                    logisticreq.Status = (int)LogisticRequestStatus.IssuePurchaseOrder;
                    logisticreq.ModifiedById = request.ModifiedById;
                    logisticreq.ModifiedDate = request.ModifiedDate;

                    var logisticItem = await _dbContext.ProjectLogisticItems.Where(x=>x.IsDeleted == false && x.LogisticRequestsId == request.requestId).ToListAsync();
                    foreach(var item in logisticItem) {
                        item.PurchaseSubmitted = false;
                    }

                    var documents = await _dbContext.EntitySourceDocumentDetails
                    .Include(x=>x.DocumentFileDetail)
                    .Where(x=>x.IsDeleted == false && x.EntityId == request.requestId && x.DocumentFileDetail.PageId == (int)FileSourceEntityTypes.ProjectLogisticPurchase).ToListAsync();
                    foreach(var doc in documents) {
                        doc.IsDeleted = true;
                        doc.DocumentFileDetail.IsDeleted = true;
                    }

                    var goodsDocuments = await _dbContext.EntitySourceDocumentDetails
                    .Include(x=>x.DocumentFileDetail)
                    .Where(x=>x.IsDeleted == false && x.EntityId == request.requestId && x.DocumentFileDetail.PageId == (int)FileSourceEntityTypes.GoodsRecievedDocument).ToListAsync();
                    foreach(var doc in goodsDocuments) {
                        doc.IsDeleted = true;
                        doc.DocumentFileDetail.IsDeleted = true;
                    }

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
