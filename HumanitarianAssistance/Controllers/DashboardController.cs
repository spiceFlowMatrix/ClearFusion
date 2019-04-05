using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/Dashboard/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class DashboardController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private IDashboardService _iDashboard;

    public DashboardController(
       UserManager<AppUser> userManager,
      IDashboardService iDashboard
      )
    {
      _userManager = userManager;
      _iDashboard = iDashboard;
    }

    [HttpGet]
    public APIResponse GetTrainingLink()
    {
      APIResponse response = _iDashboard.GetTrainingLink();
      return response;
    }
  }
}
