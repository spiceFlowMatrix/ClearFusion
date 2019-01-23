using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Store;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/Store/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class StoreController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private IStore _iStore;
    private IVoucherDetail _iVoucherDetail;
    public StoreController(UserManager<AppUser> userManager, IStore iStore, IVoucherDetail iVoucherDetail)
    {
      _userManager = userManager;
      _iStore = iStore;
      _iVoucherDetail = iVoucherDetail;
    }

    #region "Store Inventories"
    [HttpGet]
    public async Task<APIResponse> GetAllInventories(int? AssetType)
    {
      APIResponse apiresponse = await _iStore.GetAllInventories(AssetType);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddInventory([FromBody] StoreInventoryModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;
        apiRespone = await _iStore.AddInventory(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditInventory([FromBody] StoreInventoryModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.EditInventory(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteInventory([FromBody] StoreInventoryModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.DeleteInventory(model);
      }
      return apiRespone;
    }

    #endregion


    #region "Store Items"

    [HttpPost]
    public async Task<APIResponse> AddInventoryItems([FromBody] StoreInventoryItemModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;
        apiRespone = await _iStore.AddInventoryItems(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditInventoryItems([FromBody] StoreInventoryItemModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.EditInventoryItems(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteInventoryItems([FromBody] StoreInventoryItemModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.DeleteInventoryItems(model);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllInventoryItems(string ItemInventory)
    {
      APIResponse apiresponse = await _iStore.GetAllInventoryItems(ItemInventory);
      return apiresponse;
    }

    #endregion


    #region "Store Item Types"

    [HttpPost]
    public async Task<APIResponse> AddInventoryItemsType([FromBody] InventoryItemTypeModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;
        apiRespone = await _iStore.AddInventoryItemsType(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditInventoryItemsType([FromBody] InventoryItemTypeModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.EditInventoryItemsType(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteInventoryItemsType([FromBody] InventoryItemTypeModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.DeleteInventoryItemsType(model);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllInventoryItemsType()
    {
      APIResponse apiresponse = await _iStore.GetAllInventoryItemsType();
      return apiresponse;
    }

    #endregion


    #region "Store Purchase"

    [HttpGet]
    public async Task<APIResponse> GetSerialNumber(string serialNumber)
    {
      APIResponse apiresponse = await _iStore.GetSerialNumber(serialNumber);
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllPurchasesByItem(string itemId)
    {
      APIResponse apiresponse = await _iStore.GetAllPurchasesByItem(itemId);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddPurchase([FromBody] ItemPurchaseModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;
        apiRespone = await _iStore.AddPurchase(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditPurchase([FromBody] ItemPurchaseModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.EditPurchase(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeletePurchase([FromBody] ItemPurchaseModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.DeletePurchase(model);
      }
      return apiRespone;
    }

    #endregion



    #region "Store Order"

    [HttpPost]
    public async Task<APIResponse> AddItemOrder([FromBody] ItemOrderModel model)
    {
      APIResponse apiresponse = await _iStore.AddItemOrder(model);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> EditItemOrder([FromBody] ItemOrderModel model)
    {
      APIResponse apiresponse = await _iStore.EditItemOrder(model);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteItemOrder([FromBody] ItemOrderModel model)
    {
      APIResponse apiresponse = await _iStore.DeleteItemOrder(model);
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllItemsOrder(string ItemId)
    {
      APIResponse apiresponse = await _iStore.GetAllItemsOrder(ItemId);
      return apiresponse;
    }

    #endregion




    #region "Purchase Unit Type"

    [HttpPost]
    public async Task<APIResponse> AddPurchaseUnitType([FromBody]PurchaseUnitType model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;
        apiRespone = await _iStore.AddPurchaseUnitType(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> EditPurchaseUnitType([FromBody] PurchaseUnitType model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.EditPurchaseUnitType(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeletePurchaseUnitType([FromBody] PurchaseUnitType model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.DeletePurchaseUnitType(model);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllPurchaseUnitType()
    {
      APIResponse apiresponse = await _iStore.GetAllPurchaseUnitType();
      return apiresponse;
    }

    #endregion


    [HttpGet]
    public async Task<APIResponse> GetItemAmounts(string ItemId)
    {
      APIResponse apiresponse = await _iStore.GetItemAmounts(ItemId);
      return apiresponse;
    }


    [HttpGet]
    public async Task<APIResponse> GetProcurementSummary(int EmployeeId, int CurrencyId)
    {
      APIResponse apiresponse = await _iStore.GetProcurementSummary(EmployeeId, CurrencyId);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllDepreciationByFilter([FromBody]DepreciationReportFilter depretiationFilter)
    {
      APIResponse apiresponse = await _iStore.GetAllDepreciationByFilter(depretiationFilter);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> UpdateInvoice([FromBody]UpdatePurchaseInvoiceModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.UpdateInvoice(model, user.Id);
      }
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllPurchaseInvoices([FromQuery]string PurchaseId)
    {
      APIResponse apiresponse = await _iStore.GetAllPurchaseInvoices(PurchaseId);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> UpdatePurchaseImage([FromBody]UpdatePurchaseInvoiceModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.UpdatePurchaseImage(model, user.Id);
      }
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddItemSpecificationsDetails([FromBody]List<ItemSpecificationDetailModel> model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.AddItemSpecificationsDetails(model, user.Id);
      }
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> EditItemSpecificationsDetails([FromBody]ItemSpecificationDetailModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.EditItemSpecificationsDetails(model, user.Id);
      }
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllItemSpecificationsDetails([FromQuery]string ItemId, int ItemTypeId, int OfficeId)
    {
      APIResponse apiresponse = await _iStore.GetAllItemSpecificationsDetails(ItemId, ItemTypeId, OfficeId);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddItemSpecificationsMaster([FromBody]ItemSpecificationMasterModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.AddItemSpecificationsMaster(model, user.Id);
      }
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> EditItemSpecificationsMaster([FromBody]ItemSpecificationMasterModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.EditItemSpecificationsMaster(model, user.Id);
      }
      return apiresponse;
    }
    [HttpGet]
    public async Task<APIResponse> GetItemSpecificationsMaster([FromQuery]int ItemTypeId, int OfficeId)
    {
      APIResponse apiresponse = await _iStore.GetItemSpecificationsMaster(ItemTypeId, OfficeId);
      return apiresponse;
    }


    [HttpGet]
    public async Task<APIResponse> GetAllStatusAtTimeOfIssue()
    {
      APIResponse apiresponse = await _iStore.GetAllStatusAtTimeOfIssue();
      return apiresponse;
    }


    [HttpGet]
    public async Task<APIResponse> GetAllReceiptType()
    {
      APIResponse apiresponse = await _iStore.GetAllReceiptType();
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetInventoryCode([FromQuery] int Id)
    {
      APIResponse apiresponse = await _iStore.GetInventoryCode(Id);
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetInventoryItemCode([FromQuery] string Id, int TypeId)
    {
      APIResponse apiresponse = await _iStore.GetInventoryItemCode(Id, TypeId);
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllStoreSourceType()
    {
      APIResponse apiresponse = await _iStore.GetAllStoreSourceType();
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllStoreSourceCode(int? typeId)
    {
      APIResponse apiresponse = await _iStore.GetAllStoreSourceCode(typeId);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddStoreSourceCode([FromBody]StoreSourceCodeDetailModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.AddStoreSourceCode(model, user.Id);
      }
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetStoreTypeCode([FromQuery] int CodeTypeId)
    {
      APIResponse apiresponse = await _iStore.GetStoreTypeCode(CodeTypeId);
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> EditStoreSourceCode([FromBody]StoreSourceCodeDetailModel model)
    {
      APIResponse apiresponse = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiresponse = await _iStore.EditStoreSourceCode(model, user.Id);
      }
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> DeleteStoreSourceCode([FromQuery]int Id)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.DeleteStoreSourceCode(Id, user.Id);
      }
       
      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllPaymentTypes()
    {
      APIResponse apiresponse = new APIResponse();

      apiresponse = await _iStore.GetAllPaymentTypes();

      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddPaymentTypes([FromBody] PaymentTypes paymentTypes)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.AddPaymentTypes(paymentTypes, user.Id);
      }

      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> EditPaymentTypes([FromBody] PaymentTypes paymentTypes)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.EditPaymentTypes(paymentTypes, user.Id);
      }

      return apiresponse;
    }

    [HttpDelete]
    public async Task<APIResponse> DeletePaymentTypes([FromQuery] int PaymentId)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.DeletePaymentTypes(PaymentId, user.Id);
      }

      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> VerifyPurchase([FromBody] ItemPurchaseModel model)
    {
      APIResponse apiRespone = null;

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.VerifyPurchase(model);

        //if (apiRespone.StatusCode == 200 && apiRespone.Message.ToLower() == "success" && apiRespone.data.VoucherTransactionModel != null)
        //{

        //    long? debitAccount = apiRespone.data.VoucherTransactionModel.DebitAccount;
        //    apiRespone.data.VoucherTransactionModel.DebitAccount = 0;

        //    //Credit
        //    xApiRespone = await _iVoucherDetail.AddVoucherTransactionConvertedToExchangeRate(apiRespone.data.VoucherTransactionModel, apiRespone.data.ExchangeRates);

        //    apiRespone.data.VoucherTransactionModel.CreditAccount = 0;
        //    apiRespone.data.VoucherTransactionModel.DebitAccount = debitAccount;
        //    apiRespone.data.VoucherTransactionModel.AccountNo = debitAccount;
        //    apiRespone.data.VoucherTransactionModel.Debit = apiRespone.data.VoucherTransactionModel.Credit;
        //    apiRespone.data.VoucherTransactionModel.Credit = 0;

        //    //Debit
        //    xApiRespone = await _iVoucherDetail.AddVoucherTransactionConvertedToExchangeRate(apiRespone.data.VoucherTransactionModel, apiRespone.data.ExchangeRates);
        //}
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> UnverifyPurchase([FromBody] ItemPurchaseModel model)
    {
      APIResponse apiRespone = null;
      //APIResponse xApiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        apiRespone = await _iStore.UnverifyPurchase(model);

        //if (apiRespone.StatusCode == 200 && apiRespone.Message.ToLower() == "success")
        //{

        //  if (apiRespone.data.VoucherTransactionModelList.Any() && apiRespone.data.ExchangeRates.Any())
        //  {

        //    foreach (var item in apiRespone.data.VoucherTransactionModelList)
        //    {

        //      xApiRespone = await _iVoucherDetail.AddVoucherTransactionConvertedToExchangeRate(item, apiRespone.data.ExchangeRates);

        //    }
        //  }
        //}
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddStoreItemGroup([FromBody] StoreItemGroupModel storeGroupItem)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.AddStoreItemGroup(storeGroupItem, user.Id);
      }

      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetStoreGroupItemCode([FromQuery] string Id)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.GetStoreGroupItemCode(Id);
      }

      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> EditStoreItemGroup([FromBody] StoreItemGroupModel storeGroupItem)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.EditStoreItemGroup(storeGroupItem, user.Id);
      }

      return apiresponse;
    }


    [HttpPost]
    public async Task<APIResponse> GetStoreItemCode([FromQuery] long Id)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.GetStoreItemCode(Id);
      }

      return apiresponse;
    }

    [HttpGet]
    public async Task<APIResponse> GetAllStoreItemGroups([FromQuery]string Id)
    {
      APIResponse apiresponse = new APIResponse();

      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        apiresponse = await _iStore.GetAllStoreItemGroups(Id);
      }

      return apiresponse;
    }

  }
}
