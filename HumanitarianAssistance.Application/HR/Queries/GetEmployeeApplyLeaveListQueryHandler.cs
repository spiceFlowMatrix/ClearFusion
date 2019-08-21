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
    public class GetEmployeeApplyLeaveListQueryHandler: IRequestHandler<GetEmployeeApplyLeaveListQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeApplyLeaveListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeApplyLeaveListQuery request, CancellationToken cancellationToken)
        {
             ApiResponse response = new ApiResponse();

            try
            {
                var list = await _dbContext.EmployeeApplyLeave
                                                     .Include(x => x.LeaveReasonDetails)
                                                     .Include(x => x.EmployeeDetails)
                                                     .Include(x => x.EmployeeDetails.EmployeeProfessionalDetail)
                                                     .Where(x => x.EmployeeDetails.EmployeeProfessionalDetail.OfficeId == request.OfficeId && x.ApplyLeaveStatusId == null)
                                                     .OrderByDescending(x => x.EmployeeId)
                                                     .ToListAsync();

                var empapplyleavelist = list.Select(x => new EmployeeApplyLeaveModel
                {
                    ApplyLeaveId = x.ApplyLeaveId,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.EmployeeDetails?.EmployeeName ?? "",
                    EmployeeCode = x.EmployeeDetails?.EmployeeCode ?? "",
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    LeaveReasonId = x.LeaveReasonId,
                    LeaveReasonName = x.LeaveReasonDetails?.ReasonName ?? null,
                    ApplyLeaveStatusId = x.ApplyLeaveStatusId,
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