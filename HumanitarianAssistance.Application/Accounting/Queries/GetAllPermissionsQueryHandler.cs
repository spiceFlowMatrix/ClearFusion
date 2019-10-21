using System;
using System.Collections.Generic;
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
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetAllPermissionsQueryHandler(HumanitarianAssistanceDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                IList<PermissionsModel> list = await _dbContext.Permissions
                                .Where(x => x.IsDeleted == false)
                                .Select(x => new PermissionsModel { Name = x.Name, Id = x.Id })
                                .ToListAsync();

                response.data.PermissionsList = list;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}