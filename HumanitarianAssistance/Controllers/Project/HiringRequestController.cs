using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.ProjectManagement;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Project
{
  [Produces("application/json")]
  [Route("api/HiringRequest/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class HiringRequestController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHiringRequestService _hiringRequestService;
    public HiringRequestController(UserManager<AppUser> userManager, IHiringRequestService hiringRequestServ)
    {
      _userManager = userManager;
      _hiringRequestService = hiringRequestServ;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    #region "GetProjectHiringRequestDetail"
    [HttpPost]
    public async Task<APIResponse> GetProjectHiringRequestDetail()
    {
      APIResponse response = await _hiringRequestService.GetallHiringRequestDetail();
      return response;
    }
    #endregion

    #region "AddHiringRequestDetail"
    [HttpPost]
    public async Task<APIResponse> AddHiringRequestDetail([FromBody]ProjectHiringRequestModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _hiringRequestService.AddProjectHiringRequest(Model, id);
      }
      return apiRespone;
    }
    #endregion

    #region "AddHiringRequestDetail"
    [HttpPost]
    public async Task<APIResponse> EditHiringRequestDetail([FromBody]ProjectHiringRequestModel Model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _hiringRequestService.EditProjectHiringRequest(Model, id);
      }
      return apiRespone;
    }
    #endregion

  }



}
