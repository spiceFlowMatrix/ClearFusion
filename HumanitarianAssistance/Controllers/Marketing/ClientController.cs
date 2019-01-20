 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Marketing
{
  [Produces("application/json")]
  [Route("api/Client/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class ClientController : Controller
  {    
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IClientDetails _iclientDetailService;
    public ClientController(UserManager<AppUser> userManager, IClientDetails clientDetails)
    {
      _userManager = userManager;
      _iclientDetailService = clientDetails;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    #region Client

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetClientsPaginatedList([FromBody]ClientPaginationModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iclientDetailService.GetClientsPaginatedList(model, id);
      }
      return apiRespone;
    }

    /// <summary>
    /// get Client Details By Id
    /// </summary>
    /// <param name="ClientId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> GetClientDetailsById([FromBody]string ClientId)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iclientDetailService.GetClientDetailsById(Convert.ToInt32(ClientId), id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get Client List
    /// </summary>
    /// <returns></returns>
    
    [HttpGet]
    public async Task<APIResponse> GetAllClientList()
    {
      APIResponse apiresponse = await _iclientDetailService.GetAllClient();
      return apiresponse;
    }

    /// <summary>
    /// Add New Client
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    
    [HttpPost]
    public async Task<APIResponse> AddClient([FromBody]ClientDetailModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iclientDetailService.AddEditClientDetails(model, id);       
      }
      return apiResponse;
    }

    /// <summary>
    /// Edit Selected Client
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    
    [HttpPost]
    public async Task<APIResponse> EditClient([FromBody]ClientDetailModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iclientDetailService.EditClientDetails(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Delete Selected Client
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    
    [HttpPost]
    public async Task<APIResponse> DeleteClient([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iclientDetailService.DeleteClientDetails(model, id);
      }
      return apiRespone;
    }

    /// <summary>
    /// client filter
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> GetFilteredClientList([FromBody]FilterClientModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iclientDetailService.FilterClientList(model, id);
      }
      return response;
    }

    #endregion

    #region Category
    [HttpGet]
    public async Task<APIResponse> GetAllCategoryList()
    {
      APIResponse apiresponse = await _iclientDetailService.GetAllCategory();
      return apiresponse;
    }

    [HttpPost]
    public async Task<APIResponse> AddCategory([FromBody]CategoryModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        if (model.CategoryId == 0)
        {
          apiResponse = await _iclientDetailService.AddCategory(model, id);
        }
        else
        {
          apiResponse = await _iclientDetailService.EditCategory(model, id);
        }

      }
      return apiResponse;
    }
    #endregion
  }

}
