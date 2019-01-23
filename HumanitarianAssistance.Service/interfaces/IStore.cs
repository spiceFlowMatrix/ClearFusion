using DataAccess.DbEntities.Store;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces
{
    public interface IStore
    {
        Task<APIResponse> AddInventory(StoreInventoryModel model);
        Task<APIResponse> EditInventory(StoreInventoryModel model);
        Task<APIResponse> DeleteInventory(StoreInventoryModel model);
        Task<APIResponse> GetAllInventories(int? AssetType);
        Task<APIResponse> AddItemPurchaseDocument(StoreItemPurchaseDocumentModel model);
        //Task<APIResponse> GetAllDepreciationByFilter();

        Task<APIResponse> AddInventoryItems(StoreInventoryItemModel model);
        Task<APIResponse> EditInventoryItems(StoreInventoryItemModel model);
        Task<APIResponse> DeleteInventoryItems(StoreInventoryItemModel model);
        Task<APIResponse> GetAllInventoryItems(string ItemInventory);

        Task<APIResponse> AddInventoryItemsType(InventoryItemTypeModel model);
        Task<APIResponse> EditInventoryItemsType(InventoryItemTypeModel model);
        Task<APIResponse> DeleteInventoryItemsType(InventoryItemTypeModel model);
        Task<APIResponse> GetAllInventoryItemsType();

        Task<APIResponse> AddPurchase(ItemPurchaseModel model);
        Task<APIResponse> EditPurchase(ItemPurchaseModel model);
        Task<APIResponse> DeletePurchase(ItemPurchaseModel model);
        Task<APIResponse> GetAllPurchasesByItem(string itemId);
        Task<APIResponse> GetSerialNumber(string serialNumber);


        Task<APIResponse> AddPurchaseUnitType(PurchaseUnitType model);
        Task<APIResponse> EditPurchaseUnitType(PurchaseUnitType model);
        Task<APIResponse> DeletePurchaseUnitType(PurchaseUnitType model);
        Task<APIResponse> GetAllPurchaseUnitType();



        Task<APIResponse> AddItemOrder(ItemOrderModel model);
        Task<APIResponse> EditItemOrder(ItemOrderModel model);
        Task<APIResponse> DeleteItemOrder(ItemOrderModel model);
        Task<APIResponse> GetAllItemsOrder(string ItemId);


        Task<APIResponse> GetItemAmounts(string ItemId);
        Task<APIResponse> GetProcurementSummary(int EmployeeId, int CurrencyId);
        Task<APIResponse> GetAllDepreciationByFilter(DepreciationReportFilter depretiationFilter);

        Task<APIResponse> UpdateInvoice(UpdatePurchaseInvoiceModel model, string UserId);
        Task<APIResponse> GetAllPurchaseInvoices(string PurchaseId);

        Task<APIResponse> UpdatePurchaseImage(UpdatePurchaseInvoiceModel model, string UserId);

        Task<APIResponse> AddItemSpecificationsDetails(List<ItemSpecificationDetailModel> model, string UserId);
        Task<APIResponse> EditItemSpecificationsDetails(ItemSpecificationDetailModel model, string UserId);
        Task<APIResponse> GetAllItemSpecificationsDetails(string ItemId, int ItemTypeId, int OfficeId);
        Task<APIResponse> AddItemSpecificationsMaster(ItemSpecificationMasterModel model, string UserId);
        Task<APIResponse> EditItemSpecificationsMaster(ItemSpecificationMasterModel model, string UserId);
        Task<APIResponse> GetItemSpecificationsMaster(int ItemTypeId, int OfficeId);

        Task<APIResponse> GetAllReceiptType();
        Task<APIResponse> GetAllStatusAtTimeOfIssue();
        Task<APIResponse> GetInventoryCode(int Id);
        Task<APIResponse> GetInventoryItemCode(string InventoryId, int TypeId);
        Task<APIResponse> GetAllStoreSourceType();
        Task<APIResponse> GetAllStoreSourceCode(int? typeId);
        Task<APIResponse> AddStoreSourceCode(StoreSourceCodeDetailModel storeSourceCodeDetailModel, string userId);
        Task<APIResponse> GetStoreTypeCode(int CodeTypeId);
        Task<APIResponse> EditStoreSourceCode(StoreSourceCodeDetailModel storeSourceCodeDetailModel, string userId);
        Task<APIResponse> DeleteStoreSourceCode(int storeSourceCodeId, string userId);
        Task<APIResponse> GetAllPaymentTypes();
        Task<APIResponse> AddPaymentTypes(PaymentTypes model, string UserId);
        Task<APIResponse> EditPaymentTypes(PaymentTypes model, string UserId);
        Task<APIResponse> DeletePaymentTypes(int PaymentId, string UserId);
        Task<APIResponse> VerifyPurchase(ItemPurchaseModel model);
        Task<APIResponse> UnverifyPurchase(ItemPurchaseModel model);
        Task<APIResponse> AddStoreItemGroup(StoreItemGroupModel storeGroupItem, string userId);
        Task<APIResponse> GetStoreGroupItemCode(string inventoryId);
        Task<APIResponse> EditStoreItemGroup(StoreItemGroupModel storeGroupItem, string userId);
    }
}
