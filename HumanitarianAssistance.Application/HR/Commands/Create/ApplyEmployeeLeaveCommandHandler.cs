using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class ApplyEmployeeLeaveCommandHandler : IRequestHandler<ApplyEmployeeLeaveCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public ApplyEmployeeLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<object> Handle(ApplyEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
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

                //get Total leave in days
                int leaveDays = request.ToDate.Subtract(request.FromDate).Days+1;

                //get total leave hours
                int totalLeaveHours = hour.Hours.Value * leaveDays;

                //get exiting record for applied leave 
                var existrecord = await _dbContext.AssignLeaveToEmployee.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.LeaveReasonId == request.LeaveReasonId);
                
                //get balance leave hours
                int balanceleaveHours = (int)existrecord.AssignUnit - (int)(existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit);

                // check if leave balance hour is greater than availed hours of leave
                if (balanceleaveHours >= (balanceleaveHours-totalLeaveHours))
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
                    applyleavelist.IsDeleted = false;

                    await _dbContext.EmployeeApplyLeave.AddAsync(applyleavelist);
                    await _dbContext.SaveChangesAsync();

                    //update existing record with availed leave hours
                    if (existrecord != null)
                    {
                        int? usedleaveunit = existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit;
                        existrecord.UsedLeaveUnit = usedleaveunit + totalLeaveHours;
                        existrecord.ModifiedById = request.ModifiedById;
                        existrecord.ModifiedDate = DateTime.UtcNow;
                        existrecord.IsDeleted = false;
                        _dbContext.AssignLeaveToEmployee.Update(existrecord);
                        await _dbContext.SaveChangesAsync();
                    }
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