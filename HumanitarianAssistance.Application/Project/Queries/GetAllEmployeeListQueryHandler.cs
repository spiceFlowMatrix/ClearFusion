using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Queries
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
                response.data.EmployeeDetailListData = await _dbContext.EmployeeDetail.Where(x => x.EmployeeTypeId != (int)EmployeeTypeStatus.Terminated &&
                                                                                                           x.IsDeleted == false).Select(x => new EmployeeDetailListModel
                                                                                                           {
                                                                                                               EmployeeId = x.EmployeeID,
                                                                                                               EmployeeName = x.EmployeeName
                                                                                                           }
                                                                                                      ).OrderByDescending(x => x.EmployeeId).ToListAsync();

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
