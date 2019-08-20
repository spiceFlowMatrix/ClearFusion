using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class ApproveEmployeeLeaveCommandHandler: IRequestHandler<ApproveEmployeeLeaveCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public ApproveEmployeeLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(ApproveEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);
                foreach (var item in request.AppliedLeave)
                {
                    var existleaverecord = await _dbContext.EmployeeApplyLeave.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ApplyLeaveId == item.ApplyLeaveId);

                    if (existleaverecord != null)
                    {
                        existleaverecord.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Approve;
                        existleaverecord.ModifiedById = request.ModifiedById;
                        existleaverecord.ModifiedDate = DateTime.UtcNow;
                        existleaverecord.IsDeleted = false;
                        _dbContext.EmployeeApplyLeave.Update(existleaverecord);
                        await _dbContext.SaveChangesAsync();

                        for (DateTime i = existleaverecord.FromDate.Date; i <= existleaverecord.ToDate.Date; i = i.AddDays(1))
                        {
                            var existrecord = await _dbContext.EmployeeAttendance.FirstOrDefaultAsync(x => x.Date.Date == i.Date.Date && x.EmployeeId == existleaverecord.EmployeeId);
                            
                            if (existrecord == null)
                            {
                                EmployeeAttendance attendance = new EmployeeAttendance();
                                attendance.EmployeeId = existleaverecord.EmployeeId == null ? 0 : existleaverecord.EmployeeId.Value;
                                attendance.AttendanceTypeId = (int)AttendanceType.L;
                                attendance.LeaveReasonId = existleaverecord.LeaveReasonId;
                                attendance.Date = i.Date;
                                attendance.InTime = i.Date.Date;
                                attendance.OutTime = i.Date.Date;
                                attendance.TotalWorkTime = "00";
                                attendance.HoverTimeHours = 0;
                                attendance.FinancialYearId = financialyear.FinancialYearId;
                                attendance.CreatedById = request.CreatedById;
                                attendance.CreatedDate = DateTime.UtcNow;
                                attendance.IsDeleted = false;
                                await _dbContext.EmployeeAttendance.AddAsync(attendance);
                                await _dbContext.SaveChangesAsync();
                            }
                            else
                            {
                                existrecord.AttendanceTypeId = (int)AttendanceType.L;
                                existrecord.LeaveReasonId = existleaverecord.LeaveReasonId;
                                existrecord.TotalWorkTime = "00";
                                existrecord.HoverTimeHours = 0;
                                existrecord.ModifiedById = request.ModifiedById;
                                existrecord.ModifiedDate = DateTime.UtcNow;
                                _dbContext.EmployeeAttendance.Update(existrecord);
                                await _dbContext.SaveChangesAsync();
                            }
                        }
                    }
                }
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