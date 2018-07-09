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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [Route("api/EmployeeDetail/[Action]")]
  public class EmployeeDetailController : Controller
  {
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;

    private IEmployeeDetail _iEmployeeDetail;
    public EmployeeDetailController(
       UserManager<AppUser> userManager,
      IEmployeeDetail iEmployeeDetail
      )
    {
      _userManager = userManager;
      _iEmployeeDetail = iEmployeeDetail;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    #region "Employee History Outside Organization"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeHistoryOutsideOrganization([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeHistoryOutsideOrganization(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeHistoryOutsideOrganization([FromBody]EmployeeHistoryOutsideOrganizationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;        
        apiResponse = await _iEmployeeDetail.AddEmployeeHistoryOutsideOrganization(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeHistoryOutsideOrganization([FromBody]EmployeeHistoryOutsideOrganizationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeHistoryOutsideOrganization(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeHistoryOutsideOrganization([FromBody]EmployeeHistoryOutsideOrganizationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeHistoryOutsideOrganization(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee History Outside Country"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeHistoryOutsideCountry([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeHistoryOutsideCountry(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeHistoryOutsideCountry([FromBody]EmployeeHistoryOutsideOrganizationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeHistoryOutsideCountry(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeHistoryOutsideCountry([FromBody]EmployeeHistoryOutsideOrganizationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeHistoryOutsideCountry(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeHistoryOutsideCountry([FromBody]EmployeeHistoryOutsideOrganizationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeHistoryOutsideCountry(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee Relative Information"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeRelativeInformation([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeRelativeInformation(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeRelativeInformation([FromBody]EmployeeRelativeInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeRelativeInformation(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeRelativeInformation([FromBody]EmployeeRelativeInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeRelativeInformation(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeRelativeInformation([FromBody]EmployeeRelativeInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeRelativeInformation(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee Info References"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeInfoReferences([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeInfoReferences(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeInfoReferences([FromBody]EmployeeRelativeInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeInfoReferences(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeInfoReferences([FromBody]EmployeeRelativeInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeInfoReferences(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeInfoReferences([FromBody]EmployeeRelativeInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeInfoReferences(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee Other Skills"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeOtherSkills([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeOtherSkills(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeOtherSkills([FromBody]EmployeeOtherSkillsModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeOtherSkills(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeOtherSkills([FromBody]EmployeeOtherSkillsModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeOtherSkills(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeOtherSkills([FromBody]EmployeeOtherSkillsModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeOtherSkills(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee Salary Budgets"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeSalaryBudgets([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeSalaryBudgets(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeSalaryBudgets([FromBody]EmployeeSalaryBudgetModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeSalaryBudgets(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeSalaryBudgets([FromBody]EmployeeSalaryBudgetModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeSalaryBudgets(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeSalaryBudgets([FromBody]EmployeeSalaryBudgetModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeSalaryBudgets(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee Educations"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeEducations([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeEducations(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeEducations([FromBody]EmployeeEducationsModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeEducations(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeEducations([FromBody]EmployeeEducationsModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeEducations(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeEducations([FromBody]EmployeeEducationsModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeEducations(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee Salary Analytical Info"

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeSalaryAnalyticalInfo([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeSalaryAnalyticalInfo(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeSalaryAnalyticalInfo([FromBody]EmployeeSalaryAnalyticalInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeSalaryAnalyticalInfo(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeSalaryAnalyticalInfo([FromBody]EmployeeSalaryAnalyticalInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeSalaryAnalyticalInfo(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteEmployeeSalaryAnalyticalInfo([FromBody]EmployeeSalaryAnalyticalInfoModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.DeleteEmployeeSalaryAnalyticalInfo(model, id);
      }
      return apiResponse;
    }

    #endregion

    #region "Employee Health"


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeHealthInfo([FromQuery]int EmployeeId)
    {
      APIResponse apiresponse = await _iEmployeeDetail.GetAllEmployeeHealthInfo(EmployeeId);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddEmployeeHealthInfo([FromBody]EmployeeHealthInformationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.AddEmployeeHealthInfo(model, id);
      }
      return apiResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeHealthInfo([FromBody]EmployeeHealthInformationModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiResponse = await _iEmployeeDetail.EditEmployeeHealthInfo(model, id);
      }
      return apiResponse;
    }

    #endregion

  }
}
