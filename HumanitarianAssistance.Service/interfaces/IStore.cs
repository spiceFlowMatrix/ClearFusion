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
		Task<APIResponse> EditInventory(StoreInventoryViewModel model);
		Task<APIResponse> DeleteInventory(StoreInventoryModel model);
		Task<APIResponse> GetAllInventories();
		//Task<APIResponse> AddPurchase(ItemPurchaseWithDataModel model);
		Task<APIResponse> GetAllPurchasesByItem(string itemId);
		Task<APIResponse> AddItemPurchaseDocument(StoreItemPurchaseDocumentModel model);
		//Task<APIResponse> GetAllDepreciationByFilter();
	}
}
