using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppliedLeavesQueryHandler: IRequestHandler<GetEmployeeAppliedLeavesQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAppliedLeavesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeAppliedLeavesQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

           try
           {
               var queryResult = await _dbContext.EmployeeApplyLeave.Include(x => x.LeaveReasonDetails).Where(x => x.EmployeeId == request.EmployeeId).OrderByDescending(o => o.ApplyLeaveId).ToListAsync();

               //Get hours for employee attendance group in office
                var hour = (from e in _dbContext.EmployeeDetail.Where(x=> x.EmployeeID == request.EmployeeId)
                             join p in _dbContext.EmployeeProfessionalDetail on  e.EmployeeID equals p.EmployeeId 
                             join h in _dbContext.PayrollMonthlyHourDetail on p.AttendanceGroupId equals h.AttendanceGroupId 
                             where h.OfficeId == p.OfficeId
                             select new 
                             {
                                 h.Hours
                             }).FirstOrDefault();

                if(hour == null)
                {
                    throw new Exception("Office Hours not set");
                }

                var empapplyleavelist = queryResult.Select(x => new
                {
                    ApplyLeaveId = x.ApplyLeaveId,
                    EmployeeId = x.EmployeeId,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    LeaveHoursCount= (x.ToDate.Subtract(x.FromDate).Days +1) * hour.Hours.Value,
                    LeaveReasonId = x.LeaveReasonId,
                    LeaveReasonName = x.LeaveReasonDetails?.ReasonName ?? null,
                    ApplyLeaveStatusId = x.ApplyLeaveStatusId,
                    ApplyLeaveStatus = x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve ? "Approve" : x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Reject ? "Reject" : "Pending",
                    Remarks = x.Remarks
                }).ToList();

                response.Add("LeaveList", empapplyleavelist);
           }
           catch(Exception ex)
           {
               throw ex;
           }

           return response;

        }
    }
}