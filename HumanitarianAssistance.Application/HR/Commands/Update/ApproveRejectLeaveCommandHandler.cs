using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.HR;
using System.Linq;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveRejectLeaveCommandHandler : IRequestHandler<ApproveRejectLeaveCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public ApproveRejectLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(ApproveRejectLeaveCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                EmployeeApplyLeave obj = await _dbContext.EmployeeApplyLeave.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ApplyLeaveId == request.Id);

                if (obj != null)
                {
                    //Leave Approved
                    if (request.Approved)
                    {
                        obj.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Approve;
                        _dbContext.EmployeeApplyLeave.Update(obj);
                        await _dbContext.SaveChangesAsync();
                    }
                    else //Leave Rejected
                    {
                        //Get hours for employee attendance group in office
                        // var hour = (from e in _dbContext.EmployeeDetail.Where(x => x.EmployeeID == request.EmployeeId)
                        //             join p in _dbContext.EmployeeProfessionalDetail on e.EmployeeID equals p.EmployeeId
                        //             join h in _dbContext.PayrollMonthlyHourDetail on p.AttendanceGroupId equals h.AttendanceGroupId
                        //             where h.OfficeId == p.OfficeId
                        //             select new
                        //             {
                        //                 h.Hours
                        //             }).FirstOrDefault();

                        // if (hour == null)
                        // {
                        //     throw new Exception("Office Hours not set");
                        // }

                        //get exiting record for Assigned leave 
                        var existrecord = await _dbContext.AssignLeaveToEmployee.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.LeaveReasonId == obj.LeaveReasonId);
                        var appliedLeave = await _dbContext.EmployeeApplyLeave.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ApplyLeaveId == request.Id);

                        if (existrecord != null)
                        {
                            int? usedleaveunit = existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit;
                            // get Total leave in days
                            // int leaveDays = obj.ToDate.Subtract(obj.FromDate).Days + 1;
                            // get total leave hours
                            // int totalLeaveHours = hour.Hours.Value * leaveDays;

                            //subtract applied leave hours from used leave hour
                            existrecord.UsedLeaveUnit = usedleaveunit - appliedLeave.AppliedLeaveCount;
                            _dbContext.AssignLeaveToEmployee.Update(existrecord);

                            //Update status to rejected
                            obj.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Reject;
                            _dbContext.EmployeeApplyLeave.Update(obj);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                
                success = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return success;
        }
    }
}