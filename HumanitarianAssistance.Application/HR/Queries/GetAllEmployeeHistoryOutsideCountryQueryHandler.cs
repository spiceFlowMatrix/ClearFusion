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
    public class GetAllEmployeeHistoryOutsideCountryQueryHandler : IRequestHandler<GetAllEmployeeHistoryOutsideCountryQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeHistoryOutsideCountryQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllEmployeeHistoryOutsideCountryQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeHistoryOutsideCountry> employeeHistoryRecord = await _dbContext.EmployeeHistoryOutsideCountry.Where(x => x.IsDeleted == false &&
                                                                                                      x.EmployeeID == request.EmployeeId)
                                                                                          .ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeHistoryOutsideOrganizationList = employeeHistoryRecord.Select(x => new EmployeeHistoryOutsideOrganizationModel
                    {
                        EmployeeHistoryOutsideCountryId = x.EmployeeHistoryOutsideCountryId,
                        EmployeeID = Convert.ToInt32(x.EmployeeID),
                        EmploymentFrom = x.EmploymentFrom == null ? null : (DateTime?)Convert.ToDateTime(x.EmploymentFrom),
                        EmploymentTo = x.EmploymentTo == null ? null : (DateTime?)Convert.ToDateTime(x.EmploymentTo),
                        MonthlySalary = x.MonthlySalary.ToString(),
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
