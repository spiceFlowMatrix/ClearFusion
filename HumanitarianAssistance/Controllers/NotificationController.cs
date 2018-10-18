using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/Notification/[Action]")]
  public class NotificationController : Controller
  {

    private readonly UserManager<AppUser> _userManager;
    private INotificationManager _iNotificationManager;

    public NotificationController(
     UserManager<AppUser> userManager,
     INotificationManager iNotificationManager
     )
    {
      _userManager = userManager;
      _iNotificationManager = iNotificationManager;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> SetNotificationIsReadFlag(int loggerDetailsId)
    {
      APIResponse apiRespone = null;
      apiRespone = await _iNotificationManager.SetNotificationIsReadFlag(loggerDetailsId);
      return apiRespone;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetNotificationIsReadCount()
    {
      APIResponse apiRespone = null;
      apiRespone = await _iNotificationManager.GetNotificationIsReadCount();
      return apiRespone;
    }



  }
}
