using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllEmployeeListQueryHandler : IRequestHandler<GetAllEmployeeListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                response.data.EmployeeDetailListData = await _dbContext.EmployeeDetail
                                                            .Where(x => x.EmployeeTypeId == (int)EmployeeTypeStatus.Active)
                                                            .Select(x => new EmployeeDetailListModel
                                                            {
                                                                EmployeeId = x.EmployeeID,
                                                                EmployeeName = x.EmployeeName,
                                                                EmployeeCode = x.EmployeeCode ?? x.EmployeeProfessionalDetail.OfficeDetail.OfficeCode + x.EmployeeID,
                                                                CodeEmployeeName = x.EmployeeCode != null ? x.EmployeeCode + " - " + x.EmployeeName : x.EmployeeProfessionalDetail.OfficeDetail.OfficeCode + x.EmployeeID + " - " + x.EmployeeName
                                                            }).ToListAsync();
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
