using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class ApplyEmployeeLeaveCommandHandler : IRequestHandler<ApplyEmployeeLeaveCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public ApplyEmployeeLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(ApplyEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                //get Total leave in days
                int leaveDays = request.ToDate.Subtract(request.FromDate).Days+1;

                //get total leave hours
                // int totalLeaveHours = obj.Hours.Value * leaveDays;

                //get exiting record for applied leave 
                var existrecord = await _dbContext.AssignLeaveToEmployee.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.LeaveReasonId == request.LeaveReasonId);
                
                //get balance leave hours
                int balanceleaveHours = (int)existrecord.AssignUnit - (int)(existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit);

                // check if leave balance hour is greater than availed hours of leave
                if (balanceleaveHours >= (request.LeaveApplied))
                {
                    EmployeeApplyLeave applyleavelist = new EmployeeApplyLeave();
                    applyleavelist.EmployeeId = request.EmployeeId;
                    applyleavelist.FromDate = request.FromDate;
                    applyleavelist.ToDate = request.ToDate;
                    applyleavelist.LeaveReasonId = request.LeaveReasonId;
                    applyleavelist.Remarks = request.Remarks;
                    applyleavelist.FinancialYearId = existrecord.FinancialYearId;
                    applyleavelist.CreatedById = request.CreatedById;
                    applyleavelist.CreatedDate = DateTime.UtcNow;
                    applyleavelist.AppliedLeaveCount = request.LeaveApplied;
                    applyleavelist.IsDeleted = false;

                    await _dbContext.EmployeeApplyLeave.AddAsync(applyleavelist);
                    await _dbContext.SaveChangesAsync();
                } 
                else
                {
                    throw new Exception("Applied leave exceeded from Balance leave");
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