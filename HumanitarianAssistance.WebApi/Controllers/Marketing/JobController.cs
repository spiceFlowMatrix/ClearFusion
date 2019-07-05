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

namespace HumanitarianAssistance.WebApi.Controllers.Marketing
{
  [Produces("application/json")]
  [Route("api/Job/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetJobsPaginatedList([FromBody]JobPaginationModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.GetJobsPaginatedList(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> ApproveJob([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
         apiRespone = await _iJobDetailsService.ApproveJob(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GenerateInvoice([FromBody]int jobId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.GenerateInvoice(jobId, id);
      }
      return apiRespone;
    }

   


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> FetchInvoice([FromBody]int jobId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.FetchInvoice(jobId, id);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> ApproveInvoice([FromBody]int jobId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.ApproveInvoice(jobId, id);
      }
      return apiRespone;
    }


    #region Job Details

    [HttpPost]
    public async Task<APIResponse> GetJobDetailsById([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.GetJobDetailsById(model, id);
      }
      return apiRespone;
    }
    

    /// <summary>
    /// Add And Update New Job
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    ///
    [HttpPost]
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
    public async Task<APIResponse> DeleteJobDetail([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.DeleteJobDetail(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    public async Task<APIResponse> GetFilteredJoblist([FromBody]JobFilterModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.FilterJobList(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetFilteredJobslist([FromBody]FilterJobModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.FilterJobsList(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AcceptAgreement([FromBody]int model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.AcceptAgreement(model, id);
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
    public async Task<APIResponse> AddPhase([FromBody]JobPhaseModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.AddEditPhase(model, id);       
      }
      return apiResponse;
    }

    [HttpPost]
    public async Task<APIResponse> GetPhaseById([FromBody]int model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iMasterPageService.GetPhaseById(model, id);
      }
      return apiResponse;
    }

    /// <summary>
    /// delete selected phase
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
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

    #region"RemoveInvoice AS 22/06/19"
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> RemoveInvoice([FromBody]int jobId)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iJobDetailsService.RemoveInvoice(jobId, id);
      }
      return apiRespone;
    }
    #endregion
  }
}
