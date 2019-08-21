using MediatR;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using System.Threading.Tasks;
using System.Threading;
using HumanitarianAssistance.Persistence;
using Microsoft.AspNetCore.Identity;
using HumanitarianAssistance.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Accounting.Models;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse>
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public LoginCommandHandler(
                                    HumanitarianAssistanceDbContext dbContext,
                                    SignInManager<AppUser> signInManager,
                                    UserManager<AppUser> userManager,
                                    RoleManager<IdentityRole> roleManager,
                                    IConfiguration configuration
                                )
        {
            _signInManager = signInManager;
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;

        }

        public async Task<ApiResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                // model validate
                if (request.UserName == null || request.Password == null)
                {
                    throw new Exception(StaticResource.InvalidUserCredentials);
                }

                // validate user
                SignInResult result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    throw new Exception(StaticResource.InvalidUserCredentials);
                }

                var user = await _userManager.FindByNameAsync(request.UserName.Trim());
                //var result1 = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (user == null)
                {
                    throw new Exception(StaticResource.InvalidUserCredentials);
                }

                List<UserRolePermissionsModel> userRolePermissionsList = new List<UserRolePermissionsModel>();
                List<RolePermissionModel> RolePermissionModelList = new List<RolePermissionModel>();
                List<ApproveRejectPermissionModel> ApproveRejectRolePermissionModelList = new List<ApproveRejectPermissionModel>();
                List<AgreeDisagreePermissionModel> AgreeDisagreeRolePermissionModelList = new List<AgreeDisagreePermissionModel>();
                List<OrderSchedulePermissionModel> OrderSchedulePermissionModelList = new List<OrderSchedulePermissionModel>();

                #region "Get CLAIMS & ROLES"
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())); // subject used for ClaimTypes.NameIdentifier
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
                userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                #endregion

                #region "Generate token"
                string k = _configuration["JwtKey"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(k));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

                var token = new JwtSecurityTokenHandler()
                                .WriteToken(new JwtSecurityToken(
                                issuer: _configuration.GetSection("JwtIssuerOptions:Issuer").Value,
                                audience: _configuration.GetSection("JwtIssuerOptions:Audience").Value,
                                claims: userClaims,
                                expires: DateTime.Now.AddYears(1),
                                signingCredentials: creds
                            ));

                #endregion

                #region "Approve / reject permissions"
                foreach (var role in roles)
                {
                    UserRolePermissionsModel userRolePermissions = new UserRolePermissionsModel();

                    //userClaims.Add(new Claim("Roles", role)); //imp

                    var roleid = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == role);
                    List<RolePermissions> rolePermissionsList = _dbContext.RolePermissions.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
                    List<ApproveRejectPermission> approveRejectRolePermissionsList = _dbContext.ApproveRejectPermission.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
                    List<AgreeDisagreePermission> agreeDisagreeRolePermissionsList = _dbContext.AgreeDisagreePermission.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
                    List<OrderSchedulePermission> orderScheduleRolePermissionsList = _dbContext.OrderSchedulePermission.Where(x => x.IsDeleted == false && x.RoleId == roleid.Id).ToList();
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

                #region "Set responses"
                var User = _dbContext.UserDetails.AsNoTracking().FirstOrDefault(x => x.IsDeleted == false && x.AspNetUserId == user.Id);
                var Offices = _dbContext.UserDetailOffices.Where(x => x.IsDeleted == false && x.UserId == User.UserID).Select(x => x.OfficeId).ToList();

                response.data.AspNetUserId = user.Id;
                response.data.Token = token;
                response.data.Roles = roles.ToList();
                response.data.RolePermissionModelList = RolePermissionModelList;
                response.data.ApproveRejectPermissionsInRole = ApproveRejectRolePermissionModelList;
                response.data.AgreeDisagreePermissionsInRole = AgreeDisagreeRolePermissionModelList;
                response.data.OrderSchedulePermissionsInRole = OrderSchedulePermissionModelList;
                response.data.UserOfficeList = Offices.Count > 0 ? Offices : null;

                response.StatusCode = 200;
                response.Message = StaticResource.SuccessText;
                #endregion

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}