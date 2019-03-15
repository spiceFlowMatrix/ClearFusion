using System;
using System.Collections.Generic;
using System.Linq;
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
    public SchedulerController(UserManager<AppUser> userManager, ISchedulerService iSchedulerService)
    {
      _userManager = userManager;
      _iScheduleService = iSchedulerService;
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
    public async Task<APIResponse> GetScheduleDetailsById([FromBody] ScheduleDetailModel model)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iScheduleService.GetScheduleDetailsById(model);
      return apiRespone;
    }

  }
}
