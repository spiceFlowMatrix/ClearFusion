using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppliedLeaveByIdQueryHandler: IRequestHandler<GetEmployeeAppliedLeaveByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAppliedLeaveByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeAppliedLeaveByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                var queryResult = await _dbContext.EmployeeApplyLeave.Include(x => x.LeaveReasonDetails).Where(x => x.EmployeeId == request.EmployeeId).OrderByDescending(o => o.ApplyLeaveId).ToListAsync();

                var empapplyleavelist = queryResult.Select(x => new EmployeeApplyLeaveModel
                {
                    ApplyLeaveId = x.ApplyLeaveId,
                    EmployeeId = x.EmployeeId,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    LeaveReasonId = x.LeaveReasonId,
                    LeaveReasonName = x.LeaveReasonDetails?.ReasonName ?? null,
                    ApplyLeaveStatusId = x.ApplyLeaveStatusId,
                    ApplyLeaveStatus = x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve ? "Approve" : x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Reject ? "Reject" : "",
                    Remarks = x.Remarks
                }).ToList();
                
                response.data.EmployeeApplyLeaveList = empapplyleavelist;
                
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