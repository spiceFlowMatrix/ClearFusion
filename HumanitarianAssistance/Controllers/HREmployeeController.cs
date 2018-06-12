using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HumanitarianAssistance.ViewModels.Models;
using System.Security.Claims;
using HumanitarianAssistance.ViewModels;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/HREmployee/[Action]")]
  public class HREmployeeController : Controller
  {
    //private readonly SignInManager<AppUser> _signInManager;
    //private readonly UserManager<IdentityUser> _userManager;
    //private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    //private readonly IConfiguration _configuration;
    //private APIResponse response;
    private IHREmployee _iHREmployee;


    public HREmployeeController(
      UserManager<AppUser> userManager,
      IHREmployee iHREmployee
    )
    {
      _userManager = userManager;
      _iHREmployee = iHREmployee;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddNewEmployee([FromBody]EmployeeDetailModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
        apiRespone = await _iHREmployee.AddNewEmployee(model);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeDetail([FromBody] EmployeeDetailModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
        apiResponse = await _iHREmployee.EditEmployeeDetail(model);
      }
      return apiResponse;
    }

    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public async Task<object> GetAllEmployeeDetail()
    //{
    //  APIResponse apiresponse = await _iHREmployee.GetAllEmployeeDetail();
    //  return apiresponse;
    //}

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddJobHiringDetail([FromBody]JobHiringDetailsModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
        apiRespone = await _iHREmployee.AddJobHiringDetail(model);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditJobHiringDetail([FromBody] JobHiringDetailsModel model)
    {
      APIResponse apiResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
        apiResponse = await _iHREmployee.EditJobHiringDetail(model);
      }
      return apiResponse;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllJobHiringDetails()
    {
      APIResponse apiresponse = await _iHREmployee.GetAllJobHiringDetails();
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddInterviewScheduleDetails([FromBody]List<InterviewScheduleModel> model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iHREmployee.AddInterviewScheduleDetails(model, id);
      }
      return apiRespone;
    }

    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public async Task<object> GetAllProspectiveEmployee()
    //{
    //  APIResponse apiresponse = await _iHREmployee.GetAllProspectiveEmployee();
    //  return apiresponse;
    //}

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeDetailsByEmployeeId(int EmployeeId)
    {
      APIResponse apiresponse = await _iHREmployee.GetEmployeeDetailsByEmployeeId(EmployeeId);
      return apiresponse;
    }

    //[HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public async Task<object> AddInterviewFeedbackDetails([FromBody] InterviewFeedbackDetailsModel model)
    //{
    //  APIResponse apiRespone = null;
    //  var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //  if (user != null)
    //  {
    //    var id = user.Id;
    //    model.CreatedById = id;
    //    model.CreatedDate = DateTime.UtcNow;
    //    model.IsDeleted = false;
    //    apiRespone = await _iHREmployee.AddInterviewFeedbackDetails(model);
    //  }
    //  return apiRespone;
    //}

    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public async Task<object> GetAllInterviewFeedbackByScheduleId(int ScheduleId)
    //{
    //  APIResponse apiresponse = await _iHREmployee.GetAllInterviewFeedbackByScheduleId(ScheduleId);
    //  return apiresponse;
    //}

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeSalaryDetail([FromBody] List<EmployeePayrollModel> model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        apiRespone = await _iHREmployee.AddEmployeeSalaryDetail(model, id);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeSalaryDetail([FromBody] List<EmployeePayrollModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.EditEmployeeSalaryDetail(model,id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeSalaryDetailsByEmployeeId(int employeeid)
    {
      APIResponse apiresponse = await _iHREmployee.GetEmployeeSalaryDetailsByEmployeeId(employeeid);
      return apiresponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddDocumentDetail([FromBody] EmployeeDocumentDetailModel model)
    {
      APIResponse aPIResponse = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        aPIResponse = await _iHREmployee.AddDocumentDetail(model);
      }
      return aPIResponse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> DeleteDocumentDetail(int DocumentId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var userid = user.Id;
        response = await _iHREmployee.DeleteDocumentDetail(DocumentId, userid);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllDocumentDetailByEmployeeId(int EmployeeId)
    {
      APIResponse response = await _iHREmployee.GetAllDocumentDetailByEmployeeId(EmployeeId);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeHistoryDetail([FromBody] EmployeeHistoryDetailModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.AddEmployeeHistoryDetail(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeHistoryDetail([FromBody] EmployeeHistoryDetailModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.EditEmployeeHistoryDetail(model);
      }
      return response;
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> DeleteEmployeeHistoryDetail(int HistoryId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var userid = user.Id;
        response = await _iHREmployee.DeleteEmployeeHistoryDetail(HistoryId, userid);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeHistoryByEmployeeId(int EmployeeId)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeeHistoryByEmployeeId(EmployeeId);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeProfessionalDetail([FromBody] EmployeeProfessionalDetailModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.AddEmployeeProfessionalDetail(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeProfessionalDetail([FromBody] EmployeeProfessionalDetailModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.EditEmployeeProfessionalDetail(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddPayrollMonthlyHourDetail([FromBody] PayrollMonthlyHourDetailModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        apiRespone = await _iHREmployee.AddPayrollMonthlyHourDetail(model);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditPayrollMonthlyHourDetail([FromBody] PayrollMonthlyHourDetailModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        apiRespone = await _iHREmployee.EditPayrollMonthlyHourDetail(model);
      }
      return apiRespone;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllPayrollMonthlyHourDetail()
    {
      APIResponse response = await _iHREmployee.GetAllPayrollMonthlyHourDetail();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllEmployeeAssignLeave(int EmployeeId)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeeAssignLeave(EmployeeId);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AssignLeaveToEmployeeDetail([FromBody] AssignLeaveToEmployeeModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        apiRespone = await _iHREmployee.AssignLeaveToEmployeeDetail(model);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditAssignLeaveToEmployee([FromBody] AssignLeaveToEmployeeModel model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        apiRespone = await _iHREmployee.EditAssignLeaveToEmployee(model);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeAttendanceDetails([FromBody] List<EmployeeAttendanceModel> modellist)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.AddEmployeeAttendanceDetails(modellist, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeProfessionalDetail(int EmployeeId)
    {
      APIResponse response = await _iHREmployee.GetEmployeeProfessionalDetail(EmployeeId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeDetail(int EmployeeType, int OfficeId)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeeDetail(EmployeeType, OfficeId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeAttendanceDetails(int employeeid)
    {
      APIResponse response = await _iHREmployee.GetEmployeeAttendanceDetails(employeeid);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeHealthDetail([FromBody] EmployeeHealthInformationModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.AddEmployeeHealthDetail(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeHealthDetail([FromBody] EmployeeHealthInformationModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.EditEmployeeHealthDetail(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeHealthDetailByEmployeeId(int employeeid)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeeHealthDetailByEmployeeId(employeeid);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeGeneralInformationById(int EmployeeId)
    {
      APIResponse response = await _iHREmployee.GetEmployeeGeneralInformationById(EmployeeId);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> ChangeEmployeeImage([FromBody] ChangeEmployeeImage model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.ChangeEmployeeImage(model);
      }
      return response;
    }

    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public async Task<object> GetAllActiveEmployeeForAttendance()
    //{
    //  APIResponse response = await _iHREmployee.GetAllActiveEmployeeForAttendance();
    //  return response;
    //}

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeesAttendanceByDate(string SelectedDate, int OfficeId, bool attendancestatus)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeesAttendanceByDate(SelectedDate, OfficeId, attendancestatus);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeAttendanceByDate([FromBody] EmployeeAttendanceModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.EditEmployeeAttendanceByDate(model, id);
      }
      return response;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetProspectiveEmployeesByProfessionId(int ProfessionId)
    {
      APIResponse response = await _iHREmployee.GetProspectiveEmployeesByProfessionId(ProfessionId);
      return response;
    }

    //Alpit

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllScheduledProspectiveEmployee()
    {
      APIResponse response = await _iHREmployee.GetAllScheduledProspectiveEmployee();
      return response;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllJobGrade()
    {
      APIResponse response = await _iHREmployee.GetAllJobGrade();
      return response;
    }


    /// <summary>
    /// TODO: Get all Scheduled Employees
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllScheduledEmployeeList()
    {
      APIResponse response = await _iHREmployee.GetAllScheduledEmployeeList();
      return response;
    }


    /// <summary>
    /// TODO: Get all Approved Finalized Employees
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllApprovedEmployeeList()
    {
      APIResponse response = await _iHREmployee.GetAllApprovedEmployeeList();
      return response;
    }


    /// <summary>
    /// TODO: Update columns according approvals
    /// </summary>
    /// <param name="model"></param>
    /// <param name="approvalId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> InterviewApprovals([FromBody] List<InterviewScheduleModel> model, int approvalId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.InterviewApprovals(model, approvalId, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeApplyLeaveDetail([FromBody] List<EmployeeApplyLeaveModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.AddEmployeeApplyLeaveDetail(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetEmployeeApplyLeaveDetailById(int employeeid)
    {
      APIResponse response = await _iHREmployee.GetEmployeeApplyLeaveDetailById(employeeid);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeApplyLeaveList(int OfficeId)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeeApplyLeaveList(OfficeId);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> ApproveEmployeeLeave([FromBody]List<ApproveLeaveModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.ApproveEmployeeLeave(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> RejectEmployeeLeave([FromBody]List<ApproveLeaveModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.RejectEmployeeLeave(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> DeleteApplyEmployeeLeave(int applyleaveid)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.DeleteApplyEmployeeLeave(applyleaveid, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeMonthlyPayrollListApproved(int officeid, int currencyid, int month, int year, int paymentType)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeeMonthlyPayrollListApproved(officeid, currencyid, month, year, paymentType);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeMonthlyPayrollList(int officeid, int currencyid, int month, int year, int paymentType)
    {
      APIResponse response = await _iHREmployee.GetAllEmployeeMonthlyPayrollList(officeid, currencyid, month, year, paymentType);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddHolidayDetails([FromBody] HolidayDetailsModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.AddHolidayDetails(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditHolidayDetails([FromBody] HolidayDetailsModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.EditHolidayDetails(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllHolidayDetails(int OfficeId)
    {
      APIResponse response = await _iHREmployee.GetAllHolidayDetails(OfficeId);
      return response;
    }



    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddJobGradeDetail([FromBody]JobGradeModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.AddJobGradeDetail(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditJobGradeDetail([FromBody]JobGradeModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.EditJobGradeDetail(model);
      }
      return response;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditEmployeeAssignLeave([FromBody] EditAssignLeaveToEmployeeModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _iHREmployee.EditEmployeeAssignLeave(model);
      }
      return response;
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> DeleteHolidayDetails(long holidayId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.DeleteHolidayDetails(holidayId, user.Id);
      }
      return response;
    }

    [HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> OnPostExport()
    {
      APIResponse response = await _iHREmployee.OnPostExport();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllDisableCalanderDate(int employeeid, int officeid)
    {
      APIResponse response = await _iHREmployee.GetAllDisableCalanderDate(employeeid, officeid);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> MonthlyEmployeeAttendanceReport([FromQuery] MonthlyEmployeeAttendanceReportModel model)
    {
      APIResponse response = await _iHREmployee.MonthlyEmployeeAttendanceReport(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllDateforDisableCalenderDate(int officeid)
    {
      APIResponse response = await _iHREmployee.GetAllDateforDisableCalenderDate(officeid);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllHolidayWeeklyDetails(int OfficeId)
    {
      APIResponse response = await _iHREmployee.GetAllHolidayWeeklyDetails(OfficeId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EmployeesSalarySummary([FromQuery]EmployeeSummaryModel model)
    {
      APIResponse response = await _iHREmployee.EmployeesSalarySummary(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EmployeePaymentTypeReport([FromBody]List<EmployeePaymentTypeModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.EmployeePaymentTypeReport(model, user.Id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EmployeePaymentTypeReportForSaveOnly([FromBody]List<EmployeePaymentTypeModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.EmployeePaymentTypeReportForSaveOnly(model, user.Id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> RemoveApprovedList([FromBody]RemoveApprovedEmployee model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.RemoveApprovedList(model, user.Id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EmployeePensionReport([FromQuery]PensionReportModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.EmployeePensionReport(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeProjects(int employeeid)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.GetAllEmployeeProjects(employeeid);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AssignEmployeeProjectPercentage([FromBody]List<EmployeeProjectModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.AssignEmployeeProjectPercentage(model, user.Id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetExchangeRate([FromBody]ExchangeRateModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.GetExchangeRate(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeContractType()
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.GetAllEmployeeContractType();
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> SaveContractContent([FromBody]ContractTypeModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.SaveContractContent(model, user.Id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllContractTypeContent([FromQuery]int officeId, int EmployeeContractTypeId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.GetAllContractTypeContent(officeId, EmployeeContractTypeId);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetSelectedEmployeeContract([FromQuery]int OfficeId, int ProjectId, int BudgetLineId, int EmployeeId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.GetSelectedEmployeeContract(OfficeId, ProjectId, BudgetLineId, EmployeeId);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeeSalaryDetails([FromQuery]int OfficeId, int year, int month, int EmployeeId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.GetEmployeeSalaryDetails(OfficeId, year, month, EmployeeId);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EmployeeTaxCalculation([FromQuery]int OfficeId, int EmployeeId, int FinancialYearId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.EmployeeTaxCalculation(OfficeId, EmployeeId, FinancialYearId);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EmployeeSalaryTaxDetails([FromBody]SalaryTaxModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iHREmployee.EmployeeSalaryTaxDetails(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddAdvances([FromBody]AdvancesModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.AddAdvances(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditAdvances([FromBody] AdvancesModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.EditAdvances(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllAdvancesByOfficeId([FromQuery] int OfficeId, int month, int year)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.GetAllAdvancesByOfficeId(OfficeId, month, year);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> ApproveAdvances([FromBody]AdvancesModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.ApproveAdvances(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> RejectAdvances([FromBody]AdvancesModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.RejectAdvances(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddInterviewDetails([FromBody]InterviewDetailModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.AddInterviewDetails(model, id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> EditInterviewDetails([FromBody]InterviewDetailModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.EditInterviewDetails(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllInterviewDetails()
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.GetAllInterviewDetails();
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddTechnicalQuestions([FromBody]InterviewTechnicalQuestionsModel model)
    {
      APIResponse response = new APIResponse();
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iHREmployee.AddTechnicalQuestions(model, id);
      }
      return response;
    }
  }
}
