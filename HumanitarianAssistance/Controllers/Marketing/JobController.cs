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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Marketing
{
  [Produces("application/json")]
  [Route("api/Job/[Action]")]
  public class JobController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private IJobDetailsService _iJobDetailsService;
    private IMasterPageService _iMasterPageService;

    public JobController(UserManager<AppUser> userManager, IJobDetailsService iJobDetailsService, IMasterPageService iMasterPageService)
    {
      _userManager = userManager;
      _iJobDetailsService = iJobDetailsService;
      _iMasterPageService = iMasterPageService;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    /// <summary>
    /// Get all Jobs List
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetJobsList()
    {
      APIResponse apiRespone = null;
      //var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      //if (user != null)
      //{
        //var id = user.Id;
        apiRespone = await _iJobDetailsService.GetAllJobDetails();
      //}
      return apiRespone;
    }

    #region Job Details

      /// <summary>
      /// Add And Update New Job
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      ///
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEditJobDetail([FromBody]JobDetailsModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.AddEditJobDetail(model, id);
      }
      return apiRespone;
    }

    /// <summary>
    /// Delete Selected Job
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteJobDetail([FromBody]JobDetails model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        apiRespone = await _iJobDetailsService.DeleteJobDetail(model);
      }
      return apiRespone;
    }
    #endregion

    #region Job Phase

    /// <summary>
    /// get all phase list
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllPhaseList()
    {
      APIResponse apiresponse = await _iMasterPageService.GetAllPhase();
      return apiresponse;
    }

    /// <summary>
    /// add new phase
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddPhase([FromBody]JobPhaseModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        if(model.JobPhaseId == null)
        {
          apiResponse = await _iMasterPageService.AddPhase(model, id);
        }
        else
        {
          apiResponse = await _iMasterPageService.EditPhase(model, id);
        }
      }
      return apiResponse;
    }

    /// <summary>
    /// edit selected phase
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditPhase([FromBody]JobPhaseModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.EditPhase(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// delete selected phase
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeletePhase([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iMasterPageService.DeletePhase(model, id);
      }
      return apiRespone;
    }

    #endregion

  }
}
