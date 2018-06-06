using HumanitarianAssistance.Service.APIResponses;
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
        //Task<APIResponse> AddPurchase(ItemPurchaseWithDataModel model);
        Task<APIResponse> GetAllPurchasesByItem(string itemId);
		Task<APIResponse> GetAllInventories();
		//Task<APIResponse> AddPurchase(ItemPurchaseWithDataModel model);
		//Task<APIResponse> GetAllPurchasesByItem(string itemId);
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
	}
}
