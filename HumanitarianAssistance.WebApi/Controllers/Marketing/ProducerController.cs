using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Marketing
{
  [Produces("application/json")]
  [Route("api/Producer/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class ProducerController : Controller
    {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IMasterPageService _iMasterPageService;
    public ProducerController(UserManager<AppUser> userManager, IMasterPageService iMasterPageService)
    {
      _userManager = userManager;
      _iMasterPageService = iMasterPageService;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }
    [HttpPost]
    public async Task<APIResponse> GetProducerById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetProducerById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get Activity Type List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllProducerList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllProducers();
      return apiresponse;
    }

    /// <summary>
    /// Add New Activity Type
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddProducer([FromBody]ProducerModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditProducer(model, id);     
      }
      return apiResponse;
    }   

    /// <summary>
    /// Delete Selected Activity Type
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteProducer([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteProducer(model, id);
      }
      return apiRespone;
    }
  }
}
