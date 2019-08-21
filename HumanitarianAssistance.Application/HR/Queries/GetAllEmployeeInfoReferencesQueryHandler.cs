using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeInfoReferencesQueryHandler : IRequestHandler<GetAllEmployeeInfoReferencesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeInfoReferencesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeInfoReferencesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeHistoryRecord = await _dbContext.EmployeeInfoReferences.Where(x => x.IsDeleted == false &&
                                                                                               x.EmployeeID == request.EmployeeId)
                                                                                               .ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeRelativeInfoList = employeeHistoryRecord.Select(x => new EmployeeRelativeInfoModel
                    {
                        EmployeeInfoReferencesId = x.EmployeeInfoReferencesId,
                        Name = x.Name,
                        Organization = x.Organization,
                        Position = x.Position,
                        Relationship = x.Relationship,
                        EmployeeID = x.EmployeeID,
                        Email = x.Email,
                        PhoneNo = x.PhoneNo
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