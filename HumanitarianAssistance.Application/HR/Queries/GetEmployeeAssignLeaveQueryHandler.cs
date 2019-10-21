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
    public class GetEmployeeAssignLeaveQueryHandler: IRequestHandler<GetEmployeeAssignLeaveQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAssignLeaveQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeAssignLeaveQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var financialYear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.IsDefault == true);

                if (financialYear != null)
                {
                    var queryResult = await _dbContext.AssignLeaveToEmployee
                                                .Include(x => x.LeaveReasonDetails)
                                                .Where(x => x.IsDeleted == false && x.FinancialYearId == financialYear.FinancialYearId && x.EmployeeId == request.EmployeeId)
                                                .OrderByDescending(a => a.LeaveId)
                                                .ToListAsync();

                    var assignleavelist = queryResult.Select(x => new AssignLeaveToEmployeeModel
                    {
                        LeaveId = x.LeaveId,
                        LeaveReasonId = x.LeaveReasonId,
                        LeaveReasonName = x.LeaveReasonDetails.ReasonName,
                        Unit = x.LeaveReasonDetails.Unit,
                        AssignUnit = x.AssignUnit,
                        BlanceLeave = (x.AssignUnit - (x.UsedLeaveUnit ?? 0)),
                        FinancialYearId = x.FinancialYearId,
                        Description = x.Description
                    }).ToList();
                    
                    response.data.AssignLeaveToEmployeeList = assignleavelist;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Financial Year Not Found";
                }
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