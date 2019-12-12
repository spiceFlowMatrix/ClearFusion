using System;
using HumanitarianAssistance.Application.Store.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCompletedPurchaseOrderDetailQueryHandler: IRequestHandler<GetCompletedPurchaseOrderDetailQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;

        public GetCompletedPurchaseOrderDetailQueryHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement)
        {
            _dbContext= dbContext;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle(GetCompletedPurchaseOrderDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            CompletedPurchaseOrderDetailModel model = new CompletedPurchaseOrderDetailModel();
            List<PurchasedItemModel> purchasedItem = new List<PurchasedItemModel>();
            var createdById = "";
            try
            {
                var _logisticReq =await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.LogisticRequestsId == request.requestId);
                if(_logisticReq == null) {
                    throw new Exception("Request doesnot exists!");
                }
                var voucherDetail = await _dbContext.VoucherDetail.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.VoucherNo == _logisticReq.VoucherNo);
                if(voucherDetail == null) {
                    throw new Exception("Voucher doesnot exists!");
                }
                var purchaseItems = await _dbContext.StoreItemPurchases
                .Include(x=>x.StoreInventoryItem)
                .Where(x=>x.IsDeleted==false && _logisticReq.PurchaseId.ToList().Contains(x.PurchaseId)).ToListAsync();
                foreach (var item in purchaseItems)
                {
                    PurchasedItemModel obj = new PurchasedItemModel{
                        PurchaseId = item.PurchaseId,
                        ItemId = item.InventoryItem,
                        ItemName = item.StoreInventoryItem.ItemName
                    };
                    purchasedItem.Add(obj);
                    createdById = item.CreatedById;
                }
                model.VoucherReferenceNo = voucherDetail.ReferenceNo;
                model.ApprovedBy = await _dbContext.UserDetails.Where(x=>x.IsDeleted==false && x.AspNetUserId == createdById).Select(x=>x.FirstName).FirstOrDefaultAsync();
                model.purchasedItems = purchasedItem;

                response.data.PurchaseOrderDetail = model;
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