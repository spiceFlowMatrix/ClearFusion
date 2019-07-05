//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using DataAccess.DbEntities;
//using HumanitarianAssistance.Common.Helpers;
//using HumanitarianAssistance.Service.APIResponses;
//using HumanitarianAssistance.Service.interfaces;
//using HumanitarianAssistance.ViewModels.Models;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace HumanitarianAssistance.WebApi.Controllers
//{
//    [Produces("application/json")]
//    [Route("api/ProjectPipeLining/[Action]")]
//    public class ProjectPipeLiningController : Controller
//    {
//      private readonly JsonSerializerSettings _serializerSettings;
//      private readonly UserManager<AppUser> _userManager;
//      private IProjectPipeLining _iprojectPipeLining;

//    public ProjectPipeLiningController(
//        UserManager<AppUser> userManager,
//        IProjectPipeLining iprojectPipeLining
//      )
//      {
//        _userManager = userManager;
//      _iprojectPipeLining = iprojectPipeLining;
//      _serializerSettings = new JsonSerializerSettings
//        {
//          Formatting = Formatting.Indented,
//          NullValueHandling = NullValueHandling.Ignore
//        };
//      }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> AddBudgetLine([FromBody] ProjectBudgetLineModel model)
//    {
//      APIResponse response;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.CreatedById = id;
//          model.IsDeleted = false;
//          model.CreatedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.AddBudgetLine(model);
//        return response;
//      }
//      response = new APIResponse();
//      response.StatusCode = StaticResource.failStatusCode;
//      response.Message = StaticResource.SomethingWentWrong;
//      return response;

//    }
//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetBudgetLines(long projectId)
//    {
//      APIResponse response = await _iprojectPipeLining.GetProjectLineDetail(projectId);
//      return response;
//    }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> EditProjectLineDetail([FromBody]ProjectBudgetLineModel model)
//    {

//      APIResponse apiRespone = null;
//      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//      if (user != null)
//      {
//        var id = user.Id;
//        model.ModifiedById = id;
//        model.ModifiedDate = DateTime.UtcNow;
//        model.IsDeleted = false;
//        apiRespone = await _iprojectPipeLining.EditProjectLineDetail(model);
//      }

//      return apiRespone;
//    }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> AddReceivable([FromBody] BudgetReceivableModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.CreatedById = id;
//          model.IsDeleted = false;
//          model.CreatedDate = DateTime.UtcNow;
//        }
//        response=  await _iprojectPipeLining.AddBudgetRecivable(model);
//      }
//      else
//      {
//        response.Message = StaticResource.SomethingWentWrong;
//        response.StatusCode = StaticResource.failStatusCode;
//      }
//      return response;
//    }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> EditRecivable([FromBody] BudgetReceivableModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.ModifiedById = id;
//          model.IsDeleted = false;
//          model.ModifiedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.EditBudgetReceivable(model);
//      }
//      else
//      {
//        response.Message = StaticResource.SomethingWentWrong;
//        response.StatusCode = StaticResource.failStatusCode;
//      }
//      return response;
//    }

//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetBudgetLineReceivable(long projectId,long budgetlineid)
//    {
//      APIResponse response = await  _iprojectPipeLining.GetBudgetRecivable(projectId, budgetlineid);
//      return response;
//    }
//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> AddBudgetReceivedAmount([FromBody] BudgetReceivedAmountModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.CreatedById = id;
//          model.IsDeleted = false;
//          model.CreatedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.AddBudgetLineReceived(model);
//      }
//      else
//      {
//        response.Message = StaticResource.SomethingWentWrong;
//        response.StatusCode = StaticResource.failStatusCode;
//      }
//      return response;
//    }
//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> EditBudgetReceivedAmount([FromBody] BudgetReceivedAmountModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.ModifiedById = id;
//          model.IsDeleted = false;
//          model.ModifiedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.EditBudgetLineReceived(model);

//      }
//      return response;
//    }
//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetBudgetReceived(long projectId,long budgetLineId,long receivableId)
//    {
//      APIResponse response = await _iprojectPipeLining.GetBudgetReceived(projectId, budgetLineId, receivableId);
//      return response;
//    }

//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetAllProjectBudgetLineByProjectId(int ProjectId)
//    {
//      APIResponse response = await _iprojectPipeLining.GetAllProjectBudgetLineByProjectId(ProjectId);
//      return response;
//    }
//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetBudgetLinePayable(long projectId, long budgetlineid)
//    {
//      APIResponse response = await _iprojectPipeLining.GetBudgetPayable(projectId, budgetlineid);
//      return response;
//    }
//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> AddBudgetLinePayable([FromBody] BudgetPayableModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.CreatedById = id;
//          model.IsDeleted = false;
//          model.CreatedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.AddBudgetPayable(model);
//      }
//      else
//      {
//        response.Message = StaticResource.SomethingWentWrong;
//        response.StatusCode = StaticResource.failStatusCode;
//      }
//      return response;
//    }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> EditBudgetLinePayable([FromBody] BudgetPayableModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.ModifiedById = id;
//          model.IsDeleted = false;
//          model.ModifiedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.EditBudgetPayable(model);
//      }
//      else
//      {
//        response.Message = StaticResource.SomethingWentWrong;
//        response.StatusCode = StaticResource.failStatusCode;
//      }
//      return response;
//    }

//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetBudgetLinePaid(long projectId, long budgetLineId, long payableId)
//    {
//      APIResponse response = await _iprojectPipeLining.GetBudgetPaidAmount(projectId, budgetLineId,payableId);
//      return response;
//    }
//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> AddBudgetLinePaidAmount([FromBody] BudgetPayableAmountModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.CreatedById = id;
//          model.IsDeleted = false;
//          model.CreatedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.AddBudgetLinePaid(model);
//      }
//      else
//      {
//        response.Message = StaticResource.SomethingWentWrong;
//        response.StatusCode = StaticResource.failStatusCode;
//      }
//      return response;
//    }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> EditBudgetLinePaidAmount([FromBody] BudgetPayableAmountModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.ModifiedById = id;
//          model.IsDeleted = false;
//          model.ModifiedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.EditBudgetLinePaid(model);

//      }
//      return response;
//    }

//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetBudgetLineSummary(long projectId)
//    {
//      APIResponse apiResponse = await _iprojectPipeLining.GetBudgetSummary(projectId);
//      return apiResponse;

//    }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> AddProjectDocument([FromBody] ProjectDocumentModel model)
//    {
//      APIResponse response = null;
//      if (ModelState.IsValid)
//      {
//        var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//        if (user != null)
//        {
//          var id = user.Id;
//          model.CreatedById = id;
//          model.IsDeleted = false;
//          model.CreatedDate = DateTime.UtcNow;
//        }
//        response = await _iprojectPipeLining.AddProjectDocument(model);
//      }
//      else
//      {
//        response.Message = StaticResource.SomethingWentWrong;
//        response.StatusCode = StaticResource.failStatusCode;
//      }
//      return response;
//    }

//    [HttpDelete]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> DeleteProjectDocument(int projectdocumentid)
//    {
//      APIResponse response = null;
//      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//      if (user != null)
//      {
//        var id = user.Id;
//        response = await _iprojectPipeLining.DeleteProjectDocument(projectdocumentid, id);
//      }
//      return response;
//    }

//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetProjectDocumentDetail(int projectid)
//    {
//      APIResponse response = await _iprojectPipeLining.GetProjectDocumentDetail(projectid);
//      return response;
//    }

//    [HttpPost]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> AssignEmployeeToBudgetLine([FromBody]List<BudgetLineEmployeesModel> model)
//    {
//      APIResponse response = new APIResponse();
//      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
//      if (user != null)
//      {
//        var id = user.Id;
//        response = await _iprojectPipeLining.AssignEmployeeToBudgetLine(model, id);
//      }      
//      return response;
//    }

//    [HttpGet]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
//    public async Task<APIResponse> GetAssignedEmployeesInBudgetLine(int OfficeId, int ProjectId, int BudgetLineId)
//    {
//      APIResponse response = await _iprojectPipeLining.GetAssignedEmployeesInBudgetLine(OfficeId, ProjectId, BudgetLineId);
//      return response;
//    }
//  }
//}
