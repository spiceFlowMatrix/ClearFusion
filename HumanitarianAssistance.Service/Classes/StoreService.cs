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
        #region "Variables & Dependencies"
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public StoreService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        #endregion


        #region "Store Inventories"
        // Store Inventories
        public async Task<APIResponse> AddInventory(StoreInventoryModel model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    if (model.InventoryCreditAccount != model.InventoryDebitAccount)
                    {
                        var inventoryAccount =
                           _uow.GetDbContext().ChartAccountDetail.Where(x =>
                              x.AccountCode == model.InventoryCreditAccount || x.AccountCode == model.InventoryDebitAccount).ToList();
                        if (inventoryAccount.Count == 2)
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
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.AccountCantAddToSameAccount;

                    }
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
                     _uow.GetDbContext().ChartAccountDetail.Where(x =>
                        x.AccountCode == model.InventoryCreditAccount || x.AccountCode == model.InventoryDebitAccount).ToList();
                if (edInv != null && inventoryAccount.Count == 2)
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

        public async Task<APIResponse> GetAllInventories(int? AssetType)
        {
            var response = new APIResponse();
            try
            {

                List<StoreInventory> inventoryList = new List<StoreInventory>();

                if (AssetType != null)
                {
                    inventoryList = await Task.Run(() =>
                       _uow.GetDbContext().StoreInventories
                           //.Include(c => c.ChartAccountDetails)
                           //.Include(c => c.CreatedBy)
                           .Where(c => c.IsDeleted == false && c.AssetType == AssetType)
                           .OrderBy(c => c.CreatedDate).ToList());
                }
                else
                {
                    inventoryList = await Task.Run(() =>
                       _uow.GetDbContext().StoreInventories
                           .Where(c => c.IsDeleted == false)
                           .OrderBy(c => c.CreatedDate).ToList());
                }


                var invModelList = inventoryList.Select(v => new StoreInventoryModel
                {
                    InventoryId = v.InventoryId,
                    InventoryCode = v.InventoryCode,
                    InventoryName = v.InventoryName,
                    InventoryDescription = v.InventoryDescription,
                    //InventoryChartOfAccount = v.ChartAccountDetails.ChartOfAccountCode,
                    InventoryCreditAccount = v.InventoryCreditAccount,
                    InventoryDebitAccount = v.InventoryDebitAccount,
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

        #endregion


        #region "Store Items"
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

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
                List<StoreInventoryItem> inventoryItemsList = new List<StoreInventoryItem>();
                if (ItemInventory != null)
                {

                    inventoryItemsList = await Task.Run(() =>
                       _uow.GetDbContext().InventoryItems
                           .Where(c => c.IsDeleted == false && c.ItemInventory == ItemInventory)
                           .OrderByDescending(c => c.CreatedDate).ToListAsync());
                }
                else
                {
                    inventoryItemsList = await Task.Run(() =>
                    _uow.GetDbContext().InventoryItems
                        .Where(c => c.IsDeleted == false)
                        .OrderByDescending(c => c.CreatedDate).ToListAsync());
                }





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

        #endregion


        #region "Store Item Types"

        public async Task<APIResponse> AddInventoryItemsType(InventoryItemTypeModel model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    InventoryItemType obj = _mapper.Map<InventoryItemType>(model);
                    await _uow.InventoryItemTypeRepository.AddAsyn(obj);
                    await _uow.SaveAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Please add item type details";
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

        public async Task<APIResponse> EditInventoryItemsType(InventoryItemTypeModel model)
        {
            var response = new APIResponse();
            try
            {
                var editItemType = await _uow.InventoryItemTypeRepository.FindAsync(x => x.ItemType == model.ItemType);
                if (editItemType != null)
                {
                    _mapper.Map(model, editItemType);
                    await _uow.InventoryItemTypeRepository.UpdateAsyn(editItemType);
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

        public async Task<APIResponse> DeleteInventoryItemsType(InventoryItemTypeModel model)
        {
            var response = new APIResponse();
            try
            {
                var deleteItem = await _uow.InventoryItemTypeRepository.FindAsync(x => x.ItemType == model.ItemType);
                if (deleteItem != null)
                {
                    deleteItem.IsDeleted = true;
                    await _uow.InventoryItemTypeRepository.UpdateAsyn(deleteItem);
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

        public async Task<APIResponse> GetAllInventoryItemsType()
        {
            var response = new APIResponse();
            try
            {
                //var inventoryItemsList = await Task.Run(() =>
                //	_uow.GetDbContext().InventoryItemType
                //		.Where(c => c.IsDeleted == false )
                //		.OrderByDescending(c => c.CreatedDate));

                var inventoryItemsList = await _uow.InventoryItemTypeRepository.FindAllAsync(x => x.IsDeleted == false);

                var invModelList = inventoryItemsList.Select(v => new InventoryItemTypeModel
                {
                    TypeName = v.TypeName,
                    ItemType = v.ItemType
                }).ToList();
                response.data.InventoryItemTypeList = invModelList;
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

        #endregion


        #region "Store Item Purchase"
        // Item purchases

        // TODO: ReadMe
        // When creating purchases towards an item, we must know the item type. Based on that
        // we must create a purchase for that item and also create entries for that object as well
        // E.g, if the itemtype is vehicle and the purchase detail specifies a quantity of 2, then two
        // vehicles must be created in the database.
        // MotorFuel, MotorMaintenance, and MotorSpareParts items in the database must only be created separately when they are ordered, and assigned a vehicle or generator
        // at purchase time, we must always know the item type.
        // Only Vehicles and Generators items are created in the database upon purchase


        public async Task<APIResponse> AddPurchase(ItemPurchaseModel model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    StoreItemPurchase purchase = _mapper.Map<StoreItemPurchase>(model);

                    // For Image 

                    if (model.ImageFileName != null && model.ImageFileName != "")
                    {
                        string[] str = model.ImageFileName.Split(",");
                        byte[] filepath = Convert.FromBase64String(str[1]);
                        string ex = str[0].Split("/")[1].Split(";")[0];
                        string guidname = Guid.NewGuid().ToString();
                        string filename = guidname + "." + ex;
                        var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                        File.WriteAllBytes(@"Documents/" + filename, filepath);

                        purchase.ImageFileName = guidname;
                        purchase.ImageFileType = "." + ex;
                    }
                    else
                    {
                        purchase.ImageFileName = null;
                        purchase.ImageFileType = null;
                    }

                    // For invoice 

                    if (model.Invoice != null && model.Invoice != "")
                    {
                        string[] str = model.Invoice.Split(",");
                        byte[] filepath = Convert.FromBase64String(str[1]);
                        string ex = str[0].Split("/")[1].Split(";")[0];
                        string guidname = Guid.NewGuid().ToString();
                        string filename = guidname + "." + ex;
                        var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                        File.WriteAllBytes(@"Documents/" + filename, filepath);

                        purchase.InvoiceFileName = guidname;
                        purchase.InvoiceFileType = "." + ex;
                    }
                    else
                    {
                        purchase.InvoiceFileName = null;
                        purchase.InvoiceFileType = null;
                    }

                    await _uow.StoreItemPurchaseRepository.AddAsyn(purchase);
                    await _uow.SaveAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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

        public async Task<APIResponse> EditPurchase(ItemPurchaseModel model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var purchaseRecord = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.PurchaseId == model.PurchaseId);
                    if (purchaseRecord != null)
                    {
                        _mapper.Map(model, purchaseRecord);

                        // For Image 

                        if (model.ImageFileName != null && model.ImageFileName != "")
                        {
                            string[] str = model.ImageFileName.Split(",");
                            byte[] filepath = Convert.FromBase64String(str[1]);
                            string ex = str[0].Split("/")[1].Split(";")[0];
                            string guidname = Guid.NewGuid().ToString();
                            string filename = guidname + "." + ex;
                            var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                            File.WriteAllBytes(@"Documents/" + filename, filepath);

                            purchaseRecord.ImageFileName = guidname;
                            purchaseRecord.ImageFileType = "." + ex;
                        }
                        //else
                        //{
                        //    purchaseRecord.ImageFileName = null;
                        //    purchaseRecord.ImageFileType = null;
                        //}

                        // For invoice 

                        if (model.Invoice != null && model.Invoice != "")
                        {
                            string[] str = model.Invoice.Split(",");
                            byte[] filepath = Convert.FromBase64String(str[1]);
                            string ex = str[0].Split("/")[1].Split(";")[0];
                            string guidname = Guid.NewGuid().ToString();
                            string filename = guidname + "." + ex;
                            var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                            File.WriteAllBytes(@"Documents/" + filename, filepath);

                            purchaseRecord.InvoiceFileName = guidname;
                            purchaseRecord.InvoiceFileType = "." + ex;
                        }
                        //else
                        //{
                        //    purchaseRecord.InvoiceFileName = null;
                        //    purchaseRecord.InvoiceFileType = null;
                        //}

                        await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);
                        await _uow.SaveAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";

                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Record cannot be updated";
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

        public async Task<APIResponse> DeletePurchase(ItemPurchaseModel model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var purchaseRecord = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.PurchaseId == model.PurchaseId);
                    if (purchaseRecord != null)
                    {
                        purchaseRecord.IsDeleted = true;
                        await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);
                        await _uow.SaveAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
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

        public async Task<APIResponse> GetAllPurchasesByItem(string itemId)
        {
            var response = new APIResponse();
            try
            {
                var purchases = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.PurchaseOrders).Include(x => x.StoreInventoryItem).Where(x => x.InventoryItem == itemId && x.IsDeleted == false).ToListAsync();
                var purchasesModel = purchases.Select(v => new StoreItemPurchaseViewModel
                {
                    PurchaseId = v.PurchaseId,
                    SerialNo = v.SerialNo,
                    Currency = v.Currency,
                    UnitCost = v.UnitCost,
                    Quantity = v.Quantity,
                    TotalCost = v.UnitCost * v.Quantity,
                    UnitType = v.UnitType,
                    CurrentQuantity = v.Quantity - (v.PurchaseOrders != null ? v.PurchaseOrders.Sum(q => q.IssuedQuantity) : 0),
                    ImageFileName = v.ImageFileName + v.ImageFileType,
                    Invoice = v.InvoiceFileName + v.InvoiceFileType,
                    PurchaseDate = v.PurchaseDate,
                    DeliveryDate = v.DeliveryDate,
                    ApplyDepreciation = v.ApplyDepreciation,
                    DepreciationRate = v.DepreciationRate,
                    PurchasedById = v.PurchasedById,
                    InventoryItem = v.InventoryItem

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

        public async Task<APIResponse> GetSerialNumber(string serialNumber)
        {
            var response = new APIResponse();
            try
            {
                var checkSerialNumber = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.SerialNo == serialNumber);
                if (checkSerialNumber != null)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Serial Number already exits";
                    return response;
                }
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

        #endregion


        #region "Store Item Order"
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

        public async Task<APIResponse> AddItemOrder(ItemOrderModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    StorePurchaseOrder obj = _mapper.Map<StorePurchaseOrder>(model);
                    await _uow.PurchaseOrderRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model value inappropriate";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditItemOrder(ItemOrderModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var recordExits = await _uow.PurchaseOrderRepository.FindAsync(x => x.OrderId == model.OrderId && x.IsDeleted == false);
                    _mapper.Map(model, recordExits);
                    await _uow.PurchaseOrderRepository.UpdateAsyn(recordExits);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record cannot be updated";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }


        public async Task<APIResponse> DeleteItemOrder(ItemOrderModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var recordExits = await _uow.PurchaseOrderRepository.FindAsync(x => x.OrderId == model.OrderId && x.IsDeleted == false);
                    recordExits.IsDeleted = true;
                    await _uow.PurchaseOrderRepository.UpdateAsyn(recordExits);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record cannot be deleted";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllItemsOrder(string ItemId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //var orders = await _uow.PurchaseOrderRepository.FindAllAsync(x => x.InventoryItem == ItemId && x.IsDeleted == false);
                var orders = await _uow.GetDbContext().StorePurchaseOrders.Include(x => x.StoreInventoryItem).Where(x => x.InventoryItem == ItemId && x.IsDeleted == false).ToListAsync();

                var ordersList = orders.Select(x => new ItemOrderModel
                {
                    InventoryItem = x.InventoryItem,
                    IssueDate = x.IssueDate,
                    IssuedQuantity = x.IssuedQuantity,
                    IssuedToEmployeeId = x.IssuedToEmployeeId,
                    MustReturn = x.MustReturn,
                    OrderId = x.OrderId,
                    Purchase = x.Purchase,
                    ReturnedDate = x.ReturnedDate,
                    //InventoryName = x.StoreInventoryItem.Inventory.InventoryName,
                    //InventoryItemName = x.StoreInventoryItem.ItemName,
                }).ToList();

                response.data.ItemOrderModelList = ordersList;
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

        #endregion


        #region "Purchase Unit Type"

        public async Task<APIResponse> AddPurchaseUnitType(PurchaseUnitType model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    //PurchaseUnitType obj = _mapper.Map<PurchaseUnitType>(model);
                    await _uow.PurchaseUnitTypeRepository.AddAsyn(model);
                    await _uow.SaveAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Please add Unit type details";
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

        public async Task<APIResponse> EditPurchaseUnitType(PurchaseUnitType model)
        {
            var response = new APIResponse();
            try
            {
                var editUnitType = await _uow.PurchaseUnitTypeRepository.FindAsync(x => x.UnitTypeId == model.UnitTypeId);
                if (editUnitType != null)
                {
                    //_mapper.Map(model, editUnitType);
                    editUnitType.UnitTypeName = model.UnitTypeName;

                    await _uow.PurchaseUnitTypeRepository.UpdateAsyn(editUnitType);
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

        public async Task<APIResponse> DeletePurchaseUnitType(PurchaseUnitType model)
        {
            var response = new APIResponse();
            try
            {
                var deletePurchaseUnitType = await _uow.PurchaseUnitTypeRepository.FindAsync(x => x.UnitTypeId == model.UnitTypeId);
                if (deletePurchaseUnitType != null)
                {
                    deletePurchaseUnitType.IsDeleted = true;
                    await _uow.PurchaseUnitTypeRepository.UpdateAsyn(deletePurchaseUnitType);
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

        public async Task<APIResponse> GetAllPurchaseUnitType()
        {
            var response = new APIResponse();
            try
            {
                //var inventoryItemsList = await Task.Run(() =>
                //	_uow.GetDbContext().InventoryItemType
                //		.Where(c => c.IsDeleted == false )
                //		.OrderByDescending(c => c.CreatedDate));

                var purchaseUnitTypeList = await _uow.PurchaseUnitTypeRepository.FindAllAsync(x => x.IsDeleted == false);

                var invModelList = purchaseUnitTypeList.Select(v => new PurchaseUnitType
                {
                    UnitTypeName = v.UnitTypeName,
                    UnitTypeId = v.UnitTypeId
                }).ToList();
                response.data.PurchaseUnitTypeList = invModelList;
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

        #endregion


        #region "Item Amount"

        public async Task<APIResponse> GetItemAmounts(string ItemId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //int procuredAmount, spentAmount;
                var procuredAmount = await _uow.GetDbContext().StoreItemPurchases.Where(x => x.InventoryItem == ItemId && x.IsDeleted == false).ToListAsync();
                var spentAmount = await _uow.GetDbContext().StorePurchaseOrders.Where(x => x.InventoryItem == ItemId && x.IsDeleted == false).ToListAsync();


                response.ItemAmount.ProcuredAmount = procuredAmount != null ? procuredAmount.Sum(x => x.Quantity) : 0;
                response.ItemAmount.SpentAmount = spentAmount != null ? spentAmount.Sum(x => x.IssuedQuantity) : 0;
                response.ItemAmount.CurrentAmount = response.ItemAmount.ProcuredAmount - response.ItemAmount.SpentAmount;
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

        #endregion

        #region "Employee Procurement Summary"

        public async Task<APIResponse> GetProcurementSummary(int EmployeeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProcurmentData = await _uow.GetDbContext().StorePurchaseOrders
                    .Include(x => x.StoreItemPurchase).ThenInclude(c => c.CurrencyDetails)
                    .Include(x => x.EmployeeDetail)
                    .Include(x => x.StoreItemPurchase.PurchaseUnitType)
                    .Include(x => x.StoreItemPurchase.StoreInventoryItem).ThenInclude(x => x.Inventory)
                    .Where(x => x.IssuedToEmployeeId == EmployeeId).ToListAsync();

                List<ProcurmentSummaryModel> lst = new List<ProcurmentSummaryModel>();
                if (ProcurmentData != null)
                {
                    foreach (var item in ProcurmentData)
                    {
                        ProcurmentSummaryModel obj = new ProcurmentSummaryModel();
                        obj.ProcurementId = item.OrderId;
                        obj.ProcurementDate = item.IssueDate;
                        obj.EmployeeName = item.EmployeeDetail?.EmployeeName;
                        obj.Store = item.StoreInventoryItem?.Inventory?.AssetType ?? 0;
                        obj.Inventory = item.StoreInventoryItem?.Inventory?.InventoryName ?? null;
                        obj.Item = item.StoreInventoryItem?.ItemName ?? null;
                        obj.TotalCost = (item.StoreItemPurchase?.Quantity ?? 0) * (item.StoreItemPurchase?.UnitCost ?? 0);
                        obj.MustReturn = item.MustReturn == true ? "Yes" : "No";
                        obj.Returned = item.ReturnedDate == null ? "No" : "Yes";
                        obj.TotalCostDetails.UnitType = item.StoreItemPurchase?.PurchaseUnitType?.UnitTypeName ?? null;
                        obj.TotalCostDetails.Amount = item.StoreItemPurchase?.Quantity ?? 0;
                        obj.TotalCostDetails.UnitCost = item.StoreItemPurchase?.UnitCost ?? 0;
                        obj.TotalCostDetails.Currency = item.StoreItemPurchase?.CurrencyDetails?.CurrencyName ?? null;
                        lst.Add(obj);
                    }
                }
                response.data.ProcurmentSummaryModelList = lst;
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

        #endregion



        // Depreciation

        // Must filter on store name, inventory, item, and purchase
        public async Task<APIResponse> GetAllDepreciationByFilter(DepreciationReportFilter depretiationFilter)
        {
            var response = new APIResponse();
            try
            {


                if (depretiationFilter.CurrentDate != null)
                {
                    List<DepreciationReportModel> depreciationList = new List<DepreciationReportModel>();

                    List<StoreItemPurchase> storeItemPurchased = new List<StoreItemPurchase>();


                    //storeItemPurchased = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.StoreInventoryItem).Where(x => x.IsDeleted == false).ToListAsync();




                    if (depretiationFilter.StoreId != null && depretiationFilter.InventoryId != null && depretiationFilter.ItemId != null)
                    {
                        storeItemPurchased = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.StoreInventoryItem).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.StoreInventoryItem.Inventory.AssetType == depretiationFilter.StoreId && x.StoreInventoryItem.ItemInventory == depretiationFilter.InventoryId && x.StoreInventoryItem.ItemId == depretiationFilter.ItemId).ToListAsync();

                    }
                    if (depretiationFilter.StoreId != null && depretiationFilter.InventoryId != null && depretiationFilter.ItemId == null)
                    {
                        storeItemPurchased = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.StoreInventoryItem).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.StoreInventoryItem.Inventory.AssetType == depretiationFilter.StoreId && x.StoreInventoryItem.ItemInventory == depretiationFilter.InventoryId).ToListAsync();

                    }
                    if (depretiationFilter.StoreId != null && depretiationFilter.InventoryId == null && depretiationFilter.ItemId == null)
                    {
                        storeItemPurchased = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.StoreInventoryItem).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true && x.StoreInventoryItem.Inventory.AssetType == depretiationFilter.StoreId).ToListAsync();

                    }
                    if (depretiationFilter.StoreId == null && depretiationFilter.InventoryId == null && depretiationFilter.ItemId == null)
                    {
                        storeItemPurchased = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.StoreInventoryItem).Where(x => x.IsDeleted == false && x.ApplyDepreciation == true).ToListAsync();
                    }
                    foreach (var item in storeItemPurchased)
                    {
                        DepreciationReportModel obj = new DepreciationReportModel();

                        //double hoursSincePurchase;
                        //double depreciationAmount;
                        //double currentValue;

                        obj.ItemName = item.StoreInventoryItem.ItemName;
                        obj.PurchaseId = item.PurchaseId;
                        obj.PurchaseDate = item.PurchaseDate;

                        obj.HoursSincePurchase = depretiationFilter.CurrentDate.Date.Subtract(item.PurchaseDate.Date).TotalHours;
                        obj.DepreciationRate = item.DepreciationRate;
                        obj.DepreciationAmount = (obj.HoursSincePurchase * item.DepreciationRate * item.UnitCost) / 100;
                        obj.CurrentValue = item.UnitCost - obj.DepreciationAmount;

                        obj.PurchasedCost = item.UnitCost;

                        depreciationList.Add(obj);

                    }
                    response.data.DepreciationReportList = depreciationList.ToList();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Please Select Date";
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

        //public async Task<APIResponse> GetAllDepreciationByFilter(DateTime currentDate)
        //{
        //    var response = new APIResponse();
        //    try
        //    {
        //        var depData = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.StoreInventoryItem).Where(x => x.IsDeleted == false).ToListAsync();

        //        List<DepreciationReportModel> depreciationList = new List<DepreciationReportModel>();

        //        foreach (var item in depData)
        //        {
        //            DepreciationReportModel obj = new DepreciationReportModel();

        //            //double hoursSincePurchase;
        //            //double depreciationAmount;
        //            //double currentValue;

        //            obj.ItemName = item.StoreInventoryItem.ItemName;
        //            obj.PurchaseId = item.PurchaseId;
        //            obj.PurchaseDate = item.PurchaseDate;

        //            obj.HoursSincePurchase = currentDate.Date.Subtract(item.PurchaseDate.Date).TotalHours;
        //            obj.DepreciationRate = item.DepreciationRate;
        //            obj.DepreciationAmount = (obj.HoursSincePurchase * item.DepreciationRate * item.UnitCost) / 100;
        //            obj.CurrentValue = item.UnitCost - obj.DepreciationAmount;

        //            obj.PurchasedCost = item.UnitCost;

        //            depreciationList.Add(obj);

        //        }
        //        response.data.DepreciationReportList = depreciationList.ToList();
        //        response.StatusCode = StaticResource.successStatusCode;
        //        response.Message = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //        return response;
        //    }
        //    return response;
        //}
    }
}