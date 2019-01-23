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
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.Common.Enums;

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
                           _uow.GetDbContext().ChartOfAccountNew.Where(x => x.ChartOfAccountNewId == model.InventoryDebitAccount).ToList();

                        //if (inventoryAccount.Count == 2)
                        //{
                        bool inventoryCode = await _uow.GetDbContext().StoreInventories.AnyAsync(x => x.InventoryCode == model.InventoryCode);

                        if (!inventoryCode)
                        {
                            StoreInventory inventory = _mapper.Map<StoreInventory>(model);
                            inventory.IsDeleted = false;

                            await _uow.StoreInventoryRepository.AddAsyn(inventory);

                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Success";
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.InventoryCodeAlreadyExists;
                        }
                        //}
                        //else
                        //{
                        //    response.StatusCode = StaticResource.failStatusCode;
                        //    response.Message = StaticResource.AccountNoteNotExists;
                        //}
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
                     _uow.GetDbContext().ChartOfAccountNew.Where(x =>
                                        x.ChartOfAccountNewId == model.InventoryDebitAccount).ToList();
                if (edInv != null)
                {
                    //edInv.InventoryCode = model.InventoryCode;
                    //edInv.InventoryName = model.InventoryName;
                    //edInv.InventoryDescription = model.InventoryDescription;
                    //edInv.InventoryChartOfAccount = inventoryAccount.ChartOfAccountCode;
                    //edInv.AssetType = model.AssetType;

                    bool inventoryCode = await _uow.GetDbContext().StoreInventories.AnyAsync(x => x.InventoryCode == model.InventoryCode && x.InventoryId != model.InventoryId);

                    if (!inventoryCode)
                    {
                        _mapper.Map(model, edInv);

                        edInv.IsDeleted = false;

                        await _uow.StoreInventoryRepository.UpdateAsyn(edInv);

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.InventoryCodeAlreadyExists;
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
                           .Where(c => c.IsDeleted == false && c.AssetType == AssetType)
                           .OrderByDescending(c => c.CreatedDate).ToList());
                }
                else
                {
                    inventoryList = await Task.Run(() =>
                       _uow.GetDbContext().StoreInventories
                           .Where(c => c.IsDeleted == false)
                           .OrderByDescending(c => c.CreatedDate).ToList());
                }

                List<StoreInventoryModel> invModelList = inventoryList.Select(v => new StoreInventoryModel
                {
                    InventoryId = v.InventoryId,
                    InventoryCode = v.InventoryCode,
                    InventoryName = v.InventoryName,
                    InventoryDescription = v.InventoryDescription,
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
                //var addItem = await _uow.VoucherDetailRepository.FindAsync(x => x.VoucherNo == model.Voucher);
                //if (addItem != null)
                //{
                StoreInventoryItem obj = _mapper.Map<StoreInventoryItem>(model);
                obj.IsDeleted = false;

                await _uow.StoreInventoryItemRepository.AddAsyn(obj);

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                //}
                //else
                //{
                //    response.StatusCode = StaticResource.failStatusCode;
                //    response.Message = "Please add voucher first";
                //    return response;
                //}
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
                    editItem.IsDeleted = false;

                    await _uow.StoreInventoryItemRepository.UpdateAsyn(editItem);

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
                    //Voucher = v.Voucher,
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
                    obj.IsDeleted = false;

                    await _uow.InventoryItemTypeRepository.AddAsyn(obj);

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
                    editItemType.IsDeleted = false;

                    await _uow.InventoryItemTypeRepository.UpdateAsyn(editItemType);

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

                    if (model.InvoiceFileName != null && model.InvoiceFileName != "")
                    {
                        string[] str = model.InvoiceFileName.Split(",");
                        byte[] filepath = Convert.FromBase64String(str[1]);
                        string ex = str[0].Split("/")[1].Split(";")[0];
                        if (ex == "plain")
                            ex = "txt";
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

                    purchase.IsDeleted = false;

                    await _uow.StoreItemPurchaseRepository.AddAsyn(purchase);
                    //await _uow.SaveAsync();

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

                        if (!string.IsNullOrEmpty(model.ImageFileName))
                        {
                            if (model.ImageFileName.Contains(","))
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
                        }

                        if (model.InvoiceFileName != null && model.InvoiceFileName != "")
                        {
                            if (model.InvoiceFileName.Contains(","))
                            {
                                string[] str = model.InvoiceFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.InvoiceFileName = guidname;
                                purchaseRecord.InvoiceFileType = "." + ex;
                            }
                        }

                        purchaseRecord.IsDeleted = false;
                        await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);

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
                        var isOrderExist = _uow.GetDbContext().StorePurchaseOrders.Where(x => x.Purchase == model.PurchaseId && x.IsDeleted == false).Count();

                        if (isOrderExist > 0)
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.DeleteProcurementsFirst;
                            return response;
                        }
                        else
                        {
                            purchaseRecord.IsDeleted = true;
                            await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);
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

        public async Task<APIResponse> GetAllPurchasesByItem(string itemId)
        {
            var response = new APIResponse();
            try
            {
                var purchases = await _uow.GetDbContext().StoreItemPurchases.Include(x => x.PurchaseOrders)
                                                                            .Include(x => x.StoreInventoryItem)
                                                                            .Where(x => x.InventoryItem == itemId && x.IsDeleted == false).ToListAsync();

                var officeDetails = await _uow.GetDbContext().OfficeDetail.ToListAsync();

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
                    InventoryItem = v.InventoryItem,
                    //Newly added fields
                    VoucherId = v.VoucherId,
                    VoucherDate = v.VoucherDate,
                    AssetTypeId = v.AssetTypeId,
                    InvoiceNo = v.InvoiceNo,
                    InvoiceDate = v.InvoiceDate,
                    Status = v.Status,
                    ReceiptTypeId = v.ReceiptTypeId,
                    ReceivedFromLocation = v.ReceivedFromLocation,
                    ProjectId = v.ProjectId,
                    BudgetLineId = v.BudgetLineId,
                    PaymentTypeId = v.PaymentTypeId,
                    IsPurchaseVerified = v.IsPurchaseVerified,
                    VerifiedPurchaseVoucher = v.VerifiedPurchaseVoucher,
                    JournalCode = v.JournalCode,
                    VerifiedPurchaseVoucherReferenceNo = v.VerifiedPurchaseVoucher != null ? officeDetails.FirstOrDefault(x => x.IsDeleted == false && x.OfficeId == v.OfficeId).OfficeCode + "-" + v.VerifiedPurchaseVoucher : null
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
                doc.IsDeleted = false;

                await _uow.ItemPurchaseDocumentRepository.AddAsyn(doc);
                //await _uow.SaveAsync();

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

                    obj.IsDeleted = false;

                    await _uow.PurchaseOrderRepository.AddAsyn(obj);
                    //await _uow.SaveAsync();
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

                    recordExits.IsDeleted = false;

                    await _uow.PurchaseOrderRepository.UpdateAsyn(recordExits);
                    //await _uow.SaveAsync();
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
                    //await _uow.SaveAsync();
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

                //var user = await _userManager.
                //var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);




                var ordersList = orders.Select(x => new ItemOrderModel
                {
                    InventoryItem = x.InventoryItem,
                    IssueDate = x.IssueDate,
                    IssuedQuantity = x.IssuedQuantity,
                    IssuedToEmployeeId = x.IssuedToEmployeeId,
                    MustReturn = x.MustReturn,
                    Returned = x.Returned,
                    OrderId = x.OrderId,
                    Purchase = x.Purchase,
                    ReturnedDate = x.ReturnedDate,
                    IssedToLocation = x.IssedToLocation,
                    IssueVoucherNo = x.IssueVoucherNo.ToString(),
                    Project = x.Project,
                    Remarks = x.Remarks,
                    StatusAtTimeOfIssue = x.StatusAtTimeOfIssue
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


        #region "Unit Type"
        public async Task<APIResponse> AddPurchaseUnitType(PurchaseUnitType model)
        {
            var response = new APIResponse();
            try
            {
                if (model != null)
                {
                    PurchaseUnitType obj = _mapper.Map<PurchaseUnitType>(model);
                    obj.IsDeleted = false;

                    await _uow.PurchaseUnitTypeRepository.AddAsyn(model);

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
                    editUnitType.IsDeleted = false;

                    await _uow.PurchaseUnitTypeRepository.UpdateAsyn(editUnitType);

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

                //NOTE: x.MustReturn == false --> Use to keep track if Employee Returned the Item or not.
                var spentAmount = await _uow.GetDbContext().StorePurchaseOrders.Where(x => x.InventoryItem == ItemId && x.IsDeleted == false && x.Returned == false).ToListAsync();


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

        /// <summary>
        /// Getting consolidated/single procurement summary details for employee 
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="CurrencyId"></param>
        /// <returns>Procurement summary list</returns>
        public async Task<APIResponse> GetProcurementSummary(int EmployeeId, int CurrencyId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CurrencyDetails currencyDetails = _uow.GetDbContext().CurrencyDetails.FirstOrDefault(x => x.IsDeleted == false && x.CurrencyId == CurrencyId);

                List<StorePurchaseOrder> ProcurmentData = new List<StorePurchaseOrder>();

                ProcurmentData = await _uow.GetDbContext().StorePurchaseOrders
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
                        obj.TotalCost = (item.IssuedQuantity) * (item.StoreItemPurchase?.UnitCost ?? 0);
                        obj.MustReturn = item.MustReturn == true ? "Yes" : "No";
                        obj.Returned = item.Returned == true ? "Yes" : "No";
                        obj.TotalCostDetails.UnitType = item.StoreItemPurchase?.PurchaseUnitType?.UnitTypeName ?? null;
                        obj.TotalCostDetails.Amount = item.IssuedQuantity; //item.StoreItemPurchase?.Quantity ?? 0;
                        obj.TotalCostDetails.UnitCost = item.StoreItemPurchase?.UnitCost ?? 0;
                        obj.TotalCostDetails.Currency = item.StoreItemPurchase?.CurrencyDetails?.CurrencyName ?? null;
                        obj.VoucherDate = item.StoreItemPurchase.VoucherDate;
                        obj.VoucherNo = item.StoreItemPurchase.VoucherId;
                        obj.CurrencyId = item.StoreItemPurchase.Currency;
                        lst.Add(obj);
                    }

                    if (CurrencyId != 0)
                    {
                        //if procurement summary contains all items of currency selected in the drop down
                        bool isProcurementCurrencySame = lst.Any(x => x.CurrencyId == CurrencyId);

                        if (!isProcurementCurrencySame)
                        {
                            var dates = lst.Select(y => y.VoucherDate.Date).ToList();

                            List<ExchangeRateDetail> exchangeRateDetail = _uow.GetDbContext().ExchangeRateDetail.Where(x => x.IsDeleted == false && x.ToCurrency == CurrencyId && dates.Contains(x.Date.Date)).ToList();

                            //If Exchange rate is available on voucher date
                            if (exchangeRateDetail.Any())
                            {
                                lst.ForEach(x => x.TotalCostDetails.UnitCost *= exchangeRateDetail.FirstOrDefault(y => y.Date.Date == x.VoucherDate.Date && y.FromCurrency == x.CurrencyId && y.ToCurrency == CurrencyId).Rate);
                                lst.ForEach(x => x.TotalCost = x.TotalCostDetails.Amount * x.TotalCostDetails.UnitCost);
                                lst.ForEach(x => x.TotalCostDetails.Currency = currencyDetails.CurrencyName);
                            }
                            else //If Exchange rate is not available on voucher date then take the latest exchange rate updated previously from voucher date
                            {
                                foreach (var obj in lst)
                                {
                                    var exchangeRate = await _uow.GetDbContext().ExchangeRateDetail.OrderByDescending(x => x.Date).FirstOrDefaultAsync(x => x.IsDeleted == false && x.FromCurrency == obj.CurrencyId && x.ToCurrency == CurrencyId && x.Date.Date <= obj.VoucherDate.Date);
                                    if (exchangeRate != null)
                                    {
                                        obj.TotalCostDetails.UnitCost *= exchangeRate.Rate;
                                        obj.TotalCost = obj.TotalCostDetails.Amount * obj.TotalCostDetails.UnitCost;
                                        lst.ForEach(x => x.TotalCostDetails.Currency = currencyDetails.CurrencyName);
                                    }
                                    else
                                    {

                                        throw new Exception("Exchange Rate Not Defined");

                                    }
                                }
                            }
                        }
                    }
                }

                response.data.ProcurmentSummaryModelList = lst;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + " " + ex.Message;
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

        #region "Update Invoice"

        public async Task<APIResponse> UpdateInvoice(UpdatePurchaseInvoiceModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //byte[] filepathBase64 = Encoding.UTF8.GetBytes(model.EmployeeImage);
                string[] str = model.Invoice.Split(",");
                byte[] filepath = Convert.FromBase64String(str[1]);

                string ex = str[0].Split("/")[1].Split(";")[0];
                if (ex == "plain")
                    ex = "txt";
                string guidname = Guid.NewGuid().ToString();
                //byte[] filepath = Encoding.UTF8.GetBytes(str[1]);
                string filename = guidname + "." + ex;
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                File.WriteAllBytes(@"Documents/" + filename, filepath);

                var employeeinfo = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.IsDeleted == false && x.PurchaseId == model.PurchaseId);
                if (employeeinfo != null)
                {
                    employeeinfo.InvoiceFileName = guidname;
                    employeeinfo.InvoiceFileType = "." + ex;
                    employeeinfo.ModifiedById = UserId;
                    employeeinfo.ModifiedDate = DateTime.Now;
                    employeeinfo.IsDeleted = false;
                    await _uow.StoreItemPurchaseRepository.UpdateAsyn(employeeinfo);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllPurchaseInvoices(string PurchaseId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var Invoices = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.PurchaseId == PurchaseId);
                UpdatePurchaseInvoiceModel obj = new UpdatePurchaseInvoiceModel();
                obj.PurchaseId = Invoices.PurchaseId;
                obj.Invoice = Invoices.InvoiceFileName + Invoices.InvoiceFileType;
                response.data.UpdatePurchaseInvoiceModel = obj;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion

        #region "Update Purchase Image"

        public async Task<APIResponse> UpdatePurchaseImage(UpdatePurchaseInvoiceModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //byte[] filepathBase64 = Encoding.UTF8.GetBytes(model.EmployeeImage);
                string[] str = model.Invoice.Split(",");
                byte[] filepath = Convert.FromBase64String(str[1]);

                string ex = str[0].Split("/")[1].Split(";")[0];
                if (ex == "plain")
                    ex = "txt";
                string guidname = Guid.NewGuid().ToString();
                //byte[] filepath = Encoding.UTF8.GetBytes(str[1]);
                string filename = guidname + "." + ex;
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                File.WriteAllBytes(@"Documents/" + filename, filepath);

                var employeeinfo = await _uow.StoreItemPurchaseRepository.FindAsync(x => x.IsDeleted == false && x.PurchaseId == model.PurchaseId);
                if (employeeinfo != null)
                {
                    employeeinfo.ImageFileName = guidname;
                    employeeinfo.ImageFileType = "." + ex;
                    employeeinfo.ModifiedById = UserId;
                    employeeinfo.ModifiedDate = DateTime.Now;
                    employeeinfo.IsDeleted = false;
                    await _uow.StoreItemPurchaseRepository.UpdateAsyn(employeeinfo);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion


        #region "Store Item Type Specifications"

        public async Task<APIResponse> AddItemSpecificationsDetails(List<ItemSpecificationDetailModel> model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.Count > 0)
                {
                    List<ItemSpecificationDetails> obj = _mapper.Map<List<ItemSpecificationDetails>>(model);
                    obj = obj.Select(x =>
                    {
                        x.CreatedById = UserId;
                        x.CreatedDate = DateTime.Now;
                        x.IsDeleted = false;
                        return x;
                    }).ToList();
                    await _uow.GetDbContext().ItemSpecificationDetails.AddRangeAsync(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model is invalid";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditItemSpecificationsDetails(ItemSpecificationDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model !=null)
                {
                    var existRecord = await _uow.ItemSpecificationDetailsRepository.FindAllAsync(x => x.IsDeleted == false && x.ItemId == model.ItemId && x.ItemSpecificationMasterId== model.ItemSpecificationMasterId);
                    if (existRecord.Count > 0)
                    {
                        _uow.GetDbContext().ItemSpecificationDetails.RemoveRange(existRecord);
                    }
                     ItemSpecificationDetails obj = _mapper.Map<ItemSpecificationDetails>(model);
                    //obj = obj.Select(x =>
                    //{
                    //    x.CreatedById = UserId;
                    //    x.CreatedDate = DateTime.Now;
                    //    x.IsDeleted = false;
                    //    return x;
                    //}).ToList();

                    obj.CreatedById = UserId;
                    obj.CreatedDate= DateTime.Now;
                    obj.IsDeleted = false;

                    await _uow.GetDbContext().ItemSpecificationDetails.AddRangeAsync(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model is invalid";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllItemSpecificationsDetails(string ItemId, int ItemTypeId, int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                bool flag = await _uow.GetDbContext().ItemSpecificationDetails.AnyAsync(x => x.ItemId == ItemId && x.IsDeleted == false);
                var masterList = await _uow.ItemSpecificationMasterRepository.FindAllAsync(x => x.IsDeleted == false && x.ItemTypeId == ItemTypeId && x.OfficeId == OfficeId);
                if (flag == true)
                {

                    var list = await _uow.GetDbContext().ItemSpecificationDetails.Include(x => x.ItemSpecificationMaster).Where(x => x.ItemId == ItemId && x.ItemSpecificationMaster.ItemTypeId == ItemTypeId && x.IsDeleted == false).ToListAsync();
                    response.data.ItemSpecificationDetailList = list.Select(x => new ItemSpecificationDetailModel
                    {
                        ItemSpecificationMasterId = x.ItemSpecificationMasterId,
                        ItemId = x.ItemId,
                        ItemSpecificationValue = x.ItemSpecificationValue,
                        ItemSpecificationField = x.ItemSpecificationMaster.ItemSpecificationField
                    }).ToList();

                    foreach (var item in masterList)
                    {
                        var recordExist = list.Where(x => x.ItemSpecificationMasterId == item.ItemSpecificationMasterId).FirstOrDefault();
                        if (recordExist == null)
                        {
                            ItemSpecificationDetailModel obj = new ItemSpecificationDetailModel();
                            obj.ItemSpecificationMasterId = item.ItemSpecificationMasterId;
                            obj.ItemId = ItemId;
                            obj.ItemSpecificationValue = null;
                            obj.ItemSpecificationField = item.ItemSpecificationField;
                            response.data.ItemSpecificationDetailList.Add(obj);
                        }
                    }
                }
                else
                {

                    response.data.ItemSpecificationDetailList = masterList.Select(x => new ItemSpecificationDetailModel
                    {
                        ItemSpecificationMasterId = x.ItemSpecificationMasterId,
                        ItemId = ItemId,
                        ItemSpecificationValue = null,
                        ItemSpecificationField = x.ItemSpecificationField
                    }).ToList();
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion


        #region "Store Item Type Specifications Master"

        public async Task<APIResponse> AddItemSpecificationsMaster(ItemSpecificationMasterModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    ItemSpecificationMaster obj = _mapper.Map<ItemSpecificationMaster>(model);
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsDeleted = false;
                    await _uow.GetDbContext().ItemSpecificationMaster.AddAsync(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model is invalid";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditItemSpecificationsMaster(ItemSpecificationMasterModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model != null)
                {
                    var existRecord = await _uow.ItemSpecificationMasterRepository.FindAsync(x => x.IsDeleted == false && x.ItemSpecificationMasterId == model.ItemSpecificationMasterId);
                    _mapper.Map(model, existRecord);
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    await _uow.ItemSpecificationMasterRepository.UpdateAsyn(existRecord);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model is invalid";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetItemSpecificationsMaster(int ItemTypeId, int OfficeId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.ItemSpecificationMasterRepository.FindAllAsync(x => x.IsDeleted == false && x.ItemTypeId == ItemTypeId && x.OfficeId == OfficeId);
                response.data.ItemSpecificationMasterList = list.Select(x => new ItemSpecificationMasterModel
                {
                    ItemSpecificationMasterId = x.ItemSpecificationMasterId,
                    ItemSpecificationField = x.ItemSpecificationField,
                    OfficeId = x.OfficeId,
                    ItemTypeId = x.ItemTypeId
                }).ToList();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion



        #region "Dropdown Fields"

        public async Task<APIResponse> GetAllStatusAtTimeOfIssue()
        {
            APIResponse response = new APIResponse();
            try
            {
                var statusAtTimeOfIssueList = await _uow.StatusAtTimeOfIssueRepository.GetAllAsyn();

                response.data.StatusAtTimeOfIssueList = statusAtTimeOfIssueList.ToList();
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

        public async Task<APIResponse> GetAllReceiptType()
        {
            APIResponse response = new APIResponse();
            try
            {
                var receiptTypeList = await _uow.ReceiptTypeRepository.GetAllAsyn();

                response.data.ReceiptTypeList = receiptTypeList.ToList();
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

        public async Task<APIResponse> GetInventoryCode(int Id)
        {
            APIResponse response = new APIResponse();
            try
            {
                StoreInventory storeInventories = await _uow.GetDbContext().StoreInventories.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.AssetType == Id && x.IsDeleted== false);

                if (storeInventories != null)
                {
                    int InventoryNumber = Convert.ToInt32(storeInventories.InventoryCode.Substring(1));

                    if (Id == (int)InventoryMasterType.Consumables)
                    {
                        response.data.InventoryCode = "C" + String.Format("{0:D4}", ++InventoryNumber);
                    }
                    else if (Id == (int)InventoryMasterType.Expendables)
                    {
                        response.data.InventoryCode = "E" + String.Format("{0:D4}", ++InventoryNumber);
                    }
                    else
                    {
                        response.data.InventoryCode = "N" + String.Format("{0:D4}", ++InventoryNumber);
                    }
                }
                else
                {
                    if (Id == (int)InventoryMasterType.Consumables)
                    {
                        response.data.InventoryCode = "C" + String.Format("{0:D4}", 1);
                    }
                    else if (Id == (int)InventoryMasterType.Expendables)
                    {
                        response.data.InventoryCode = "E" + String.Format("{0:D4}", 1);
                    }
                    else
                    {
                        response.data.InventoryCode = "N" + String.Format("{0:D4}", 1);
                    }
                }

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

        public async Task<APIResponse> GetInventoryItemCode(string InventoryId, int TypeId)
        {
            APIResponse response = new APIResponse();

            try
            {
                StoreInventoryItem storeInventoryItem = await _uow.GetDbContext().InventoryItems.Include(x => x.Inventory).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.ItemInventory == InventoryId);

                string inventoryItemCode = string.Empty;

                if (storeInventoryItem != null)
                {
                    string InventoryCode = storeInventoryItem.Inventory.InventoryCode;

                    int storeItemNumber = Convert.ToInt32(storeInventoryItem.ItemCode.Substring(6));

                    response.data.InventoryItemCode = InventoryCode + String.Format("{0:D5}", ++storeItemNumber);
                }
                else
                {
                    StoreInventory storeInventory = await _uow.GetDbContext().StoreInventories.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.AssetType == TypeId);

                    if (storeInventory != null)
                    {
                        response.data.InventoryItemCode = storeInventory.InventoryCode + "00001";
                    }
                    else
                    {
                        throw new Exception("No Master Invenmtory found");
                    }
                }

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

        /// <summary>
        /// Get All Store Source Types
        /// </summary>
        /// <returns>List of CodeType</returns>
        public async Task<APIResponse> GetAllStoreSourceType()
        {
            APIResponse response = new APIResponse();

            try
            {

                List<CodeType> CodeTypelist = await _uow.GetDbContext().CodeType.ToListAsync();

                response.data.SourceCodeTypelist = CodeTypelist;
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

        /// <summary>
        /// Get All Store Source Code
        /// </summary>
        /// <returns>List of Store Source Code</returns>
        public async Task<APIResponse> GetAllStoreSourceCode(int? typeId)
        {
            APIResponse response = new APIResponse();

            try
            {
                List<StoreSourceCodeDetail> StoreSourceCodeDetailList = new List<StoreSourceCodeDetail>();

                //Get Store Source Code Detail based on source code type selected
                if (typeId != null)
                {
                   StoreSourceCodeDetailList = await _uow.GetDbContext().StoreSourceCodeDetail.Where(x => x.IsDeleted == false && x.CodeTypeId == typeId).ToListAsync();
                }
                else //Source Code Type is empty so Get all Store Source Code Detail
                {
                    StoreSourceCodeDetailList = await _uow.GetDbContext().StoreSourceCodeDetail.Where(x => x.IsDeleted == false).ToListAsync();
                }

                List<StoreSourceCodeDetailModel> obj = _mapper.Map<List<StoreSourceCodeDetailModel>>(StoreSourceCodeDetailList);
                response.data.SourceCodeDatalist = obj;
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

        /// <summary>
        /// Add Store Source Code
        /// </summary>
        /// <param name="storeSourceCodeDetailModel"></param>
        /// <param name="userId"></param>
        /// <returns>list of StoreSourceCode</returns>
        public async Task<APIResponse> AddStoreSourceCode(StoreSourceCodeDetailModel storeSourceCodeDetailModel, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                StoreSourceCodeDetail obj = _mapper.Map<StoreSourceCodeDetail>(storeSourceCodeDetailModel);

                obj.IsDeleted = false;
                obj.CreatedDate = DateTime.Now;
                obj.CreatedById = userId;

                await _uow.GetDbContext().StoreSourceCodeDetail.AddAsync(obj);
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

        /// <summary>
        /// Get Store Type Code
        /// </summary>
        /// <param name="storeSourceCodeDetailModel"></param>
        /// <param name="userId"></param>
        /// <returns> Store Type Code </returns>
        public async Task<APIResponse> GetStoreTypeCode(int CodeTypeId)
        {
            APIResponse response = new APIResponse();

            try
            {
                string storeCode = string.Empty;

                int codeNumber = 0;

                //Getting latest created record of StoreSourceCodeDetail based on source code type selected
                StoreSourceCodeDetail storeSourceCodeDetail = await _uow.GetDbContext().StoreSourceCodeDetail.OrderByDescending(x => x.SourceCodeId).FirstOrDefaultAsync(x => x.IsDeleted == false && x.CodeTypeId == CodeTypeId);

                if (storeSourceCodeDetail != null)
                {
                    //retreiving the number in code
                    if (int.TryParse(storeSourceCodeDetail.Code.Substring(1), out codeNumber))
                    {

                        //generating a new code for new entry in StoreSourceCodeDetail table based on source code type selected
                        switch (CodeTypeId)
                        {
                            case (int)SourceCode.Organizations:
                                storeCode = "O" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.Suppliers:
                                storeCode = "S" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.RepairShops:
                                storeCode = "R" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.LocationsStores:
                                storeCode = "L" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.IndividualOthers:
                                storeCode = "I" + String.Format("{0:D5}", ++codeNumber);
                                break;
                            case (int)SourceCode.Test:
                                storeCode = "T" + String.Format("{0:D5}", ++codeNumber);
                                break;
                        }

                        response.data.StoreSourceCode = storeCode;
                    }

                }
                else//record is not present
                {
                    switch (CodeTypeId)
                    {
                        case (int)SourceCode.Organizations:
                            storeCode = "O" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.Suppliers:
                            storeCode = "S" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.RepairShops:
                            storeCode = "R" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.LocationsStores:
                            storeCode = "L" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.IndividualOthers:
                            storeCode = "I" + String.Format("{0:D5}", ++codeNumber);
                            break;
                        case (int)SourceCode.Test:
                            storeCode = "T" + String.Format("{0:D5}", ++codeNumber);
                            break;
                    }

                    response.data.StoreSourceCode = storeCode;
                }

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

        /// <summary>
        /// Edit Store Source Code
        /// </summary>
        /// <param name="storeSourceCodeDetailModel"></param>
        /// <param name="userId"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> EditStoreSourceCode(StoreSourceCodeDetailModel storeSourceCodeDetailModel, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                //Retrieving the Old storeSourceCode record from  db
                StoreSourceCodeDetail storeSourceCodeDetail = await _uow.GetDbContext().StoreSourceCodeDetail.FirstOrDefaultAsync(x=> x.IsDeleted== false && x.SourceCodeId== storeSourceCodeDetailModel.SourceCodeId);

                //Mapping new values in old storeSourceCode record
                storeSourceCodeDetail.Address = storeSourceCodeDetailModel.Address;
                storeSourceCodeDetail.Code = storeSourceCodeDetailModel.Code;
                storeSourceCodeDetail.CodeTypeId = storeSourceCodeDetailModel.CodeTypeId;
                storeSourceCodeDetail.Description = storeSourceCodeDetailModel.Description;
                storeSourceCodeDetail.EmailAddress = storeSourceCodeDetailModel.EmailAddress;
                storeSourceCodeDetail.Fax = storeSourceCodeDetailModel.Fax;
                storeSourceCodeDetail.Guarantor = storeSourceCodeDetailModel.Guarantor;
                storeSourceCodeDetail.Phone = storeSourceCodeDetailModel.Phone;
                storeSourceCodeDetail.IsDeleted = false;
                storeSourceCodeDetail.ModifiedDate = DateTime.Now;
                storeSourceCodeDetail.ModifiedById = userId;

                //saving newly mapped object
                _uow.GetDbContext().StoreSourceCodeDetail.Update(storeSourceCodeDetail);
                _uow.Save();

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

        /// <summary>
        /// Delete Store Source Code
        /// </summary>
        /// <param name="storeSourceCodeDetailModel"></param>
        /// <param name="userId"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> DeleteStoreSourceCode(int storeSourceCodeId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                //Returning the storeSourceCodeDetail record based on storeSourceCodeId received
                StoreSourceCodeDetail storeSourceCodeDetail = _uow.StoreSourceCodeRepository.Find(x=> x.SourceCodeId== storeSourceCodeId && x.IsDeleted== false);

                //Deleting record
                storeSourceCodeDetail.IsDeleted = true;
                storeSourceCodeDetail.ModifiedDate = DateTime.Now;
                storeSourceCodeDetail.ModifiedById = userId;

                //updating to db
                await _uow.StoreSourceCodeRepository.UpdateAsyn(storeSourceCodeDetail);

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

        /// <summary>
        /// Get All Store Payment Types
        /// </summary>
        /// <returns>Payment Type List</returns>
        public async Task<APIResponse> GetAllPaymentTypes()
        {
            APIResponse response = new APIResponse();

            try
            {
                //Get List of Store Payment Types
                ICollection<PaymentTypes> paymentTypesList = await _uow.PaymentTypesRepository.FindAllAsync(x => x.IsDeleted == false);

                response.data.PaymentTypesList = paymentTypesList;
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

        /// <summary>
        /// Add Store Payment Type
        /// </summary>
        /// <param name="model">Payment type Model</param>
        /// <param name="UserId"></param>
        /// <returns>Succuss/Failure</returns>
        public async Task<APIResponse> AddPaymentTypes(PaymentTypes model, string UserId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {
                    PaymentTypes storePaymentTypes = new PaymentTypes();

                    storePaymentTypes.IsDeleted = false;
                    storePaymentTypes.CreatedById = UserId;
                    storePaymentTypes.CreatedDate = DateTime.Now;
                    storePaymentTypes.Name = model.Name;
                    storePaymentTypes.ChartOfAccountNewId = model.ChartOfAccountNewId;

                    await _uow.PaymentTypesRepository.AddAsyn(storePaymentTypes);
                }

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

        /// <summary>
        /// Update Store Payment Type
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> EditPaymentTypes(PaymentTypes model, string UserId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (model != null)
                {
                    //Get Payment Type Record based on payment id
                    PaymentTypes storePaymentTypes = await _uow.PaymentTypesRepository.FindAsync(x => x.IsDeleted == false && x.PaymentId == model.PaymentId);

                    if (storePaymentTypes != null)
                    {
                        storePaymentTypes.ModifiedById = UserId;
                        storePaymentTypes.ModifiedDate = DateTime.Now;
                        storePaymentTypes.Name = model.Name;
                        storePaymentTypes.ChartOfAccountNewId = model.ChartOfAccountNewId;
                    }

                    //update PaymentType Record
                    await _uow.PaymentTypesRepository.UpdateAsyn(storePaymentTypes);
                }

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

        /// <summary>
        /// Delete Payment Type Record
        /// </summary>
        /// <param name="PaymentId"></param>
        /// <param name="UserId"></param>
        /// <returns>Success/Failure</returns>
        public async Task<APIResponse> DeletePaymentTypes(int PaymentId, string UserId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (PaymentId != 0)
                {
                    //Get Payment Type Record based on payment id
                    PaymentTypes storePaymentTypes = await _uow.PaymentTypesRepository.FindAsync(x => x.IsDeleted == false && x.PaymentId == PaymentId);

                    if (storePaymentTypes != null)
                    {
                        storePaymentTypes.ModifiedById = UserId;
                        storePaymentTypes.ModifiedDate = DateTime.Now;
                        storePaymentTypes.IsDeleted = true;
                    }

                    //update PaymentType Record
                    await _uow.PaymentTypesRepository.UpdateAsyn(storePaymentTypes);
                }

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

        public async Task<APIResponse> VerifyPurchase(ItemPurchaseModel model)
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

                        if (!string.IsNullOrEmpty(model.ImageFileName))
                        {
                            if (model.ImageFileName.Contains(","))
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
                        }

                        if (model.InvoiceFileName != null && model.InvoiceFileName != "")
                        {
                            if (model.InvoiceFileName.Contains(","))
                            {
                                string[] str = model.InvoiceFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.InvoiceFileName = guidname;
                                purchaseRecord.InvoiceFileType = "." + ex;
                            }
                        }

                        purchaseRecord.IsDeleted = false;

                        
                        //List<ExchangeRate> exchangeRate = new List<ExchangeRate>();

                        if (model.IsPurchaseVerified.HasValue && model.IsPurchaseVerified.Value)
                        {

                            var officeCode = _uow.OfficeDetailRepository.FindAsync(o => o.OfficeId == model.OfficeId).Result.OfficeCode; //use OfficeCode
                            var financialYearDetails = _uow.GetDbContext().FinancialYearDetail.FirstOrDefault(x => x.IsDeleted == false && x.StartDate.Date.Year == DateTime.Now.Year);
                            var inventory = _uow.GetDbContext().InventoryItems.Include(x => x.Inventory).FirstOrDefault(x => x.ItemId == model.InventoryItem);
                            var paymentTypes = _uow.GetDbContext().PaymentTypes.FirstOrDefault(x => x.PaymentId == model.PaymentTypeId);
                            //exchangeRate = await _uow.GetDbContext().ExchangeRates.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Date).ToListAsync();

                            VoucherDetail obj = new VoucherDetail();
                            obj.CreatedById = model.CreatedById;
                            obj.CreatedDate = DateTime.UtcNow;
                            obj.IsDeleted = false;
                            obj.FinancialYearId = financialYearDetails.FinancialYearId;
                            obj.VoucherTypeId = (int)VoucherTypes.Journal;
                            obj.Description = StaticResource.PurchaseVoucherCreated;
                            obj.CurrencyId = model.Currency;
                            obj.VoucherDate = DateTime.Now;
                            //obj.ChequeNo = model.ChequeNo;
                            obj.JournalCode = model.JournalCode;
                            obj.OfficeId = model.OfficeId;
                            obj.ProjectId = model.ProjectId;
                            obj.BudgetLineId = model.BudgetLineId;
                            obj.IsExchangeGainLossVoucher = false;

                            await _uow.VoucherDetailRepository.AddAsyn(obj);

                            obj.ReferenceNo = officeCode + "-" + obj.VoucherNo;
                            await _uow.VoucherDetailRepository.UpdateAsyn(obj);

                            purchaseRecord.VerifiedPurchaseVoucher = obj.VoucherNo;
                            await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);

                            List<VoucherTransactions> voucherTransactionsList = new List<VoucherTransactions>();

                            //Credit
                            VoucherTransactions voucherTransactionCredit = new VoucherTransactions();
                            //Debit
                            VoucherTransactions voucherTransactionDebit = new VoucherTransactions();

                            voucherTransactionCredit.IsDeleted = false;
                            voucherTransactionCredit.VoucherNo = obj.VoucherNo;
                            voucherTransactionCredit.FinancialYearId = financialYearDetails.FinancialYearId;
                            voucherTransactionCredit.ChartOfAccountNewId = paymentTypes.ChartOfAccountNewId;
                            voucherTransactionCredit.CreditAccount = paymentTypes.ChartOfAccountNewId;
                            voucherTransactionCredit.DebitAccount = inventory.Inventory.InventoryDebitAccount;
                            voucherTransactionCredit.Credit = model.UnitCost * model.Quantity;
                            voucherTransactionCredit.Debit = 0;
                            voucherTransactionCredit.CurrencyId = model.Currency;
                            voucherTransactionCredit.Description = StaticResource.PurchaseVoucherCreated;
                            voucherTransactionCredit.OfficeId = model.OfficeId;
                            voucherTransactionCredit.TransactionDate = obj.VoucherDate;

                            voucherTransactionsList.Add(voucherTransactionCredit);

                            //Debit
                            voucherTransactionDebit.IsDeleted = false;
                            voucherTransactionDebit.VoucherNo = obj.VoucherNo;
                            voucherTransactionDebit.FinancialYearId = financialYearDetails.FinancialYearId;
                            //voucherTransactionCredit.ChartOfAccountNewId = paymentTypes.ChartOfAccountNewId;
                            //voucherTransactionCredit.CreditAccount = paymentTypes.ChartOfAccountNewId;
                            //voucherTransactionCredit.DebitAccount = inventory.Inventory.InventoryDebitAccount;
                            //voucherTransactionCredit.Credit = model.UnitCost * model.Quantity;
                            //voucherTransactionCredit.Debit = 0;
                            voucherTransactionDebit.CurrencyId = model.Currency;
                            voucherTransactionDebit.Description = StaticResource.PurchaseVoucherCreated;
                            voucherTransactionDebit.OfficeId = model.OfficeId;
                            voucherTransactionDebit.TransactionDate = obj.VoucherDate;
                            voucherTransactionDebit.CreditAccount = 0;
                            voucherTransactionDebit.DebitAccount = voucherTransactionCredit.DebitAccount;
                            voucherTransactionDebit.ChartOfAccountNewId = voucherTransactionCredit.DebitAccount;
                            voucherTransactionDebit.Debit = voucherTransactionCredit.Credit;
                            voucherTransactionDebit.Credit = 0;

                            voucherTransactionsList.Add(voucherTransactionDebit);

                            await _uow.GetDbContext().VoucherTransactions.AddRangeAsync(voucherTransactionsList);
                            await _uow.SaveAsync();

                            ////Passing Voucher Transaction model for Credit
                            //response.data.VoucherTransactionModel = xVoucherTransactionModel;
                            //response.data.VoucherReferenceNo = obj.ReferenceNo;
                            ////response.data.ExchangeRates = exchangeRate;
                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Success";

                        }
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

        public async Task<APIResponse> UnverifyPurchase(ItemPurchaseModel model)
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

                        if (!string.IsNullOrEmpty(model.ImageFileName))
                        {
                            if (model.ImageFileName.Contains(","))
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
                        }

                        if (model.InvoiceFileName != null && model.InvoiceFileName != "")
                        {
                            if (model.InvoiceFileName.Contains(","))
                            {
                                string[] str = model.InvoiceFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.InvoiceFileName = guidname;
                                purchaseRecord.InvoiceFileType = "." + ex;
                            }
                        }

                        purchaseRecord.IsDeleted = false;

                        List<VoucherTransactions> newVoucherTransactionList = new List<VoucherTransactions>();

                        if (model.IsPurchaseVerified.HasValue && !model.IsPurchaseVerified.Value)
                        {
                            
                            VoucherDetail voucherDetail = _uow.GetDbContext().VoucherDetail.FirstOrDefault(x => x.IsDeleted == false && x.VoucherNo == model.VerifiedPurchaseVoucher);

                            if (voucherDetail != null)
                            {
                                List<VoucherTransactions> voucherTransactionsList = await _uow.GetDbContext().VoucherTransactions.Where(x => x.IsDeleted == false && x.VoucherNo == model.VerifiedPurchaseVoucher).ToListAsync();

                                foreach (VoucherTransactions transaction in voucherTransactionsList)
                                {
                                    VoucherTransactions voucherTransaction = new VoucherTransactions();

                                    voucherTransaction.IsDeleted = false;
                                    voucherTransaction.VoucherNo = model.VerifiedPurchaseVoucher;
                                    voucherTransaction.FinancialYearId = voucherDetail.FinancialYearId;
                                    voucherTransaction.ChartOfAccountNewId = transaction.ChartOfAccountNewId;
                                    voucherTransaction.CurrencyId = transaction.CurrencyId;
                                    voucherTransaction.Description = StaticResource.PurchaseVoucherUnverified;
                                    voucherTransaction.OfficeId = model.OfficeId;
                                    voucherTransaction.TransactionDate = DateTime.Now;

                                    if (transaction.Credit != 0 && transaction.Credit != null)
                                    {

                                        voucherTransaction.Debit = transaction.Credit;
                                        voucherTransaction.Credit = 0;
                                        voucherTransaction.DebitAccount = transaction.CreditAccount;
                                    }
                                    else if (transaction.Debit != 0 && transaction.Debit != null)
                                    {
                                        voucherTransaction.Credit = transaction.Debit;
                                        voucherTransaction.Debit = 0;
                                        voucherTransaction.CreditAccount= transaction.DebitAccount;
                                    }

                                    newVoucherTransactionList.Add(voucherTransaction);
                                }

                                await _uow.GetDbContext().VoucherTransactions.AddRangeAsync(newVoucherTransactionList);
                                await _uow.SaveAsync();

                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Success";
                            }
                            else
                            {
                                throw new Exception(" Voucher Not Found on Verified Purchase");
                            }
                        }
                        else
                        {
                            await _uow.StoreItemPurchaseRepository.UpdateAsyn(purchaseRecord);
                        }
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

        public async Task<APIResponse> AddStoreItemGroup(StoreItemGroupModel storeGroupItem, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (storeGroupItem != null)
                {
                    StoreItemGroup storeItemGroup = new StoreItemGroup();

                    storeItemGroup.CreatedById = userId;
                    storeItemGroup.CreatedDate = DateTime.Now;
                    storeItemGroup.IsDeleted = false;
                    storeItemGroup.Description = storeGroupItem.Description;
                    storeItemGroup.InventoryId = storeGroupItem.InventoryId;
                    storeItemGroup.ItemGroupCode = storeGroupItem.ItemGroupCode;
                    storeItemGroup.ItemGroupName = storeGroupItem.ItemGroupName;

                    await _uow.GetDbContext().StoreItemGroups.AddAsync(storeItemGroup);
                    await _uow.GetDbContext().SaveChangesAsync();
                }

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

        public async Task<APIResponse> GetStoreGroupItemCode(string inventoryId)
        {
            APIResponse response = new APIResponse();
            string ItemGroupCode = "";
            try
            {
                if (inventoryId != null)
                {
                    StoreItemGroup storeItemGroup = await _uow.GetDbContext().StoreItemGroups.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.IsDeleted == false && x.InventoryId == inventoryId);

                    if (storeItemGroup != null)
                    {
                        int count = Convert.ToInt32(storeItemGroup.ItemGroupCode.Substring(5));
                        ItemGroupCode = storeItemGroup.StoreInventory.InventoryCode + String.Format("{0:D4}", ++count);
                    }
                    else
                    {
                       StoreInventory storeInventory= await _uow.GetDbContext().StoreInventories.FirstOrDefaultAsync(x => x.IsDeleted == false && x.InventoryId == inventoryId);

                        ItemGroupCode = storeInventory.InventoryCode + String.Format("{0:D4}", 1);
                    }
                }

                response.data.ItemGroupCode = ItemGroupCode;
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

        public async Task<APIResponse> EditStoreItemGroup(StoreItemGroupModel storeGroupItem, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                if (storeGroupItem != null)
                {
                    StoreItemGroup storeItemGroup = await _uow.GetDbContext().StoreItemGroups.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ItemGroupId == storeGroupItem.ItemGroupId);

                    storeItemGroup.ModifiedById = userId;
                    storeItemGroup.ModifiedDate = DateTime.Now;
                    storeItemGroup.IsDeleted = false;
                    storeItemGroup.Description = storeGroupItem.Description;
                    storeItemGroup.InventoryId = storeGroupItem.InventoryId;
                    storeItemGroup.ItemGroupCode = storeGroupItem.ItemGroupCode;
                    storeItemGroup.ItemGroupName = storeGroupItem.ItemGroupName;

                    _uow.GetDbContext().StoreItemGroups.Update(storeItemGroup);
                    await _uow.GetDbContext().SaveChangesAsync();
                }

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