using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetEmployeeDetailByOfficeIdQueryHandler : IRequestHandler<GetEmployeeDetailByOfficeIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeDetailByOfficeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeDetailByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var OfficeDetail = await _dbContext.OfficeDetail.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId);

                response.data.EmployeeDetailListData = await _dbContext.EmployeeDetail.Include(x => x.EmployeeProfessionalDetail).Where(x => x.EmployeeProfessionalDetail.OfficeId == request.OfficeId && x.EmployeeTypeId != 1 && x.IsDeleted == false).Select(x => new EmployeeDetailListModel
                {
                    EmployeeId = x.EmployeeID,
                    EmployeeName = x.EmployeeName,
                    EmployeeCode = x.EmployeeCode != null ? x.EmployeeCode : OfficeDetail.OfficeCode + x.EmployeeID,
                    CodeEmployeeName = x.EmployeeCode != null ? x.EmployeeCode + " - " + x.EmployeeName : OfficeDetail.OfficeCode + x.EmployeeID + " - " + x.EmployeeName
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
