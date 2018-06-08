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
using HumanitarianAssistance.ViewModels.Models.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/Store/[Action]")]
  public class StoreController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private IStore _iStore;
    public StoreController(UserManager<AppUser> userManager, IStore iStore)
    {
      _userManager = userManager;
      _iStore = iStore;
    }

    #region "Store Inventories"
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllInventories(int? AssetType)
    {
      APIResponse apiresponse = await _iStore.GetAllInventories(AssetType);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllInventoryItems(string ItemInventory)
    {
      APIResponse apiresponse = await _iStore.GetAllInventoryItems(ItemInventory);
      return apiresponse;
    }

    #endregion


    #region "Store Item Types"

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllInventoryItemsType()
    {
      APIResponse apiresponse = await _iStore.GetAllInventoryItemsType();
      return apiresponse;
    }

    #endregion


    #region "Store Purchase"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetSerialNumber(string serialNumber)
    {
      APIResponse apiresponse = await _iStore.GetSerialNumber(serialNumber);
      return apiresponse;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllPurchasesByItem(string itemId)
    {
      APIResponse apiresponse = await _iStore.GetAllPurchasesByItem(itemId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddItemOrder([FromBody] ItemOrderModel model)
    {
      APIResponse apiresponse = await _iStore.AddItemOrder(model);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditItemOrder([FromBody] ItemOrderModel model)
    {
      APIResponse apiresponse = await _iStore.EditItemOrder(model);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteItemOrder([FromBody] ItemOrderModel model)
    {
      APIResponse apiresponse = await _iStore.DeleteItemOrder(model);
      return apiresponse;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllItemsOrder(string ItemId)
    {
      APIResponse apiresponse = await _iStore.GetAllItemsOrder(ItemId);
      return apiresponse;
    }
    
    #endregion




    #region "Purchase Unit Type"

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllPurchaseUnitType()
    {
      APIResponse apiresponse = await _iStore.GetAllPurchaseUnitType();
      return apiresponse;
    }

    #endregion



  }
}
