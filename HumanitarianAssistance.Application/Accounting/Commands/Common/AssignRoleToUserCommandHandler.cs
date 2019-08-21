using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{

    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public AssignRoleToUserCommandHandler(HumanitarianAssistanceDbContext dbContext, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApiResponse> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                var roles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, roles);

                var result = await _userManager.AddToRolesAsync(user, request.Roles);

                await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));

                foreach (var role in request?.Roles)
                {
                    await _userManager.AddClaimAsync(user, new Claim("Roles", role));

                }
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to update Role");
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
