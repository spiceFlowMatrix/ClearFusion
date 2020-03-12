using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Application.FileManagement.Models;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetStorePurchaseByIdQueryHandler: IRequestHandler<GetStorePurchaseByIdQuery, StorePurchaseModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        IFileManagementService _fileManagement;

        public GetStorePurchaseByIdQueryHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement)
        {
            _dbContext = dbContext;
           _fileManagement = fileManagement;
        }

        public async Task<StorePurchaseModel> Handle(GetStorePurchaseByIdQuery request, CancellationToken cancellationToken)
        {

            StorePurchaseModel model= new StorePurchaseModel();

            try
            {
                int orderValue= 0;

                // Get Purchase Document name and Id
                // var documentsAsync = _dbContext.DocumentFileDetail
                //                           .Include(x=> x.EntitySourceDocumentDetail)
                //                           .Where(x=> x.IsDeleted == false && x.PageId == (int)FileSourceEntityTypes.StorePurchase &&
                //                                 x.EntitySourceDocumentDetail.EntityId == request.PurchaseId)
                //                           .Select(x=> new StoreDocumentModel {
                //                              DocumentFileId = x.DocumentFileId,
                //                              DocumentName= x.Name,
                //                              DocumentTypeId= x.DocumentTypeId
                //                           }).ToListAsync();


                var documentsAsync = (from es in _dbContext.EntitySourceDocumentDetails 
                                join df in _dbContext.DocumentFileDetail on es.DocumentFileId equals df.DocumentFileId
                                into docs
                                from doc in docs.DefaultIfEmpty()
                                join u in _dbContext.UserDetails on doc.CreatedById equals u.AspNetUserId
                                into finaldocs
                                from final in finaldocs.DefaultIfEmpty()
                                where doc.IsDeleted == false && es.EntityId == request.PurchaseId &&
                                doc.PageId == (int)FileSourceEntityTypes.StorePurchase
                                select new StoreDocumentModel 
                                {
                                    DocumentFileId= doc.DocumentFileId,
                                    DocumentName= doc.Name,
                                    DocumentTypeId= doc.DocumentTypeId,
                                    UploadedBy= $"{final.FirstName} {final.LastName}",
                                    UploadedOn= doc.CreatedDate,
                                    DocumentTypeName= doc.DocumentTypeId== (int)DocumentFileTypes.PurchaseImage ? "Image" : "Invoice"
                                }).ToListAsync();

                // Get Purchase Data
                model = await _dbContext.StoreItemPurchases
                                  .Include(x=> x.StoreInventoryItem)
                                  .ThenInclude(x=> x.Inventory)
                                  .Include(x=> x.StoreInventoryItem)
                                  .ThenInclude(x=> x.StoreItemGroup)
                                  .Include(x=> x.PurchasedVehicleDetailList)
                                  .Include(x=> x.PurchasedGeneratorDetailList)
                                  .Include(x=> x.GeneratorItemDetail)
                                  .Include(x=> x.VehicleItemDetail)
                                  .Where(x=> x.IsDeleted == false  && x.PurchaseId == request.PurchaseId)
                                   .Select(z=> new StorePurchaseModel 
                                   {
                                       InventoryTypeId= z.StoreInventoryItem.Inventory.AssetType,
                                       InventoryId = z.StoreInventoryItem.ItemInventory,
                                       InventoryMasterName = z.StoreInventoryItem.StoreItemGroup.StoreInventory.InventoryCode + "-" + z.StoreInventoryItem.StoreItemGroup.StoreInventory.InventoryName,
                                       ItemGroupId = z.StoreInventoryItem.ItemGroupId,
                                       ItemGroupName = z.StoreInventoryItem.StoreItemGroup.ItemGroupCode + "-" + z.StoreInventoryItem.StoreItemGroup.ItemGroupName,
                                       ItemId = z.InventoryItem,
                                       ItemName = z.StoreInventoryItem.ItemCode + "-" + z.StoreInventoryItem.ItemName,
                                       ProjectName = z.ProjectDetail.ProjectCode + "-" + z.ProjectDetail.ProjectName,
                                       BudgetLineName = z.ProjectBudgetLineDetail.BudgetCode + "-" +z.ProjectBudgetLineDetail.BudgetName,
                                       ReceivedFromLocationName = z.StoreSourceCodeDetail.Code + "-" + z.StoreSourceCodeDetail.Description,
                                       ReceivedFromEmployeeName = z.EmployeeDetail.EmployeeCode + "-" + z.EmployeeDetail.EmployeeName,
                                       PurchaseId = z.PurchaseId,
                                       SerialNo = z.SerialNo,
                                       InventoryItem= z.InventoryItem,
                                       PurchaseDate= z.PurchaseDate,
                                       Currency= z.Currency,
                                       UnitType= z.UnitType,
                                       UnitCost= z.UnitCost,
                                       Quantity= z.Quantity,
                                       ApplyDepreciation= z.ApplyDepreciation,
                                       DepreciationRate= z.DepreciationRate,
                                       ImageFileName= null,
                                       InvoiceFileName= null,
                                       PurchasedById= z.PurchasedById,
                                       PurchaseOrderNo= int.TryParse(z.SerialNo, out orderValue) ? orderValue : 0,
                                       AssetTypeId= z.AssetTypeId,
                                       InvoiceNo= z.InvoiceNo,
                                       InvoiceDate= z.InvoiceDate,
                                       Status= z.Status,
                                       ReceiptTypeId= z.ReceiptTypeId,
                                       ReceivedFromLocation= z.ReceivedFromLocation,
                                       DeliveryDate= z.DeliveryDate,
                                       ProjectId= z.ProjectId,
                                       BudgetLineId= z.BudgetLineId,
                                       PaymentTypeId= z.PaymentTypeId,
                                       OfficeId= z.OfficeId,
                                       JournalCode= z.JournalCode,
                                       PurchaseName= z.PurchaseName,
                                       TransportItemTypeCategory= z.StoreInventoryItem.ItemTypeCategory,
                                       ItemGroupTransportCategory= z.StoreInventoryItem.StoreItemGroup.ItemTypeCategory,
                                       TransportItemId= z.GeneratorItemDetail != null ? z.GeneratorItemDetail.GeneratorPurchaseId :
                                                        z.VehicleItemDetail != null ? z.VehicleItemDetail.VehiclePurchaseId : 0,
                                       PurchasedVehicleList = z.PurchasedVehicleDetailList.Where(x=> x.IsDeleted == false)
                                                               .Select(y=> new PurchasedVehicleModel 
                                                               {
                                                                   Id= y.Id,
                                                                   PlateNo= y.PlateNo,
                                                                   EmployeeId= y.EmployeeId,
                                                                   StartingMileage= y.StartingMileage,
                                                                   IncurredMileage= y.IncurredMileage,
                                                                   FuelConsumptionRate= y.FuelConsumptionRate,
                                                                   MobilOilConsumptionRate= y.MobilOilConsumptionRate,
                                                                   OfficeId= y.OfficeId,
                                                                   ModelYear= y.ModelYear,
                                                                   PurchaseId= y.PurchaseId,
                                                                   ChasisNo= y.ChasisNo,
                                                                   EngineNo= y.EngineNo,
                                                                   ManufacturerCountry= y.ManufacturerCountry,
                                                                   RegistrationNo= y.RegistrationNo,
                                                                   Remarks= y.PersonRemarks
                                                               }).ToList(),
                                    PurchasedGeneratorList = z.PurchasedGeneratorDetailList.Where(x=> x.IsDeleted== false)
                                                              .Select(y=> new PurchasedGeneratorModel 
                                                               {
                                                                   Id= y.Id,
                                                                   Voltage= y.Voltage,
                                                                   StartingUsage= y.StartingUsage,
                                                                   IncurredUsage= y.IncurredUsage,
                                                                   FuelConsumptionRate= y.FuelConsumptionRate,
                                                                   MobilOilConsumptionRate= y.MobilOilConsumptionRate,
                                                                   OfficeId= y.OfficeId,
                                                                   ModelYear= y.ModelYear,
                                                                   PurchaseId= y.PurchaseId,
                                                                   ChasisNo= y.ChasisNo,
                                                                   EngineNo= y.EngineNo,
                                                                   ManufacturerCountry= y.ManufacturerCountry,
                                                                   RegistrationNo= y.RegistrationNo,
                                                                   Remarks= y.PersonRemarks,
                                                                   EmployeeId= y.EmployeeID
                                                               }).ToList()
                                   }).FirstOrDefaultAsync();

                model.StoreDocumentList = await documentsAsync;

                foreach(var item in model.StoreDocumentList)
                {
                    FileModel fileModel = new FileModel()
                    {
                        PageId = (int)FileSourceEntityTypes.StorePurchase,
                        RecordId = request.PurchaseId,
                        DocumentFileId= item.DocumentFileId
                    };

                     //get Saved Document ID and Signed URL For Purchase Image
                     var documentModel = await _fileManagement.GetFilesByDocumentFileId(fileModel);

                    if (documentModel != null)
                    {
                        item.SignedURL = documentModel.SignedURL;
                        item.DocumentFileId = documentModel.DocumentFileId;
                    }
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }

            return model;
        }
    }
}