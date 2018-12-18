using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using HumanitarianAssistance.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataAccess.DbEntities;
using Microsoft.AspNetCore.Authorization;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IO;
using DataAccess;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/AccountReports/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class AccountReportsController : Controller
    {
    private readonly SignInManager<AppUser> _signInManager;
    //private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JsonSerializerSettings _serializerSettings;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private APIResponse response;
    private IPermissions _ipermissions;
    private IUserDetails _iuserDetails;
    private IPermissionsInRoles _ipermissionsInRoles;
    private IAccountRecords _iaccountRecords;
    private IExchangeRate _iExchangeRate;
    IUnitOfWork _uow;

    public AccountReportsController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            IPermissions ipermissions,
            IUserDetails iuserDetails,
            IPermissionsInRoles ipermissionsInRoles,
            IAccountRecords iaccountRecords,
            IExchangeRate iExchangeRate,
            IUnitOfWork uow)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _ipermissions = ipermissions;
      _iuserDetails = iuserDetails;
      _ipermissionsInRoles = ipermissionsInRoles;
      _iaccountRecords = iaccountRecords;
      _iExchangeRate = iExchangeRate;
      _uow = uow;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore

      };

    }

    [HttpGet]
    public async Task<object> GetBalanceSheetReport()
    {
      APIResponse response = await _iaccountRecords.GetBalanceSheet();
      return response;
    }


  }
}
