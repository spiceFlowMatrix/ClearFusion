using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HumanitarianAssistance.WebAPI.Controllers.Accounting
{
  [Produces("application/json")]
  [Route("api/FinancialReport/[Action]")]
  public class FinancialReportController
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
    private IPermissionsInRoles _iPermissionsInRolesService;
    IUnitOfWork _uow;

    public FinancialReportController(
      UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
      SignInManager<AppUser> signInManager

    )
    {

    }
  }
}
