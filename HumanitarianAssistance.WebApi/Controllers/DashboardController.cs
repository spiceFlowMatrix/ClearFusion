
using Microsoft.Extensions.Configuration;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HumanitarianAssistance.Service.APIResponses;

namespace HumanitarianAssistance.WebApi.Controllers
{
  [Produces("application/json")]
  [Route("api/Dashboard/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class DashboardController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private IDashboardService _iDashboard;
    private IConfiguration _configuration;
    public DashboardController(
         UserManager<AppUser> userManager,
         IDashboardService iDashboard,
         IConfiguration configuration
        )
    {
      _configuration = configuration;
      _userManager = userManager;
      _iDashboard = iDashboard;
    }

    //[HttpGet]
    //public APIResponse GetTrainingLink()
    //{
    //  APIResponse response = _iDashboard.GetTrainingLink();
    //  return response;
    //}

    [HttpGet]
    public APIResponse GetTrainingLink()
    {
      APIResponse response = new APIResponse();
      if (_configuration["DOCS_URL"] != null)
      {
        response.StatusCode = 200;
        response.data.TrainingLink = _configuration["DOCS_URL"].ToString();
        response.Message = "Success";
      }
      else
      {
        response.StatusCode = 400;
        response.data.TrainingLink = null;
        response.Message = "Training link not provided";
      }
      return response;

      //if (_configuration["DOCS_URL"] != null)
      //{
      //  return Ok(_configuration["DOCS_URL"].ToString());
      //}
      //else
      //{
      //  return NotFound();
      //}
    }
  }
}
