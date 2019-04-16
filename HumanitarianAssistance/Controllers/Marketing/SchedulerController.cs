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
  [Route("api/Scheduler/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class SchedulerController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private ISchedulerService _iScheduleService;
    private IMasterPageService _iMasterPageService;
    public SchedulerController(UserManager<AppUser> userManager, ISchedulerService iSchedulerService, IMasterPageService iMasterPageService)
    {
      _userManager = userManager;
      _iScheduleService = iSchedulerService;
      _iMasterPageService = iMasterPageService;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    [HttpPost]
    public async Task<APIResponse> GetAllPolicyScheduleList(string text)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iScheduleService.GetAllPolicyScheduleList();
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetScheduleDetailsById([FromBody] int model)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iScheduleService.GetScheduleDetailsById(model);
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetChannelById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetChannelById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Get Activity Type List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<APIResponse> GetAllChannelList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllChannels();
      return apiresponse;
    }

    /// <summary>
    /// Add New Activity Type
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> AddChannel([FromBody]ChannelModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditChannel(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// Delete Selected Activity Type
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<APIResponse> DeleteChannel([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeleteChannel(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllChannelListByMedium([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        apiRespone = await _iMasterPageService.GetChannelListByMediumId(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddSchedule([FromBody]SchedulerModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iScheduleService.AddEditSchedule(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> DeleteSchedule([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iScheduleService.DeleteSchedule(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetAllScheduleList(string text)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iScheduleService.GetAllScheduleList();
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> FilterScheduleList([FromBody]FilterSchedulerModel mediumId)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iScheduleService.FilterScheduleList(mediumId);
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> AddPlayoutMinutes([FromBody]PlayoutMinutesModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iScheduleService.AddEditPlayoutMinutes(model, id);
      }     
      return apiRespone;
    }

  }
}
