using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Commands.Common;
using HumanitarianAssistance.Application.Accounting.Commands.Create;
using HumanitarianAssistance.Application.Accounting.Commands.Update;
using HumanitarianAssistance.Application.Accounting.Queries;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanitarianAssistance.WebApi.Controllers.Accounting
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/Account/[Action]")]
    [Authorize]
    [ApiExplorerSettings(GroupName = nameof(SwaggerGrouping.Accounting))]
    public class AccountController : BaseController
    {

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse> Login([FromBody]LoginCommand model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AddUsers([FromBody]AddUserCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> EditUser([FromBody]EditUsersCommand model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            model.ModifiedById = userId;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> ChangePassword([FromBody]ChangeUserPasswordCommand model)
        {

            model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> ResetPassword([FromBody]ResetUserPasswordCommand model)
        {

            model.ModifiedById = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.ModifiedDate = DateTime.UtcNow;

            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> AssignRoleToUser([FromBody]AssignRoleToUserCommand model)
        {
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetRoles()
        {
            return await _mediator.Send(new GetAllRolesQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllUserDetails()
        {
            return await _mediator.Send(new GetAllUserDetailsQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllUserList()
        {
            return await _mediator.Send(new GetAllUserListQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetUserDetailsByUserId(string UserId)
        {
            return await _mediator.Send(new GetUserDetailsByUserIdQuery { UserId = UserId });
        }

        [HttpGet]
        public async Task<ApiResponse> GetPermissionByRoleId(string roleid)
        {
            return await _mediator.Send(new GetPermissionByRoleIdQuery { RoleId = roleid });
        }

        [HttpGet]
        public async Task<ApiResponse> GetPermissions()
        {
            return await _mediator.Send(new GetAllPermissionsQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetUserRole(string userid)
        {
            return await _mediator.Send(new GetUserRolesByUserIdQuery { UserId = userid });
        }

        [HttpGet]
        public async Task<ApiResponse> CheckCurrentPassword(string pwd)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (pwd != null)
                {
                    var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    if (await _userManager.CheckPasswordAsync(user, pwd))
                    {
                        response.StatusCode = 200;
                    }
                    else
                    {
                        response.StatusCode = 401;
                    }
                }
                else
                {
                    response.StatusCode = 401;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllVouchersByOfficeId(int officeid)
        {
            return await _mediator.Send(new GetAllVouchersByOfficeIdQuery { OfficeId = officeid });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllVoucherType()
        {
            return await _mediator.Send(new GetAllVoucherTypeQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllAccountCode()
        {
            return await _mediator.Send(new GetAllAccountsQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllUserNotifications(string userid)
        {
            return await _mediator.Send(new GetAllUserNotificationsQuery { UserId = userid });
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllInputLevelAccountCode()
        {
            return await _mediator.Send(new GetAllInputLevelAccountsQuery());
        }

        [HttpPost]
        public async Task<ApiResponse> GenerateSalaryVoucher([FromBody]GenerateSalaryVoucherCommand model)
        {
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetEmployeeSalaryVoucher(int EmployeeId, int Month, int Year)
        {
            return await _mediator.Send(new GetEmployeeSalaryVoucherQuery
            {
                EmployeeId = EmployeeId,
                Month = Month,
                Year = Year
            });
        }

        [HttpGet]
        public async Task<ApiResponse> ReverseEmployeeSalaryVoucher(long VoucherNo)
        {
            return await _mediator.Send(new ReverseEmployeeSalaryVoucherCommand
            {
                VoucherNo = VoucherNo
            });
        }

        [HttpPost]
        public async Task<ApiResponse> DisapproveEmployeeApprovedSalary([FromBody]DisapproveEmployeeApprovedSalaryCommand model)
        {
            return await _mediator.Send(model);
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllAccountFilter()
        {
            return await _mediator.Send(new GetAllAccountFilterTypesQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllApplicationPages()
        {
            return await _mediator.Send(new GetAllApplicationPagesQuery());
        }

        [HttpGet]
        public async Task<ApiResponse> GetPermissionsOnSelectedRole([FromQuery]string RoleId)
        {
            return await _mediator.Send(new GetPermissionsOnSelectedRoleQuery { RoleId = RoleId });
        }

        [HttpPost]
        public async Task<ApiResponse> AddRoleWithPagePermissions([FromBody]AddRoleWithPagePermissionsCommand model)
        {
            return await _mediator.Send(model);
        }

        [HttpPost]
        public async Task<ApiResponse> UpdatePermissionsOnSelectedRole([FromBody]UpdatePermissionsOnSelectedRoleCommand model)
        {
            return await _mediator.Send(model);
        }




    }
}