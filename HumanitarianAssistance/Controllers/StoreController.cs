using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbEntities;
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

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllInventories()
    {
      APIResponse apiresponse = await _iStore.GetAllInventories();
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

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllPurchasesByItem(string itemId)
    {
      APIResponse apiresponse = await _iStore.GetAllPurchasesByItem(itemId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddItemPurchaseDocument([FromBody] StoreItemPurchaseDocumentModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;
        apiRespone = await _iStore.AddItemPurchaseDocument(model);
      }
      return apiRespone;
    }

  }
}
