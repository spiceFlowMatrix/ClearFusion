using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Accounting
{
  [Produces("application/json")]
  [Route("api/FinancialReport/[Action]/")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class FinancialReportController : Controller
  {

    private readonly SignInManager<AppUser> _signInManager;
    //private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private APIResponse response;
    private IPermissions _ipermissions;
    private IUserDetails _iuserDetails;
    private IPermissionsInRoles _ipermissionsInRoles;
    private IPermissionsInRoles _iPermissionsInRolesService;
    IUnitOfWork _uow;
    private IAccountBalance _financialReportService;

    public FinancialReportController(
      SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,
      UserManager<AppUser> userManager,
      IConfiguration configuration, IPermissions ipermissions,
      IUserDetails iuserDetails, IPermissionsInRoles ipermissionsInRoles,
      IPermissionsInRoles iPermissionsInRolesService, IUnitOfWork uow,
      IAccountBalance financialReportService)
    {
      _signInManager = signInManager;
      _roleManager = roleManager;
      _userManager = userManager;
      _configuration = configuration;
      _ipermissions = ipermissions;
      _iuserDetails = iuserDetails;
      _ipermissionsInRoles = ipermissionsInRoles;
      _iPermissionsInRolesService = iPermissionsInRolesService;
      _uow = uow;
      _financialReportService = financialReportService;
    }

    [HttpGet]
    public async Task<object> Get()
    {
      var reportResult = await _financialReportService.GetNoteBalancesByHeadType(1, 3, DateTime.Now);
      return reportResult;
    }

    [HttpPost]
    public async Task<APIResponse> GetExchangeGainLossReport([FromBody]ExchangeGainLossFilterModel exchangeGainLossReport)
    {
      APIResponse response = new APIResponse();
      response = await _financialReportService.GetExchangeGainLossReport(exchangeGainLossReport);
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> SaveGainLossAccountList([FromBody] List<long> accountIds)
    {
      APIResponse response = new APIResponse();
      response = await _financialReportService.SaveGainLossAccountList(accountIds);
      return response;
    }

    [HttpGet]
    public async Task<APIResponse> GetExchangeGainLossFilterAccountList()
    {
      APIResponse response = new APIResponse();
      response = await _financialReportService.GetExchangeGainLossFilterAccountList();
      return response;
    }

    [HttpPost]
    public async Task<APIResponse> GetDetailOfNotes([FromBody] DetailOfNotesFilterModel model)
    {
      APIResponse response = new APIResponse();
      response = await _financialReportService.GetDetailOfNotes(model);
      return response;
    }
  }
}
