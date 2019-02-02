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
  [Route("api/Policy/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class PolicyController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IPolicyService _iPolicyService;
    public PolicyController(UserManager<AppUser> userManager, IPolicyService iPolicyService)
    {
      _userManager = userManager;
      _iPolicyService = iPolicyService;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    [HttpPost]
    public async Task<APIResponse> AddEditPolicy([FromBody]PolicyModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.AddEditPolicy(model, id);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<APIResponse> GetPolicyList()
    {
      APIResponse apiRespone = null;      
      apiRespone = await _iPolicyService.GetAllPolicyList();
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeletePolicy([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.DeletePolicy(model, id);
      }
      return apiRespone;
    }

  }
}
