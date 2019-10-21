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
    public class GetAllEmployeeRelativeInformationQueryHandler : IRequestHandler<GetAllEmployeeRelativeInformationQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeRelativeInformationQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeRelativeInformationQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeRelativeInfo> employeeHistoryRecord = await _dbContext.EmployeeRelativeInfo.Where(x => x.IsDeleted == false &&
                                                                                             x.EmployeeID == request.EmployeeId)
                                                                                             .ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeRelativeInfoList = employeeHistoryRecord.Select(x => new EmployeeRelativeInfoModel
                    {
                        EmployeeRelativeInfoId = x.EmployeeRelativeInfoId,
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
