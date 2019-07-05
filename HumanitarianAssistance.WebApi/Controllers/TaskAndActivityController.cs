using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/TaskAndActivity/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class TaskAndActivityController : Controller
    {
      private readonly JsonSerializerSettings _serializerSettings;
      private readonly UserManager<AppUser> _userManager;
      private ITaskAndActivity _taskAndActivity;

      public TaskAndActivityController(
        UserManager<AppUser> userManager,
        ITaskAndActivity taskAndActivity
      )
      {
      _userManager = userManager;
      _taskAndActivity = taskAndActivity;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    [HttpPost]
    public async Task<object> AddTask([FromBody]TaskMasterModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
        apiRespone = await _taskAndActivity.AddTask(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<object> EditTask([FromBody]TaskMasterModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
        apiRespone = await _taskAndActivity.EditTask(model);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<object> GetAllTask(int projectid)
    {
      APIResponse response = await _taskAndActivity.GetAllTask(projectid);
      return response;
    }

    [HttpPost]
    public async Task<object> AddActivityDetail([FromBody]ActivityMasterModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
        apiRespone = await _taskAndActivity.AddActivityDetail(model);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<object> EditActivityDetail([FromBody]ActivityMasterModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
        apiRespone = await _taskAndActivity.EditActivityDetail(model);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<object> GetAllActivity()
    {
      APIResponse response = await _taskAndActivity.GetAllActivity();
      return response;
    }

    [HttpPost]
    public async Task<object> AddAssignActivityDetail([FromBody]AssignActivityModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
        apiRespone = await _taskAndActivity.AddAssignActivityDetail(model);
      }
      return apiRespone;
    }

    [HttpGet]
    public async Task<object> GetAllAssignActivityDetailByCondition(long? ProjectId, int? TaskId, int? ActivityId)
    {
      APIResponse response = await _taskAndActivity.GetAllAssignActivityDetailByCondition(ProjectId, TaskId, ActivityId);
      return response;
    }

  }
}
