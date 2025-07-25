using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppliedLeavesQueryHandler : IRequestHandler<GetEmployeeAppliedLeavesQuery, object>
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
                
                if (queryResult.Any())
                {
                    // //Get hours for employee attendance group in office
                    // var obj = (from e in _dbContext.EmployeeDetail.Where(x => x.EmployeeID == request.EmployeeId)
                    //            join p in _dbContext.EmployeeProfessionalDetail on e.EmployeeID equals p.EmployeeId
                    //            join o in _dbContext.OfficeDetail on p.OfficeId equals o.OfficeId
                    //            join h in _dbContext.PayrollMonthlyHourDetail on p.AttendanceGroupId equals h.AttendanceGroupId
                    //            where h.OfficeId == p.OfficeId
                    //            select new
                    //            {
                    //                h.Hours,
                    //                o.OfficeName
                    //            }).FirstOrDefault();

                    // if (obj == null)
                    // {
                    //     throw new Exception(string.Format(StaticResource.PayrollDailyHoursNotSaved, DateTime.Now.Month, obj.OfficeName));
                    // }

                    var empapplyleavelist = queryResult.Select(x => new
                    {
                        ApplyLeaveId = x.ApplyLeaveId,
                        EmployeeId = x.EmployeeId,
                        FromDate = x.FromDate.ToShortDateString(),
                        ToDate = x.ToDate.ToShortDateString(),
                        LeaveHoursCount = x.AppliedLeaveCount,
                        LeaveReasonId = x.LeaveReasonId,
                        LeaveReasonName = x.LeaveReasonDetails?.ReasonName ?? null,
                        ApplyLeaveStatusId = x.ApplyLeaveStatusId,
                        ApplyLeaveStatus = x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve ? "Approve" : x.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Reject ? "Reject" : "Pending",
                        Remarks = x.Remarks
                    }).ToList();

                    response.Add("LeaveList", empapplyleavelist);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;

        }
    }
}