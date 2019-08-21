using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetAllRolesQueryHandler(HumanitarianAssistanceDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var roles = await _roleManager.Roles.Select(x => new Roles
                {
                    RoleName = x.Name,
                    Id = x.Id
                }).ToListAsync();

                response.data.RoleList = roles;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "There is server error " + ex.Message;
            }
            return response;
        }
    }
}