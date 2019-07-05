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
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.interfaces.AccountingNew;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [Produces("application/json")]
    [Route("api/Account/[Action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private APIResponse response;
        private IPermissions _ipermissions;
        private IUserDetails _iuserDetails;
        private IPermissionsInRoles _ipermissionsInRoles;
        private IVoucherDetail _ivoucherDetail;
        private IVoucherNewService _iVoucherNewService;
        private IExchangeRate _iExchangeRate;
        private IChartOfAccountNewService _iChartOfAccountNewService;
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
                Console.WriteLine(exception.Message);
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

                // model validate
                if (!ModelState.IsValid)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.InvalidUserCredentials;

                    return response;
                }

                var user = await _userManager.FindByNameAsync(model.UserName.Trim());

                if (user != null)
                {
                    List<UserRolePermissionsModel> userRolePermissionsList = new List<UserRolePermissionsModel>();
                    List<RolePermissionModel> RolePermissionModelList = new List<RolePermissionModel>();
                    List<ApproveRejectPermissionModel> ApproveRejectRolePermissionModelList = new List<ApproveRejectPermissionModel>();
                    List<AgreeDisagreePermissionModel> AgreeDisagreeRolePermissionModelList = new List<AgreeDisagreePermissionModel>();
                    List<OrderSchedulePermissionModel> OrderSchedulePermissionModelList = new List<OrderSchedulePermissionModel>();
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    #region "Check wrong credientials"
                    if (!result.Succeeded)
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.InvalidUserCredentials;
                        return response;
                    }
                    #endregion

                    #region "Get CLAIMS & ROLES"
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    var roles = await _userManager.GetRolesAsync(user);

                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
                    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    #endregion

                    #region "Approve / reject permissions"
                    foreach (var role in roles)
                    {
                        UserRolePermissionsModel userRolePermissions = new UserRolePermissionsModel();

                        //userClaims.Add(new Claim("Roles", role)); //imp

                        var roleid = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == role);
                        List<RolePermissions> rolePermissionsList = _uow.GetDbContext().RolePermissions.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
                        List<ApproveRejectPermission> approveRejectRolePermissionsList = _uow.GetDbContext().ApproveRejectPermission.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
                        List<AgreeDisagreePermission> agreeDisagreeRolePermissionsList = _uow.GetDbContext().AgreeDisagreePermission.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
                        List<OrderSchedulePermission> orderScheduleRolePermissionsList = _uow.GetDbContext().OrderSchedulePermission.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
                        if (rolePermissionsList.Any())
                        {
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
                        if (approveRejectRolePermissionsList.Any())
                        {
                            foreach (ApproveRejectPermission rolePermissions in approveRejectRolePermissionsList)
                            {
                                if (ApproveRejectRolePermissionModelList.Any())
                                {
                                    ApproveRejectPermissionModel rolePermissionModel = ApproveRejectRolePermissionModelList.FirstOrDefault(x => x.PageId == rolePermissions.PageId);

                                    if (rolePermissionModel == null)
                                    {
                                        ApproveRejectPermissionModel rolePermission = new ApproveRejectPermissionModel();
                                        rolePermission.Approve = rolePermissions.Approve;
                                        rolePermission.Id = rolePermissions.Id;
                                        rolePermission.PageId = rolePermissions.PageId;
                                        rolePermission.PageId = rolePermissions.PageId;
                                        rolePermission.Reject = rolePermissions.Reject;
                                        rolePermission.RoleId = rolePermissions.RoleId;
                                        ApproveRejectRolePermissionModelList.Add(rolePermission);
                                    }
                                    else
                                    {
                                        if (rolePermissionModel.Approve && !rolePermissionModel.Reject && rolePermissions.Reject)
                                        {
                                            rolePermissionModel.Reject = rolePermissions.Reject;
                                        }
                                        else if (!rolePermissionModel.Approve && !rolePermissionModel.Reject && rolePermissions.Reject)
                                        {
                                            rolePermissionModel.Approve = true;
                                            rolePermissionModel.Reject = true;
                                        }

                                    }
                                }
                                else
                                {
                                    ApproveRejectPermissionModel rolePermissionModel = new ApproveRejectPermissionModel();
                                    rolePermissionModel.Approve = rolePermissions.Approve;
                                    rolePermissionModel.Reject = rolePermissions.Reject;
                                    rolePermissionModel.PageId = rolePermissions.PageId;
                                    rolePermissionModel.Id = rolePermissions.Id;
                                    rolePermissionModel.RoleId = rolePermissions.RoleId;
                                    ApproveRejectRolePermissionModelList.Add(rolePermissionModel);
                                }
                            }
                        }
                        if (agreeDisagreeRolePermissionsList.Any())
                        {
                            foreach (AgreeDisagreePermission rolePermissions in agreeDisagreeRolePermissionsList)
                            {
                                if (AgreeDisagreeRolePermissionModelList.Any())
                                {
                                    AgreeDisagreePermissionModel rolePermissionModel = AgreeDisagreeRolePermissionModelList.FirstOrDefault(x => x.PageId == rolePermissions.PageId);

                                    if (rolePermissionModel == null)
                                    {
                                        AgreeDisagreePermissionModel rolePermission = new AgreeDisagreePermissionModel();
                                        rolePermission.Agree = rolePermissions.Agree;
                                        rolePermission.Id = rolePermissions.Id;
                                        rolePermission.PageId = rolePermissions.PageId;
                                        rolePermission.PageId = rolePermissions.PageId;
                                        rolePermission.Disagree = rolePermissions.Disagree;
                                        rolePermission.RoleId = rolePermissions.RoleId;
                                        AgreeDisagreeRolePermissionModelList.Add(rolePermission);
                                    }
                                    else
                                    {
                                        if (rolePermissionModel.Agree && !rolePermissionModel.Disagree && rolePermissions.Disagree)
                                        {
                                            rolePermissionModel.Disagree = rolePermissions.Disagree;
                                        }
                                        else if (!rolePermissionModel.Agree && !rolePermissionModel.Disagree && rolePermissions.Disagree)
                                        {
                                            rolePermissionModel.Agree = true;
                                            rolePermissionModel.Disagree = true;
                                        }

                                    }
                                }
                                else
                                {
                                    AgreeDisagreePermissionModel rolePermissionModel = new AgreeDisagreePermissionModel();
                                    rolePermissionModel.Agree = rolePermissions.Agree;
                                    rolePermissionModel.Disagree = rolePermissions.Disagree;
                                    rolePermissionModel.PageId = rolePermissions.PageId;
                                    rolePermissionModel.Id = rolePermissions.Id;
                                    rolePermissionModel.RoleId = rolePermissions.RoleId;
                                    AgreeDisagreeRolePermissionModelList.Add(rolePermissionModel);
                                }
                            }
                        }
                        if (orderScheduleRolePermissionsList.Any())
                        {
                            foreach (OrderSchedulePermission rolePermissions in orderScheduleRolePermissionsList)
                            {
                                if (OrderSchedulePermissionModelList.Any())
                                {
                                    OrderSchedulePermissionModel rolePermissionModel = OrderSchedulePermissionModelList.FirstOrDefault(x => x.PageId == rolePermissions.PageId);

                                    if (rolePermissionModel == null)
                                    {
                                        OrderSchedulePermissionModel rolePermission = new OrderSchedulePermissionModel();
                                        rolePermission.OrderSchedule = rolePermissions.OrderSchedule;
                                        rolePermission.Id = rolePermissions.Id;
                                        rolePermission.PageId = rolePermissions.PageId;
                                        rolePermission.PageId = rolePermissions.PageId;
                                        rolePermission.RoleId = rolePermissions.RoleId;
                                        OrderSchedulePermissionModelList.Add(rolePermission);
                                    }
                                    else
                                    {
                                        if (rolePermissionModel.OrderSchedule)
                                        {
                                            rolePermissionModel.OrderSchedule = rolePermissions.OrderSchedule;
                                        }
                                        else if (!rolePermissionModel.OrderSchedule)
                                        {
                                            rolePermissionModel.OrderSchedule = true;
                                        }

                                    }
                                }
                                else
                                {
                                    OrderSchedulePermissionModel rolePermissionModel = new OrderSchedulePermissionModel();
                                    rolePermissionModel.OrderSchedule = rolePermissions.OrderSchedule;
                                    rolePermissionModel.PageId = rolePermissions.PageId;
                                    rolePermissionModel.Id = rolePermissions.Id;
                                    rolePermissionModel.RoleId = rolePermissions.RoleId;
                                    OrderSchedulePermissionModelList.Add(rolePermissionModel);
                                }
                            }
                        }
                        userRolePermissionsList.Add(userRolePermissions);

                    }
                    #endregion

                    #region "Generate token"
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
                    #endregion

                    #region "Set responses"
                    var User = _uow.GetDbContext().UserDetails.AsNoTracking().Where(x => x.IsDeleted == false && x.AspNetUserId == user.Id).Select(x => x.UserID).FirstOrDefault();
                    var Offices = _uow.GetDbContext().UserDetailOffices.Where(x => x.IsDeleted == false && x.UserId == User).Select(x => x.OfficeId).ToList();
                    response.data.AspNetUserId = user.Id;
                    response.StatusCode = 200;
                    response.Message = "Success";
                    response.data.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    response.data.Roles = roles.ToList();
                    response.data.RolePermissionModelList = RolePermissionModelList;
                    response.data.ApproveRejectPermissionsInRole = ApproveRejectRolePermissionModelList;
                    response.data.AgreeDisagreePermissionsInRole = AgreeDisagreeRolePermissionModelList;
                    response.data.OrderSchedulePermissionsInRole = OrderSchedulePermissionModelList;
                    response.data.UserOfficeList = Offices.Count > 0 ? Offices : null;
                    #endregion
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
                response.Message = "There is server error "+ ex.Message;
            }
            return response;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<object> GetAllUserDetails()
        {
            APIResponse response = await _iuserDetails.GetAllUserDetails();
            return response;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<object> GetAllUserList()
        {
            APIResponse response = await _iuserDetails.GetAllUserList();
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

        //[HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<object> GetAllVoucherDetails()
        //{
        //  APIResponse response = await _ivoucherDetail.GetAllVoucherDetails();
        //  return response;
        //}


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
                response = await _iVoucherNewService.AddEmployeePensionPayment(model);
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

                bool result = await UpdateRole(rolesWithPagePermissionsModel.RoleId, rolesWithPagePermissionsModel.RoleName);

                response = await _ipermissionsInRoles.UpdatePermissionsOnSelectedRole(rolesWithPagePermissionsModel);
            }

            return response;

        }

        [HttpPost]
        public async Task<bool> UpdateRole(string RoleId, string RoleName)
        {

            IdentityResult identityResult = null;

            try
            {
                var roleExists = await _roleManager.FindByIdAsync(RoleId);

                if (roleExists != null)
                {
                    //var role = new IdentityRole();
                    roleExists.Name = RoleName;
                    identityResult = await _roleManager.UpdateAsync(roleExists);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return identityResult.Succeeded;
        }


    }
}
