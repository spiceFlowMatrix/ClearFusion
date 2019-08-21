using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllEmployeeTypeQueryHandler : IRequestHandler<GetAllEmployeeTypeQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeTypeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeType> list = await _dbContext.EmployeeType.Where(x => x.IsDeleted == false).ToListAsync();

                var employeetypelist = list.Select(x => new EmployeeTypeModel
                {
                    EmployeeTypeId = x.EmployeeTypeId,
                    EmployeeTypeName = x.EmployeeTypeName
                }).OrderBy(x => x.EmployeeTypeName).ToList();

                response.data.EmployeeTypeList = employeetypelist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

    }
}
