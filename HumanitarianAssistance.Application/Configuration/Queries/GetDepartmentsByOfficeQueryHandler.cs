using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetDepartmentsByOfficeQueryHandler : IRequestHandler<GetDepartmentsByOfficeQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetDepartmentsByOfficeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetDepartmentsByOfficeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<Department> list = await _dbContext.Department.Where(x => x.OfficeId == request.OfficeId && x.IsDeleted == false).ToListAsync();
                var departmentlist = list.
                    Select(x => new DepartmentModel
                    {
                        OfficeCode = x.OfficeCode,
                        DepartmentId = x.DepartmentId,
                        DepartmentName = x.DepartmentName,
                        OfficeId = x.OfficeId
                    }).OrderBy(x => x.DepartmentName).ToList();
                response.data.Departments = departmentlist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong;
            }
            return response;
        }
    }
}
