using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeHistoryOutsideOrganizationQueryHandler : IRequestHandler<GetEmployeeHistoryOutsideOrganizationQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeHistoryOutsideOrganizationQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeHistoryOutsideOrganizationQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                List<EmployeeHistoryOutsideOrganization> employeeHistoryRecord = await _dbContext.EmployeeHistoryOutsideOrganization.Where(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Any())
                {
                    response.data.EmployeeHistoryOutsideOrganizationList = employeeHistoryRecord.Select(x => new EmployeeHistoryOutsideOrganizationModel
                    {
                        EmployeeHistoryOutsideOrganizationId = x.EmployeeHistoryOutsideOrganizationId,
                        EmployeeID = Convert.ToInt32(x.EmployeeID),
                        EmploymentFrom = x.EmploymentFrom == null ? null : (DateTime?)Convert.ToDateTime(x.EmploymentFrom),
                        EmploymentTo = x.EmploymentTo == null ? null : (DateTime?)Convert.ToDateTime(x.EmploymentTo),
                        MonthlySalary = x.MonthlySalary,
                        Organization = x.Organization,
                        ReasonForLeaving = x.ReasonForLeaving,
                        Position = x.Position
                    }).ToList();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
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