using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetAllEmployeeListQueryHandler : IRequestHandler<GetAllEmployeeListQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeListQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (GetAllEmployeeListQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var employeeDetails = await _dbContext.EmployeeDetail.Where (x => x.EmployeeTypeId == (int) EmployeeTypeStatus.Active &&
                    x.IsDeleted == false &&
                    x.EmployeeProfessionalDetail.OfficeId != null).Select (x => new EmployeeDetailListModel {
                    EmployeeId = x.EmployeeID,
                        EmployeeName = x.EmployeeName,
                        EmployeeCode = x.EmployeeCode
                }).OrderByDescending (x => x.EmployeeId).ToListAsync ();
                response.data.EmployeeDetailListData = employeeDetails;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
               // var result = list1.Where(f => !list2.Contains(f));
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}