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
                EmployeeApplyLeave obj = _dbContext.EmployeeApplyLeave.FirstOrDefault(x => x.IsDeleted == false && x.ApplyLeaveId == request.Id);

                if (obj != null)
                {
                    //Leave Approved
                    if (request.Approved)
                    {
                        //get exiting record for applied leave 
                        var existrecord = _dbContext.AssignLeaveToEmployee.FirstOrDefault(x => x.EmployeeId == request.EmployeeId && x.LeaveReasonId == obj.LeaveReasonId);

                        //update existing record with availed leave hours
                        if (existrecord != null)
                        {
                            int? usedleaveunit = existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit;
                            existrecord.UsedLeaveUnit = usedleaveunit + obj.AppliedLeaveCount;
                            existrecord.ModifiedById = request.ModifiedById;
                            existrecord.ModifiedDate = DateTime.UtcNow;
                            existrecord.IsDeleted = false;
                            _dbContext.AssignLeaveToEmployee.Update(existrecord);
                            _dbContext.SaveChanges();
                        }
                        
                        obj.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Approve;
                        _dbContext.EmployeeApplyLeave.Update(obj);
                        _dbContext.SaveChanges();
                    }
                    else //Leave Rejected
                    {
                        //get exiting record for Assigned leave 
                        var existrecord = _dbContext.AssignLeaveToEmployee.FirstOrDefault(x => x.EmployeeId == request.EmployeeId && x.LeaveReasonId == obj.LeaveReasonId);
                        // var appliedLeave = await _dbContext.EmployeeApplyLeave.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ApplyLeaveId == request.Id);

                        if (existrecord != null)
                        {
                            int? usedleaveunit = existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit;

                            //subtract applied leave hours from used leave hour
                            // existrecord.UsedLeaveUnit = usedleaveunit - appliedLeave.AppliedLeaveCount;
                            // _dbContext.AssignLeaveToEmployee.Update(existrecord);

                            //Update status to rejected
                            obj.ApplyLeaveStatusId = (int)ApplyLeaveStatus.Reject;
                            _dbContext.EmployeeApplyLeave.Update(obj);
                            _dbContext.SaveChanges();
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