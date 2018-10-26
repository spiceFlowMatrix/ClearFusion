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
  [Route("api/EmployeeHR/[Action]")]
  public class EmployeeHRController : Controller
  {
    private IEmployeeHR _iEmployeeHR;
    private readonly UserManager<AppUser> _userManager;
    private readonly JsonSerializerSettings _serializerSettings;

    public EmployeeHRController(UserManager<AppUser> userManager, IEmployeeHR iEmployeeHR)
    {
      _userManager = userManager;
      _iEmployeeHR = iEmployeeHR;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
      };
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
        response = await _iEmployeeHR.AddEmployeeAttendanceDetails(modellist, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeMonthlyPayrollListApproved(int officeid, int month, int year, int paymentType)
    {
      APIResponse response = await _iEmployeeHR.GetAllEmployeeMonthlyPayrollListApproved(officeid, month, year, paymentType);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeeMonthlyPayrollList(int officeid, int currencyid, int month, int year, int paymentType)
    {
      APIResponse response = await _iEmployeeHR.GetAllEmployeeMonthlyPayrollList(officeid, currencyid, month, year, paymentType);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> GetAllPayrollMonthlyHourDetail([FromBody] PayrollHourFilterModel model)
    {
      APIResponse response = null;
      response = await _iEmployeeHR.GetAllPayrollMonthlyHourDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<APIResponse> AddPayrollMonthlyHourDetail([FromBody] PayrollMonthlyHourDetailModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;
        response = await _iEmployeeHR.AddPayrollMonthlyHourDetail(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditPayrollMonthlyHourDetail([FromBody] PayrollMonthlyHourDetailModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;
        response = await _iEmployeeHR.EditPayrollMonthlyHourDetail(model);
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
        response = await _iEmployeeHR.EmployeePaymentTypeReportForSaveOnly(model, user.Id);
      }
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
        response = await _iEmployeeHR.EmployeePaymentTypeReport(model, user.Id);
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
        response = await _iEmployeeHR.EmployeePensionReport(model);
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
        response = await _iEmployeeHR.EmployeeSalaryTaxDetails(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetAllEmployeePension(int OfficeId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iEmployeeHR.GetAllEmployeePension(OfficeId);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetEmployeePensionHistoryDetail(int EmployeeId, int OfficeId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iEmployeeHR.GetEmployeePensionHistoryDetail(EmployeeId, OfficeId);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> AddEmployeeLeaveDetails([FromBody] List<AssignLeaveToEmployeeModel> model)
    {
      APIResponse apiRespone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;

        foreach (var item in model)
        {
          item.CreatedById = id;
          item.CreatedDate = DateTime.UtcNow;
          item.IsDeleted = false;
        }
        apiRespone = await _iEmployeeHR.AddEmployeeLeaveDetails(model);
      }
      return apiRespone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> EditEmployeeAccountSalaryDetail([FromBody] List<EmployeePayrollAccountModel> model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var id = user.Id;
        response = await _iEmployeeHR.EditEmployeeSalaryAccountDetail(model, id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    public async Task<object> GetPrimarySalaryHeads(int EmployeeId)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _iEmployeeHR.GetPrimarySalaryHeads(EmployeeId);
      }
      return response;
    }

    //[HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Trust")]
    //public async Task<object> DisapproveEmployeeApprovedSalary([FromBody]DisapprovePayrollModel model)
    //{
    //  APIResponse response = null;
    //  var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //  if (user != null)
    //  {
    //    response = await _iEmployeeHR.DisapproveEmployeeApprovedSalary(model, user.Id);
    //  }
    //  return response;
    //}

    #region"Data Transfer Api For Attendance"
    [HttpGet]
    public string TransferDataForAttendance()
    {
      return _iEmployeeHR.TransferDataForAttendance();
    }

    #endregion

    #region"Data Insert For Office Hours"
    [HttpGet]
    public string OfficeHours(int iYear)
    {
      return _iEmployeeHR.OfficeHours(iYear);
    }
    #endregion

    #region"Data Transfer Api For Voucher Transaction 2008 data only"
    [HttpGet]
    public async Task<string> TransferDataForVoucherTransaction2008()
    {
      return await _iEmployeeHR.TransferDataForVoucherTransaction2008();
    }

#endregion
  }
}
