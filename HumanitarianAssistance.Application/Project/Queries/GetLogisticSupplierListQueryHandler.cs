using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetLogisticSupplierListQueryHandler : IRequestHandler<GetLogisticSupplierListQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private readonly IFileManagementService _fileManagement;

            public GetLogisticSupplierListQueryHandler(HumanitarianAssistanceDbContext dbContext,IFileManagementService fileManagement)
            {
                _dbContext = dbContext;
                _fileManagement = fileManagement;
            }
            public async Task<ApiResponse> Handle(GetLogisticSupplierListQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var supplierList = await _dbContext.ProjectLogisticSuppliers
                                                               .Where(x => x.IsDeleted == false && x.LogisticRequestsId == request.requestId)
                                                               .Include(x=>x.InventoryItems)
                                                               .Include(x=>x.StoreSourceCodeDetail)
                                                               .Include(x=>x.ProjectLogisticRequests)     
                                                               .Select(x => new LogisticSupplierViewModel
                                                                {
                                                                   SupplierId = x.SupplierId,
                                                                   SourceCodeId = x.StoreSourceCodeId,
                                                                   SourceCode = x.StoreSourceCodeDetail.Code,
                                                                   StoreSourceDescription = x.StoreSourceCodeDetail.Description,
                                                                   Quantity = x.Quantity,
                                                                   FinalUnitPrice = x.FinalUnitPrice,
                                                                   ItemId = x.ItemId,
                                                                   ItemName = x.InventoryItems.ItemName,
                                                                   ItemCode = x.InventoryItems.ItemCode,
                                                                   CurrencyCode = x.ProjectLogisticRequests.CurrencyDetails.CurrencyCode
                                                                })
                                                                .ToListAsync();

                var invoiceDocument = await (from es in _dbContext.EntitySourceDocumentDetails 
                                    join df in _dbContext.DocumentFileDetail on es.DocumentFileId equals df.DocumentFileId
                                    into docs
                                    from doc in docs.DefaultIfEmpty()
                                    join u in _dbContext.UserDetails on doc.CreatedById equals u.AspNetUserId
                                    into finaldocs
                                    from final in finaldocs.DefaultIfEmpty()
                                    where doc.IsDeleted == false && supplierList.Select(x=>x.SupplierId).Contains(es.EntityId) &&
                                    doc.PageId == (int)FileSourceEntityTypes.LogisticSupplierInvoice
                                    select new StoreDocumentModel 
                                    {
                                        DocumentFileId= doc.DocumentFileId,
                                        UploadedBy= $"{final.FirstName} {final.LastName}",
                                        EntityId = es.EntityId
                                    }).ToListAsync();

                var warrantyDocument = await (from es in _dbContext.EntitySourceDocumentDetails 
                                    join df in _dbContext.DocumentFileDetail on es.DocumentFileId equals df.DocumentFileId
                                    into docs
                                    from doc in docs.DefaultIfEmpty()
                                    join u in _dbContext.UserDetails on doc.CreatedById equals u.AspNetUserId
                                    into finaldocs
                                    from final in finaldocs.DefaultIfEmpty()
                                    where doc.IsDeleted == false && supplierList.Select(x=>x.SupplierId).Contains(es.EntityId) &&
                                    doc.PageId == (int)FileSourceEntityTypes.LogisticSupplierWarranty
                                    select new StoreDocumentModel 
                                    {
                                        DocumentFileId= doc.DocumentFileId,
                                        UploadedBy= $"{final.FirstName} {final.LastName}",
                                        EntityId = es.EntityId
                                    }).ToListAsync();

                foreach (var doc in invoiceDocument)
                {
                    FileModel fileModel = new FileModel()
                    {
                        PageId = (int)FileSourceEntityTypes.LogisticSupplierInvoice,
                        RecordId = doc.EntityId,
                        DocumentFileId= doc.DocumentFileId
                    };

                    var supplier = supplierList.FirstOrDefault(x=>x.SupplierId == fileModel.RecordId);
                    if(supplier != null) {
                        var documentModel = await _fileManagement.GetFilesByDocumentFileId(fileModel);
                        supplier.InvoiceUrl = documentModel.SignedURL;
                        supplier.InvoiceName = documentModel.DocumentName;
                    }
                }

                foreach (var doc in warrantyDocument)
                {
                    FileModel fileModel = new FileModel()
                    {
                        PageId = (int)FileSourceEntityTypes.LogisticSupplierWarranty,
                        RecordId = doc.EntityId,
                        DocumentFileId= doc.DocumentFileId
                    };

                    var supplier = supplierList.FirstOrDefault(x=>x.SupplierId == fileModel.RecordId);
                    if(supplier != null) {
                        var documentModel = await _fileManagement.GetFilesByDocumentFileId(fileModel);
                        supplier.WarrantyUrl = documentModel.SignedURL;
                        supplier.WarrantyName = documentModel.DocumentName;
                    }
                }

                response.data.LogisticsSupplierList = supplierList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
