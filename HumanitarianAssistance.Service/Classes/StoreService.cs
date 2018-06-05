using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbEntities.Store;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Store;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Service.Classes
{
	public class StoreService : IStore
	{
		IUnitOfWork _uow;
		IMapper _mapper;
		UserManager<AppUser> _userManager;
		public StoreService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
		{
			this._uow = uow;
			this._mapper = mapper;
			this._userManager = userManager;
		}

		// Store Inventories
		public async Task<APIResponse> AddInventory(StoreInventoryModel model)
		{
			var response = new APIResponse();
			try
			{
				var inventoryAccount =
					await _uow.ChartAccountDetailRepository.FindAsync(x =>
						x.AccountCode == model.InventoryAccount);
				if (inventoryAccount != null)
				{
					StoreInventory inventory = _mapper.Map<StoreInventory>(model);
					await _uow.StoreInventoryRepository.AddAsyn(inventory);
					await _uow.SaveAsync();
					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = StaticResource.AccountNoteNotExists;
				}
			}
			catch (Exception e)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + e.Message;
				return response;
			}

			return response;
		}

		public async Task<APIResponse> EditInventory(StoreInventoryModel model)
		{
			var response = new APIResponse();
			try
			{
				var edInv = await _uow.StoreInventoryRepository.FindAsync(c => c.InventoryId == model.InventoryId);
				var inventoryAccount =
					await _uow.ChartAccountDetailRepository.FindAsync(x =>
						x.AccountCode == model.InventoryAccount);
				if (edInv != null && inventoryAccount != null)
				{
					//edInv.InventoryCode = model.InventoryCode;
					//edInv.InventoryName = model.InventoryName;
					//edInv.InventoryDescription = model.InventoryDescription;
					//edInv.InventoryChartOfAccount = inventoryAccount.ChartOfAccountCode;
					//edInv.AssetType = model.AssetType;

					_mapper.Map(model, edInv);

					await _uow.StoreInventoryRepository.UpdateAsyn(edInv);
					await _uow.SaveAsync();

					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = StaticResource.SomethingWrong;
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

		public async Task<APIResponse> DeleteInventory(StoreInventoryModel model)
		{
			var response = new APIResponse();
			try
			{
				var toDeleteInv = await Task.Run(() =>
					_uow.GetDbContext().StoreInventories
						.Include(x => x.InventoryItems).FirstOrDefault(i => i.InventoryId == model.InventoryId));
				if (toDeleteInv != null)
				{
					if (toDeleteInv.InventoryItems.Count == 0)
					{
						toDeleteInv.IsDeleted = true;
						toDeleteInv.ModifiedById = model.ModifiedById;
						toDeleteInv.ModifiedDate = model.ModifiedDate;

						await _uow.StoreInventoryRepository.UpdateAsyn(toDeleteInv);
						await _uow.SaveAsync();

						response.StatusCode = StaticResource.successStatusCode;
						response.Message = "Success";
					}
					else
					{
						response.StatusCode = StaticResource.IdAlreadyUsedInOtherTable;
						response.Message = "This inventory is being used for items.";
					}
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = StaticResource.SomethingWrong;
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

		public async Task<APIResponse> GetAllInventories()
		{
			var response = new APIResponse();
			try
			{
				var inventoryList = await Task.Run(() =>
					_uow.GetDbContext().StoreInventories
						//.Include(c => c.ChartAccountDetails)
						//.Include(c => c.CreatedBy)
						.Where(c => c.IsDeleted == false)
						.OrderBy(c => c.CreatedDate));

				var invModelList = inventoryList.Select(v => new StoreInventoryModel
				{
					InventoryId = v.InventoryId,
					InventoryCode = v.InventoryCode,
					InventoryName = v.InventoryName,
					InventoryDescription = v.InventoryDescription,
					//InventoryChartOfAccount = v.ChartAccountDetails.ChartOfAccountCode,
					InventoryAccount = v.InventoryAccount,
					AssetType = v.AssetType
				}).ToList();
				response.data.InventoryList = invModelList;
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
				return response;
			}

			return response;
		}

		// Inventory Items

		public async Task<APIResponse> AddInventoryItems(StoreInventoryItemModel model)
		{
			var response = new APIResponse();
			try
			{
				var addItem = await _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == model.Voucher);
				if (addItem != null)
				{
					StoreInventoryItem obj = _mapper.Map<StoreInventoryItem>(model);					
					await _uow.StoreInventoryItemRepository.AddAsyn(obj);
					await _uow.SaveAsync();
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = "Please add voucher first";
					return response;
				}
			}
			catch (Exception e)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + e.Message;
				return response;
			}

			return response;
		}

		public async Task<APIResponse> EditInventoryItems(StoreInventoryItemModel model)
		{
			var response = new APIResponse();
			try
			{
				var editItem = await _uow.StoreInventoryItemRepository.FindAsync(x => x.ItemId == model.ItemId);
				if (editItem != null)
				{					
					_mapper.Map(model, editItem);
					await _uow.StoreInventoryItemRepository.UpdateAsyn(editItem);
					await _uow.SaveAsync();

					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = StaticResource.SomethingWrong;
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

		public async Task<APIResponse> DeleteInventoryItems(StoreInventoryItemModel model)
		{
			var response = new APIResponse();
			try
			{
				var deleteItem = await _uow.StoreInventoryItemRepository.FindAsync(x => x.ItemId == model.ItemId);
				if (deleteItem != null)
				{
					deleteItem.IsDeleted = true;					
					await _uow.StoreInventoryItemRepository.UpdateAsyn(deleteItem);
					await _uow.SaveAsync();

					response.StatusCode = StaticResource.successStatusCode;
					response.Message = "Success";
				}
				else
				{
					response.StatusCode = StaticResource.failStatusCode;
					response.Message = StaticResource.SomethingWrong;
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

		public async Task<APIResponse> GetAllInventoryItems(string ItemInventory)
		{
			var response = new APIResponse();
			try
			{
				var inventoryItemsList = await Task.Run(() =>
					_uow.GetDbContext().InventoryItems						
						.Where(c => c.IsDeleted == false && c.ItemInventory == ItemInventory)
						.OrderByDescending(c => c.CreatedDate));

				var invModelList = inventoryItemsList.Select(v => new StoreInventoryItemModel
				{
					ItemId = v.ItemId,
					ItemInventory = v.ItemInventory,
					ItemName = v.ItemName,
					ItemCode = v.ItemCode,
					Description = v.Description,
					Voucher = v.Voucher,
					ItemType = v.ItemType
				}).ToList();
				response.data.InventoryItemList = invModelList;
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
				return response;
			}

			return response;
		}


		// Item purchases

		// TODO: ReadMe
		// When creating purchases towards an item, we must know the item type. Based on that
		// we must create a purchase for that item and also create entries for that object as well
		// E.g, if the itemtype is vehicle and the purchase detail specifies a quantity of 2, then two
		// vehicles must be created in the database.
		// MotorFuel, MotorMaintenance, and MotorSpareParts items in the database must only be created separately when they are ordered, and assigned a vehicle or generator
		// at purchase time, we must always know the item type.
		// Only Vehicles and Generators items are created in the database upon purchase


		//public async Task<APIResponse> AddPurchase(ItemPurchaseWithDataModel model)
		//{
		//	var response = new APIResponse();
		//	try
		//	{
		//		StoreItemPurchase purchase = _mapper.Map<StoreItemPurchase>(model.Purchase);

		//	}
		//	catch (Exception ex)
		//	{
		//		response.StatusCode = StaticResource.failStatusCode;
		//		response.Message = StaticResource.SomethingWrong + ex.Message;
		//		return response;
		//	}

		//	return response;
		//}

		public async Task<APIResponse> GetAllPurchasesByItem(string itemId)
		{
			var response = new APIResponse();
			try
			{
				var purchases = await Task.Run(() =>
					_uow.GetDbContext().StoreItemPurchases
						.Include(c => c.PurchaseOrders)
						.Include(c => c.PurchaseUnitType)
						.Include(e => e.EmployeeDetail)
						.Include(t => t.StoreInventoryItem.ItemTypes) // TODO: Make sure that these includes actually populate the required data
						.OrderBy(c => c.CreatedDate));
				var purchasesModel = purchases.Select(v => new StoreItemPurchaseViewModel
				{
					CreatedDate = v.CreatedDate,
					CreatedById = v.CreatedById,
					PurchaseDate = v.PurchaseDate,
					DeliveryDate = v.DeliveryDate,
					UnitType = v.PurchaseUnitType.UnitTypeName,
					Currency = v.CurrencyDetails.CurrencyName,
					UnitCost = v.UnitCost,
					Quantity = v.Quantity,
					TotalCost = v.UnitCost * v.Quantity,
					ApplyDepreciation = v.ApplyDepreciation,
					CurrentQuantity = v.Quantity - v.PurchaseOrders.Sum(q => q.IssuedQuantity),
					PurchasedBy = v.EmployeeDetail.EmployeeName,
					ImageFileName = v.ImageFileName,
					ItemType = v.StoreInventoryItem.ItemTypes.TypeName // TODO: Please test if this will return the item type object
				}).ToList();
				response.data.StoreItemsPurchaseViewList = purchasesModel;
				response.StatusCode = StaticResource.successStatusCode;
				response.Message = "Success";
			}
			catch (Exception ex)
			{
				response.StatusCode = StaticResource.failStatusCode;
				response.Message = StaticResource.SomethingWrong + ex.Message;
				return response;
			}
			return response;
		}

		// Purchase orders

		// TODO: ReadMe
		// When ordering MotorFuel item, a MotorFuel object must be created and then assigned to
		// a vehicle. Fuel purchase orders must not be returnable. If the purchase order is deleted, then so must the MotorFuel item
		// When ordering a MotorMaintenance item, a MotorMaintenance object must be generated in the database
		// and assigned to a vehicle
		// When ordering a MotorSpareParts item, a MotorSpareParts object must be generated in the database and 
		// assigned to a vehicle
		// For the above, the relevant details for each object must be updated both in the newly created
		// object and in the PurchaseOrder that they are related to.




		// Item Purchase Documents
		public async Task<APIResponse> AddItemPurchaseDocument(StoreItemPurchaseDocumentModel model)
		{
			var response = new APIResponse();
			try
			{
				// Save the file
				string[] str = model.File.Split(",");
				byte[] file = Convert.FromBase64String(str[1]);

				string ex = str[0].Split("/")[1].Split(";")[0];
				string guidName = Guid.NewGuid().ToString();

				string fileName = "purchaseDoc." + guidName + "." + model.DocumentName + "." + ex;
				File.WriteAllBytes(@"Documents\" + fileName, file);

				// Add database entry for document
				ItemPurchaseDocument doc = new ItemPurchaseDocument();
				doc.CreatedById = model.CreatedById;
				doc.CreatedDate = model.CreatedDate;
				doc.DocumentGuid = guidName;
				doc.DocumentName = model.DocumentName;
				doc.File = null;
				doc.FileType = ex;
				doc.FileName = fileName;
				doc.Purchase = model.PurchaseId;


				await _uow.ItemPurchaseDocumentRepository.AddAsyn(doc);
				await _uow.SaveAsync();

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

		// Depreciation

		// Must filter on store name, inventory, item, and purchase
		//public async Task<APIResponse> GetAllDepreciationByFilter()
		//{
		//	var response = new APIResponse();
		//	return response;
		//}
	}
}