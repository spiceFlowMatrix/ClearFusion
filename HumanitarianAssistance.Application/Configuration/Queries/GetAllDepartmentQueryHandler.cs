using AutoMapper;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllDepartmentQueryHandler : IRequestHandler<GetAllDepartmentQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public GetAllDepartmentQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<Department> list = await _dbContext.Department.Include(o => o.OfficeDetails).Where(x => x.IsDeleted == false).ToListAsync();

               List<DepartmentModel> departmentlist = list.Select(x => new DepartmentModel
                {
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.DepartmentName,
                    OfficeId = x.OfficeId,
                    OfficeName = x.OfficeDetails?.OfficeName ?? null
                }).OrderBy(x => x.DepartmentId).ToList();

                response.data.Departments = departmentlist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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