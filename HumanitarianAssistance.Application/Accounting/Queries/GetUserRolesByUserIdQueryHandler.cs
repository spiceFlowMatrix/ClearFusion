using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{

    public class GetUserRolesByUserIdQueryHandler : IRequestHandler<GetUserRolesByUserIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetUserRolesByUserIdQueryHandler(
                HumanitarianAssistanceDbContext dbContext,
                UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse> Handle(GetUserRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                List<Roles> currentRoles = new List<Roles>();
                var user = await _userManager.FindByIdAsync(request.UserId);

                var roles = await _roleManager.Roles
                                              .Select(x => new Roles
                                              {
                                                  RoleName = x.Name,
                                                  Id = x.Id
                                              }).ToListAsync();

                foreach (var rol in roles)
                {
                    var isExist = _userManager.IsInRoleAsync(user, rol.RoleName).Result;
                    if (isExist)
                    {
                        currentRoles.Add(rol);
                    }
                }

                response.data.RoleList = currentRoles;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Role List";
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