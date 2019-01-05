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

namespace HumanitarianAssistance.Controllers
{
  [Produces("application/json")]
  [Route("api/Account/[Action]")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class AccountController : Controller
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
    IUnitOfWork _uow;



    public AccountController(
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
            IUnitOfWork uow
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
      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore

      };
    }


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddUserInRole([FromBody] LoginViewModel model)
    {
      var user = await _userManager.FindByEmailAsync(model.UserName);
      if (user != null)
      {

        await _userManager.AddToRoleAsync(user, "SuperAdmin");
      }
      return Ok("cxvxc");
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public string EmployeeList()
    {
      return "Product List ";
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddPermissions([FromBody] PermissionsModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }

      APIResponse response = await _ipermissions.AddPermission(model);

      return response;

    }
    [HttpPost]
    //    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme,Policy = "Trust")]
    public async Task<object> AddUserClaim([FromBody]LoginViewModel model)
    {
      //var user=await _userManager.GetUserAsync(User);
      var user = await _userManager.FindByEmailAsync(model.UserName);

      if (user == null) return BadRequest("could not apply claim: invalid user name");
      var result = await _userManager.CheckPasswordAsync(user, model.Password);
      if (!result) return BadRequest("could not apply claim : invalid password");
      var claimresult = await _userManager.AddClaimAsync(user, new Claim("OfficeCode", ""));
      return Ok("Claim Created");
      //AppUser user = new AppUser
      //{

      //  UserName=model.UserName,
      //  FirstName = "naval",
      //  Email=model.UserName,
      //  LastName="bhatt"

      //};
      //if (ModelState.IsValid)
      //{
      //  var result = await _signInManager.PasswordSignInAsync(model.UserName,
      //     model.Password, model.IsRememberMe,false);

      //  if (result.Succeeded)
      //  {
      //    //if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
      //    //{
      //    //  return Redirect(model.ReturnUrl);
      //    //}
      //    //else
      //    //{
      //    //  return RedirectToAction("Index", "Home");
      //    //}

      //    //await _signInManager.SignInAsync(user, false);
      //    //return await GenerateJwtToken(model.UserName, user);
      //  }

      //}

    }

    [HttpPost]
    public async Task<bool> AddRole(string RoleName)
    {

      IdentityResult identityResult = null;

      try
      {
        var roleExists = await _roleManager.FindByNameAsync(RoleName);

        if (roleExists == null)
        {
          var role = new IdentityRole();
          role.Name = RoleName;
          identityResult = await _roleManager.CreateAsync(role);
        }
      }
      catch (Exception exception)
      {

      }
      return identityResult.Succeeded;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<object> Login([FromBody]LoginUserModel model)
    {
      try
      {
        response = new APIResponse();

        if (!ModelState.IsValid) return BadRequest("Could not create token");
        var user = await _userManager.FindByNameAsync(model.UserName.Trim());
        //if (user == null) return BadRequest("Could not create token");
        if (user != null)
        {


          var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
          //Comment
          if (!result.Succeeded) return BadRequest("Could not create token");

          var userClaims = await _userManager.GetClaimsAsync(user);



          //var officedetais = await _uow.UserDetailsRepository.FindAsync(x => x.IsDeleted == false && x.AspNetUserId == user.Id);

          var roles = await _userManager.GetRolesAsync(user);

          userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
          userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
          //Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

          List<UserRolePermissionsModel> userRolePermissionsList = new List<UserRolePermissionsModel>();

          List<RolePermissionModel> RolePermissionModelList = new List<RolePermissionModel>();

          foreach (var role in roles)
          {
            UserRolePermissionsModel userRolePermissions = new UserRolePermissionsModel();

            //userClaims.Add(new Claim("Roles", role)); //imp

            var roleid = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == role);

            //userRolePermissions.RoleName = role;
            //userRolePermissions.RoleId = roleid.Id;

            List<RolePermissions> rolePermissionsList = _uow.GetDbContext().RolePermissions.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();

            if (rolePermissionsList.Any())
            {

              //userRolePermissions.RolePagePermission = new List<RolePermissionModel>();

              //foreach (RolePermissions rolePermissions in rolePermissionsList)
              //{
              //  RolePermissionModel rolePermissionModel = new RolePermissionModel();
              //  rolePermissionModel.CanEdit = rolePermissions.CanEdit;
              //  rolePermissionModel.CanView = rolePermissions.CanView;
              //  rolePermissionModel.ModuleId = rolePermissions.ModuleId;
              //  rolePermissionModel.PageId = rolePermissions.PageId;
              //  rolePermissionModel.RolesPermissionId = rolePermissions.RolesPermissionId;
              //  userRolePermissions.RolePagePermission.Add(rolePermissionModel);
              //}

              foreach (RolePermissions rolePermissions in rolePermissionsList)
              {
                if (RolePermissionModelList.Any())
                {
                  RolePermissionModel rolePermissionModel = RolePermissionModelList.FirstOrDefault(x => x.PageId == rolePermissions.PageId);

                  if (rolePermissionModel == null)
                  {
                    RolePermissionModel rolePermission = new RolePermissionModel();
                    rolePermission.CanEdit = rolePermissions.CanEdit;
                    rolePermission.CanView = rolePermissions.CanView;
                    rolePermission.ModuleId = rolePermissions.ModuleId;
                    rolePermission.PageId = rolePermissions.PageId;
                    rolePermission.RolesPermissionId = rolePermissions.RolesPermissionId;
                    RolePermissionModelList.Add(rolePermission);
                  }
                  else
                  {
                    if (rolePermissionModel.CanView && !rolePermissionModel.CanEdit && rolePermissions.CanEdit)
                    {
                      rolePermissionModel.CanEdit = rolePermissions.CanEdit;
                    }
                    else if (!rolePermissionModel.CanView && !rolePermissionModel.CanEdit && rolePermissions.CanEdit)
                    {
                      rolePermissionModel.CanView = true;
                      rolePermissionModel.CanEdit = true; 
                    }

                  }
                }
                else
                {
                  RolePermissionModel rolePermissionModel = new RolePermissionModel();
                  rolePermissionModel.CanEdit = rolePermissions.CanEdit;
                  rolePermissionModel.CanView = rolePermissions.CanView;
                  rolePermissionModel.ModuleId = rolePermissions.ModuleId;
                  rolePermissionModel.PageId = rolePermissions.PageId;
                  rolePermissionModel.RolesPermissionId = rolePermissions.RolesPermissionId;
                  RolePermissionModelList.Add(rolePermissionModel);
                }
              }
            }

            userRolePermissionsList.Add(userRolePermissions);

          }

          //_ipermissions.GetPermissionsByRoleId()

          string k = _configuration["JwtKey"];
          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(k));
          var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
          var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

          var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("JwtIssuerOptions:Issuer").Value,
            audience: _configuration.GetSection("JwtIssuerOptions:Audience").Value,
            claims: userClaims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: creds
          );
          //response = new APIResponse();
          var User = _uow.GetDbContext().UserDetails.AsNoTracking().Where(x => x.IsDeleted == false && x.AspNetUserId == user.Id).Select(x => x.UserID).FirstOrDefault();
          var Offices = _uow.GetDbContext().UserDetailOffices.Where(x => x.IsDeleted == false && x.UserId == User).Select(x => x.OfficeId).ToList();
          response.data.AspNetUserId = user.Id;
          response.StatusCode = 200;
          response.Message = "Success";
          response.data.Token = new JwtSecurityTokenHandler().WriteToken(token);
          response.data.Roles = roles.ToList();
          //response.data.UserRolePermissions = userRolePermissionsList;
          response.data.RolePermissionModelList = RolePermissionModelList;


          response.data.UserOfficeList = Offices.Count > 0 ? Offices : null;
        }
        else
        {
          response.StatusCode = StaticResource.failStatusCode;
          response.Message = StaticResource.InvalidUserCredentials;
        }
      }
      catch (Exception ex)
      {
        response.StatusCode = StaticResource.failStatusCode;
        response.Message = StaticResource.SomethingWrong + ex.Message;
      }
      return new OkObjectResult(response);
    }

    [HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
    public async Task<object> AddUsers([FromBody]UserDetailsModel users)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        users.CreatedById = id;
        users.IsDeleted = false;
        users.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iuserDetails.AddUser(users);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> EditUser([FromBody]UserDetailsModel users)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        users.ModifiedById = id;
        users.IsDeleted = false;
        users.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iuserDetails.EditUser(users);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> ChangePassword([FromBody]ChangePasswordModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
        model.Username = user.UserName;
        response = await _iuserDetails.ChangePassword(model);
      }
      else
      {
        response = new APIResponse();
        response.StatusCode = 401;
        response.Message = "There is some error";
      }

      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> ResetPassword([FromBody]ResetPassword model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);


      if (user != null)
      {
        var id = user.Id;
        model.ModifiedById = id;
        model.IsDeleted = false;
        model.ModifiedDate = DateTime.UtcNow;
      }
      APIResponse response = await _iuserDetails.ResetPassword(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetDepartment(string officeCode)
    {
      APIResponse response = await _iuserDetails.GetDepartmentsByOfficeCodeAsyn(officeCode);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AssignRoleToUser([FromBody]RolesAndUserModel model)
    {
      var user = await _userManager.FindByIdAsync(model.UserId);
      var roles = await _userManager.GetRolesAsync(user);
      await _userManager.RemoveFromRolesAsync(user, roles);
      var result = await _userManager.AddToRolesAsync(user, model.Roles);

      await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));

      foreach (var role in model?.Roles)
      {

        await _userManager.AddClaimAsync(user, new Claim("Roles", role));

        if (result.Succeeded)
        {

        }


      }
      if (result.Succeeded)
      {
        return new APIResponse { StatusCode = 200, Message = "Success" };
      }
      return new APIResponse { StatusCode = 402, Message = "Error" };
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetRoles()
    {

      APIResponse response = new APIResponse();
      try
      {

        await Task.Run(() =>
        {
          var roles = _roleManager.Roles.Select(x => new Roles
          {
            RoleName = x.Name,
            Id = x.Id
          }).ToList();


          response.data.RoleList = roles;
          response.StatusCode = 200;
          response.Message = "Success";

        });
      }
      catch (Exception ex)
      {
        response.StatusCode = 500;
        response.Message = "There is server error";
      }
      return response;
    }

    //[HttpGet]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
    //public async Task<object> GetRolesByUserId()
    //{
    //  APIResponse response = new APIResponse();
    //  await Task.Run(() =>
    //  {
    //    var roles = _roleManager.Roles.

    //  });
    //  return response;
    //}

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllUserDetails()
    {
      APIResponse response = await _iuserDetails.GetAllUserDetails();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetUserDetailsByUserId(string UserId)
    {
      APIResponse response = await _iuserDetails.GetUserDetailsByUserId(UserId);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> PermissionsInRoles([FromBody] List<PermissionsInRolesModel> model, string RoleId)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        for (int i = 0; i < model.Count; i++)
        {
          var id = user.Id;
          model[i].CreatedById = id;
          model[i].IsDeleted = false;
          model[i].CreatedDate = DateTime.UtcNow;
        }
      }
      APIResponse response = await _ipermissionsInRoles.AddPermissionsInRoles(model, RoleId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetPermissionByRoleId(string roleid)
    {
      APIResponse response = await _ipermissionsInRoles.GetPermissionByRoleId(roleid);
      return response;

    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetPermissions()
    {
      APIResponse response = await _ipermissionsInRoles.GetPermissionsAsync();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetUserRole(string userid)
    {
      APIResponse response = await _iuserDetails.GetUserRolesByUserId(userid);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> CheckCurrentPassword(string pwd)
    {
      APIResponse api = new APIResponse();
      try
      {
        if (pwd != null)
        {
          var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
          if (await _userManager.CheckPasswordAsync(user, pwd))
          {
            api.StatusCode = 200;
          }
          else
          {
            api.StatusCode = 401;
          }
        }
        else
        {
          api.StatusCode = 401;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return api;

    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllVoucherDetails()
    {
      APIResponse response = await _ivoucherDetail.GetAllVoucherDetails();
      return response;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllVouchersByOfficeId(int officeid)
    {
      APIResponse response = await _ivoucherDetail.GetAllVouchersByOfficeId(officeid);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllVoucherDetailsByFilter([FromBody] VoucherFilterModel filterModel)
    {
      APIResponse response = await _ivoucherDetail.GetAllVoucherDetailsByFilter(filterModel);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllVoucherType()
    {
      APIResponse response = await _ivoucherDetail.GetAllVoucherType();
      return response;
    }

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
      APIResponse response = await _ivoucherDetail.AddVoucherDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> EditVoucherDetail([FromBody] VoucherDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ivoucherDetail.EditVoucherDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> DeleteVoucherDetail(int VoucherNo)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      string ModifiedById = "";
      if (user != null)
      {
        ModifiedById = user.Id;
      }
      APIResponse response = await _ivoucherDetail.DeleteVoucherDetail(VoucherNo, ModifiedById);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetJouranlVoucherDetailsByCondition([FromBody] JournalViewModel model)
    {
      //APIResponse response = await _ivoucherDetail.GetJouranlVoucherDetails();
      APIResponse response = await _ivoucherDetail.GetJouranlVoucherDetailsByCondition(model);
      return response;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllAccountCode()
    {
      APIResponse response = await _ivoucherDetail.GetAllAccountCode();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllControlLevelAccountCode()
    {
      APIResponse response = await _ivoucherDetail.GetAllControlLevelAccountCode();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllVoucherTransactionDetailByVoucherNo(int VoucherNo)
    {
      APIResponse response = await _ivoucherDetail.GetAllVoucherTransactionDetailByVoucherNo(VoucherNo);
      return response;
    }

    //[HttpPost]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
    //public async Task<object> AddVoucherTransactionDetail([FromBody] List<VoucherTransactionModel> model)
    //{
    //  APIResponse apiRespone = null;
    //  var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //  if (user != null)
    //  {
    //    var id = user.Id;
    //    APIResponse response = await _ivoucherDetail.AddVoucherTransactionDetail(model, id);
    //  }
    //  return apiRespone;
    //}


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddVoucherTransactionDetail([FromBody] List<VoucherTransactionModel> model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      string id = string.Empty;

      if (user != null)
      {
        id = user.Id;
      }
      APIResponse response = await _ivoucherDetail.AddVoucherTransactionDetail(model, id);
      return response;
    }


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> EditVoucherTransactionDetail([FromBody] VoucherTransactionModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ivoucherDetail.EditVoucherTransactionDetail(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> DeleteVoucherTransactionDetail(int Id)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var userid = user.Id;
        response = await _ivoucherDetail.DeleteVoucherTransactionDetail(Id, userid);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> DeleteVoucherTransactions(int Id)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        var userid = user.Id;
        response = await _ivoucherDetail.DeleteVoucherTransactions(Id, userid);
      }
      return response;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllVoucherDocumentDetailByVoucherNo(int VoucherNo)
    {
      APIResponse response = await _ivoucherDetail.GetAllVoucherDocumentDetailByVoucherNo(VoucherNo);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddVoucherDocumentDetail([FromBody] VoucherDocumentDetailModel model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      var s = model.FilePath.Split(",");

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ivoucherDetail.AddVoucherDocumentDetail(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> DeleteVoucherDocumentDetail(int DocumentId)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      string ModifiedById = "";
      if (user != null)
      {
        ModifiedById = user.Id;
      }
      APIResponse response = await _ivoucherDetail.DeleteVoucherDocumentDetail(DocumentId, ModifiedById);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllLedgerDetails([FromBody] LedgerModels model)
    {
      APIResponse response = null;

      if (model.OfficeIdList.Any())
      {
        response = await _ivoucherDetail.GetAllLedgerDetailsByCondition(model);
      }

      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetTrailBlanceDetailsByCondition([FromBody] LedgerModels model)
    {
      APIResponse response = await _ivoucherDetail.GetTrailBlanceDetailsByCondition(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllVoucherTransactionDetailByBudgetLine(long projectId, long budgetLineId)
    {
      APIResponse response = await _ivoucherDetail.GetAllVoucherTransactionDetailByBudgetLine(projectId, budgetLineId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetProjectAndBudgetLine()
    {
      APIResponse repsonse = await _ivoucherDetail.GetProjectAndBudgetLine();
      return repsonse;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> AddNotesDetails([FromBody] NotesMasterModel model)
    {
      APIResponse respone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        respone = await _ivoucherDetail.AddNotesDetails(model);
      }
      return respone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> EditNotesDetails([FromBody] NotesMasterModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.UtcNow;
        model.IsDeleted = false;
        response = await _ivoucherDetail.EditNotesDetails(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllNotesDetails()
    {
      APIResponse response = await _ivoucherDetail.GetAllNotesDetails();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetBlanceSheetDetails([FromBody]FinancialReportModel model)
    {
      APIResponse response = await _ivoucherDetail.GetBlanceSheetDetails(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetDetailsOfNotesReportData(int? financialyearid, int? currencyid)
    {
      APIResponse response = await _ivoucherDetail.GetDetailsOfNotesReportData(financialyearid, currencyid);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> AddCategoryPopulator([FromBody] CategoryPopulatorModel model)
    {
      APIResponse respone = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        respone = await _ivoucherDetail.AddCategoryPopulator(model, user.Id);
      }
      return respone;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> EditCategoryPopulator([FromBody] CategoryPopulatorModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _ivoucherDetail.EditCategoryPopulator(model, user.Id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> DeleteCategoryPopulator(int categoryPopulatorId)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      string ModifiedById = "";
      if (user != null)
      {
        ModifiedById = user.Id;
      }
      APIResponse response = await _ivoucherDetail.DeleteCategoryPopulator(categoryPopulatorId, ModifiedById);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllCategoryPopulator()
    {
      APIResponse response = await _ivoucherDetail.GetAllCategoryPopulator();
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllUserNotifications(string userid)
    {
      APIResponse response = await _ivoucherDetail.GetAllUserNotifications(userid);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetExchangeGainOrLossAmount([FromBody]ExchangeGainOrLossFilterModel model)
    {
      APIResponse response = await _iExchangeRate.GetExchangeGainOrLossAmount(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllVoucherByJouranlId([FromBody]JournalVoucherFilterModel JournalVoucherFilter)
    {
      APIResponse response = await _ivoucherDetail.GetAllVoucherByJouranlId(JournalVoucherFilter);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetLevelFourAccountCode()
    {
      APIResponse response = await _ivoucherDetail.GetLevelFourAccountCode();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetExchangeGainOrLossTransactionAmount([FromBody]ExchangeGainOrLossTransactionFilterModel model)
    {
      APIResponse response = await _iExchangeRate.GetExchangeGainOrLossTransactionAmount(model);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddExchangeGainLossVoucher([FromBody] ExchangeGainLossVoucher model)
    {
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

      if (user != null)
      {
        var id = user.Id;
        model.CreatedById = id;
        model.IsDeleted = false;
        model.CreatedDate = DateTime.UtcNow;
      }
      APIResponse response = await _ivoucherDetail.AddExchangeGainLossVoucher(model);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetExchangeGainLossVoucherList(int OfficeId)
    {
      APIResponse response = await _ivoucherDetail.GetExchangeGainLossVoucherList(OfficeId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> DeleteExchangeGainLossVoucher(long VoucherNo, string UserId)
    {
      APIResponse response = await _ivoucherDetail.DeleteExchangeGainLossVoucher(VoucherNo, UserId);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetAllInputLevelAccountCode()
    {
      APIResponse response = await _ivoucherDetail.GetAllInputLevelAccountCode();
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddEmployeePensionPayment([FromBody]EmployeePensionPaymentModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        response = await _ivoucherDetail.AddEmployeePensionPayment(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GenerateSalaryVoucher([FromBody]EmployeeSalaryVoucherModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        response = await _ivoucherDetail.GenerateSalaryVoucher(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetEmployeeSalaryVoucher(int EmployeeId, int Month, int Year)
    {
      APIResponse response = await _ivoucherDetail.GetEmployeeSalaryVoucher(EmployeeId, Month, Year);
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> ReverseEmployeeSalaryVoucher(long VoucherNo)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _ivoucherDetail.ReverseEmployeeSalaryVoucher(VoucherNo, user.Id);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> DisapproveEmployeeApprovedSalary([FromBody]DisapprovePayrollModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        response = await _ivoucherDetail.DisapproveEmployeeApprovedSalary(model, user.Id);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> GetVoucherDetailByVoucherNo(int VoucherNo)
    {
      APIResponse response = await _ivoucherDetail.GetVoucherDetailByVoucherNo(VoucherNo);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetMainLevelAccount([FromBody]long id)
    {
      APIResponse response = await _iChartOfAccountNewService.GetMainLevelAccount(id);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllAccountsByParentId([FromBody]int id)
    {
      APIResponse response = await _iChartOfAccountNewService.GetAllAccountsByParentId(id);
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<object> AddChartOfAccount([FromBody]ChartOfAccountNewModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.CreatedById = user.Id;
        model.CreatedDate = DateTime.Now;

        response = await _iChartOfAccountNewService.AddChartOfAccount(model);
      }
      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllAccountsByAccountHeadTypeId([FromBody]int id)
    {
      APIResponse response = await _iChartOfAccountNewService.GetAllAccountsByAccountHeadTypeId(id);
      return response;
    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllAccountFilter()
    {
      APIResponse response = await _iChartOfAccountNewService.GetAllAccountFilter();
      return response;
    }


    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> EditChartOfAccount([FromBody]ChartOfAccountNewModel model)
    {
      APIResponse response = null;
      var user = await _userManager.FindByNameAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
      if (user != null)
      {
        model.ModifiedById = user.Id;
        model.ModifiedDate = DateTime.Now;

        response = await _iChartOfAccountNewService.EditChartOfAccount(model);
      }
      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetAllApplicationPages()
    {
      APIResponse response = null;

      response = await _ipermissionsInRoles.GetAllApplicationPages();

      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> AddRoleWithPagePermissions([FromBody]RolesWithPagePermissionsModel rolesWithPagePermissionsModel)
    {
      APIResponse response = null;

      if (rolesWithPagePermissionsModel != null)
      {
        bool result = await AddRole(rolesWithPagePermissionsModel.RoleName);

        if (result)
        {

          var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == rolesWithPagePermissionsModel.RoleName);

          response = await _ipermissionsInRoles.AddRoleWithPagePermissions(rolesWithPagePermissionsModel, role.Id);
        }
      }

      return response;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> GetPermissionsOnSelectedRole([FromQuery]string RoleId)
    {
      APIResponse response = null;

      response = await _ipermissionsInRoles.GetPermissionsOnSelectedRole(RoleId);

      return response;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<APIResponse> UpdatePermissionsOnSelectedRole([FromBody]RolesWithPagePermissionsModel rolesWithPagePermissionsModel)
    {

      APIResponse response = null;

      if (rolesWithPagePermissionsModel != null)
      {
          response = await _ipermissionsInRoles.UpdatePermissionsOnSelectedRole(rolesWithPagePermissionsModel);
      }

      return response;

    }

  }
}
