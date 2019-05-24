using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.ProjectManagement;
using HumanitarianAssistance.ViewModels.Models.Project;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebAPI.Controllers.Project
{

  [Produces("application/json")]
  [Route("api/ProjectPeople/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class ProjectPeopleController : Controller
  {
    private readonly UserManager<AppUser> _userManager;

    private IProjectPeopleService _iProjectPeopleService;

    public ProjectPeopleController(
            UserManager<AppUser> userManager,
            IProjectPeopleService iProjectPeopleService
            )
    {
      _userManager = userManager;
      _iProjectPeopleService = iProjectPeopleService;
    }


    [HttpPost]
    public async Task<APIResponse> GetOpportunityControlList([FromBody] long projectId)
    {
      APIResponse response = await _iProjectPeopleService.GetOpportunityControlList(projectId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> AddOpportunityControl([FromBody] OpportunityControlAddModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.AddOpportunityControl(model, user.Id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> EditOpportunityControl([FromBody] OpportunityControlEditModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.EditOpportunityControl(model, user.Id);
      }
      return response;
    }




    [HttpPost]
    public async Task<APIResponse> GetLogisticsControlList([FromBody] long projectId)
    {
      APIResponse response = await _iProjectPeopleService.GetLogisticsControlList(projectId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> AddLogisticsControl([FromBody] LogisticsControlAddModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.AddLogisticsControl(model, user.Id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> EditLogisticsControl([FromBody] LogisticsControlEditModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.EditLogisticsControl(model, user.Id);
      }
      return response;
    }




    [HttpPost]
    public async Task<APIResponse> GetActivitiesControl([FromBody] long projectId)
    {
      APIResponse response = await _iProjectPeopleService.GetActivitiesControlList(projectId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> AddActivitiesControl([FromBody] ActivitiesControlAddModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.AddActivitiesControl(model, user.Id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> EditActivitiesControl([FromBody] ActivitiesControlEditModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.EditActivitiesControl(model, user.Id);
      }
      return response;
    }



    [HttpPost]
    public async Task<APIResponse> GetHiringControl([FromBody] long projectId)
    {
      APIResponse response = await _iProjectPeopleService.GetHiringControlList(projectId);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> AddHiringControl([FromBody] HiringControlAddModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.AddHiringControl(model, user.Id);
      }
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> EditHiringControl([FromBody] HiringControlEditModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iProjectPeopleService.EditHiringControl(model, user.Id);
      }
      return response;
    }

  }
}
