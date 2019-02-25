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

    [HttpPost]
    public async Task<APIResponse> GetFilteredPolicylist([FromBody]PolicyFilterModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.FilterPolicyList(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetPolicyPaginatedList([FromBody]PolicyPaginationModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetPolicyPaginatedList(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetPolicyById([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetPolicyById(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> PolicySchedules([FromBody]ScheduleDetailsModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.AddEditPolicySchedules(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetPolicyScheduleById([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetPolicyScheduleById(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddSchedule([FromBody]ScheduleDetailsModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.AddEditPolicySchedules(model, id);
      }
      return apiRespone;
    }


    [HttpGet]
    public async Task<APIResponse> GetAllSchedule()
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetAllSchedule(id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetScheduleByDate([FromBody]string model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetScheduleByDate(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddEditPolicyTimeSchedule([FromBody] PolicyTimeScheduleModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.AddEditPolicyTimeSchedule(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetPolicyTimeScheduleList([FromBody] int Id)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetPolicyTimeScheduleList(Id, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeletePolicyTimeSchedule([FromBody] int Id)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.DeletePolicyTimeSchedule(Id, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetPolicyTimeScheduleById([FromBody] int Id)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetPolicyTimeScheduleById(Id, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddPolicyRepeatDays([FromBody] PolicyTimeModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.AddPolicyRepeatDays(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetDayScheduleByPolicyId([FromBody] int Id)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iPolicyService.GetDayScheduleByPolicyId(Id, id);
      }
      return apiRespone;
    }
  }
}
