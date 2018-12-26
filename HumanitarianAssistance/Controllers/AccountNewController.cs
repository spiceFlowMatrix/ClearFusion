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
using HumanitarianAssistance.Service.interfaces.AccountingNew;
using HumanitarianAssistance.ViewModels.Models.AccountingNew;

namespace HumanitarianAssistance.WebAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/AccountNew/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class AccountNewController : Controller
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
    private IVoucherDetail _ivoucherDetail;
    private IExchangeRate _iExchangeRate;
    private IChartOfAccountNewService _iChartOfAccountNewService;
    private IPermissionsInRoles _iPermissionsInRolesService;
    private IVoucherNewService _iVoucherNewService;
    IUnitOfWork _uow;

    public AccountNewController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            IPermissions ipermissions,
            IUserDetails iuserDetails,
            IPermissionsInRoles ipermissionsInRoles,
            IVoucherDetail ivoucherDetail,
            IExchangeRate iExchangeRate,
            IChartOfAccountNewService iChartOfAccountNew,
            IUnitOfWork uow,
            IVoucherNewService iVoucherNewService
            )
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _ipermissions = ipermissions;
      _iuserDetails = iuserDetails;
      _ipermissionsInRoles = ipermissionsInRoles;
      _ivoucherDetail = ivoucherDetail;
      _iExchangeRate = iExchangeRate;
      _iChartOfAccountNewService = iChartOfAccountNew;
      _uow = uow;
      _iVoucherNewService = iVoucherNewService;
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore

      };
    }

    /// <summary>
    /// Get All Voucher List
    /// </summary>
    /// <param name="voucherNewFilterModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<object> GetAllVoucherList([FromBody]VoucherNewFilterModel voucherNewFilterModel)
    {
      APIResponse response = await _iVoucherNewService.GetAllNewVoucherList(voucherNewFilterModel);
      return response;
    }

    /// <summary>
    /// Get Voucher Detail By VoucherNo
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<object> GetVoucherDetailByVoucherNo([FromBody]long id)
    {
      APIResponse response = await _iVoucherNewService.GetVoucherDetailByVoucherNo(id);
      return response;
    }

    /// <summary>
    /// Add Voucher Detail
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddVoucherDetail([FromBody] VoucherDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iVoucherNewService.AddVoucherNewDetail(model);
      return response;
    }

    /// <summary>
    /// Edit Voucher Detail
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> EditVoucherNewDetail([FromBody] VoucherDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iVoucherNewService.EditVoucherNewDetail(model);
      return response;
    }

    /// <summary>
    /// Get All Voucher Transactions List
    /// </summary>
    /// <param name="voucherNewFilterModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<object> GetAllTransactionsByVoucherId([FromBody]long id)
    {
      APIResponse response = await _iVoucherNewService.GetAllTransactionsByVoucherId(id);
      return response;
    }

    /// <summary>
    /// Update the voucher transaction
    /// </summary>
    /// <param name="voucherTransactions"></param>
    /// <returns>Success/Failure</returns>
    [HttpPost]
    public async Task<object> EditVoucherTransaction([FromBody]VoucherTransactionsModel voucherTransactions)
    {
      APIResponse response = await _iVoucherNewService.EditTransactionDetail(voucherTransactions);
      return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<object> DeleteTransactionById([FromBody]long id)
    {
      APIResponse response = await _iVoucherNewService.DeleteTransactionById(id);
      return response;
    }

  }
}
