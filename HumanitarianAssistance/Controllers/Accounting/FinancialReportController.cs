using System.Threading.Tasks;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Accounting
{
  [Produces("application/json")]
  [Route("api/[controller]/")]
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
      var reportResult = await _financialReportService.GetNoteBalancesByHeadType(1, 2);
      return reportResult;
    }
  }
}
