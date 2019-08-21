using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddAttendanceDetailCommandHandler : IRequestHandler<AddAttendanceDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private  readonly IMapper _mapper;

        public AddAttendanceDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }

         public async Task<ApiResponse> Handle(AddAttendanceDetailCommand request, CancellationToken cancellationToken)
        {
            DateTime OfficeInTime = new DateTime();
            DateTime OfficeOutTime = new DateTime();
            ApiResponse response = new ApiResponse();

            try
            {
                if(!request.EmployeeAttendance.Any() && request.EmployeeAttendance== null)
                {

                    throw new Exception(StaticResource.NoAttendanceToAdd);
                }

                var existrecord = await _dbContext.EmployeeAttendance.Where(x => x.Date.Date == request.EmployeeAttendance[0].Date.Date).ToListAsync();

                //gets the total working hours in a day for an office
                PayrollMonthlyHourDetail payrollMonthlyHourDetail = await _dbContext.PayrollMonthlyHourDetail
                                                                              .FirstOrDefaultAsync(x => x.OfficeId == request.EmployeeAttendance.First().OfficeId
                                                                              && x.PayrollYear == request.EmployeeAttendance.First().Date.Year 
                                                                              && x.PayrollMonth == request.EmployeeAttendance.First().Date.Month
                                                                              && x.AttendanceGroupId== request.EmployeeAttendance.First().AttendanceGroupId);
                int? officeDailyHour = payrollMonthlyHourDetail.Hours;
                int? officeMonthlyHour = payrollMonthlyHourDetail.WorkingTime;


                if (officeDailyHour != null && officeMonthlyHour != null)
                {

                    var financiallist = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);

                    foreach (var list in request.EmployeeAttendance)
                    {
                        TimeSpan? totalworkhour;
                        TimeSpan? totalovertime;
                        int? overtime = 0, workingHours = 0, workingMinutes = 0, overtimeMinutes = 0; 

                        var isemprecord = existrecord.Where(x => x.EmployeeId == list.EmployeeId && x.Date.Date == list.Date.Date).ToList();
                        totalworkhour = list.OutTime - list.InTime;

                        if (isemprecord.Count == 0)
                        {

                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                list.AttendanceTypeId = 2;
                                list.InTime = list.Date;
                                list.OutTime = list.Date;
                                totalworkhour = list.Date.Date - list.Date.Date;
                            }

                            OfficeInTime = new DateTime(list.InTime.Value.Year, list.InTime.Value.Month, list.InTime.Value.Day, payrollMonthlyHourDetail.InTime.Value.Hour, payrollMonthlyHourDetail.InTime.Value.Minute, payrollMonthlyHourDetail.InTime.Value.Second);
                            OfficeOutTime = new DateTime(list.OutTime.Value.Year, list.OutTime.Value.Month, list.OutTime.Value.Day, payrollMonthlyHourDetail.OutTime.Value.Hour, payrollMonthlyHourDetail.OutTime.Value.Minute, payrollMonthlyHourDetail.OutTime.Value.Second);

                            if (list.InTime < OfficeInTime)
                            {
                                totalovertime = OfficeInTime - list.InTime;
                                overtime += totalovertime.Value.Hours;
                                overtimeMinutes += totalovertime.Value.Minutes;
                                if (list.OutTime <= OfficeOutTime)
                                {
                                    totalworkhour = list.OutTime - OfficeInTime;
                                    workingHours += totalworkhour.Value.Hours;
                                    workingMinutes += totalworkhour.Value.Minutes;
                                }
                                if (list.OutTime > OfficeOutTime)
                                {
                                    totalovertime = list.OutTime - OfficeOutTime;
                                    overtime += totalovertime.Value.Hours;
                                    overtimeMinutes += totalovertime.Value.Minutes;
                                    totalworkhour = OfficeOutTime - OfficeInTime;
                                    workingHours += totalworkhour.Value.Hours;
                                    workingMinutes += totalworkhour.Value.Minutes;
                                }

                                list.TotalWorkTime = workingHours.ToString();
                                list.WorkTimeMinutes = workingMinutes;
                                list.HoverTimeHours = overtime;
                                list.OvertimeMinutes = overtimeMinutes;
                            }

                            else if (list.InTime >= OfficeInTime)
                            {
                                if (list.OutTime <= OfficeOutTime)
                                {
                                    totalworkhour = list.OutTime - list.InTime;
                                    workingHours += totalworkhour.Value.Hours;
                                    workingMinutes += totalworkhour.Value.Minutes;
                                }
                                if (list.OutTime > OfficeOutTime)
                                {
                                    totalovertime = list.OutTime - OfficeOutTime;
                                    overtime += totalovertime.Value.Hours;
                                    overtimeMinutes += totalovertime.Value.Minutes;
                                    totalworkhour = OfficeOutTime - list.InTime;
                                    workingHours += totalworkhour.Value.Hours;
                                    workingMinutes += totalworkhour.Value.Minutes;
                                }

                                list.TotalWorkTime = workingHours.ToString();
                                list.WorkTimeMinutes = workingMinutes;
                                list.HoverTimeHours = overtime;
                                list.OvertimeMinutes = overtimeMinutes;
                            }
                            else
                            {
                                list.TotalWorkTime = workingHours.ToString();
                                list.HoverTimeHours = overtime;
                            }

                            list.FinancialYearId = financiallist.FinancialYearId;
                            list.CreatedById = list.CreatedById;
                            list.CreatedDate = DateTime.UtcNow;
                            list.IsDeleted = false;
                            EmployeeAttendance obj = _mapper.Map<EmployeeAttendance>(list);
                            await _dbContext.EmployeeAttendance.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();
                        }

                        EmployeeMonthlyAttendance xEmployeeMonthlyAttendanceRecord = await _dbContext.EmployeeMonthlyAttendance.FirstOrDefaultAsync(x => x.EmployeeId == list.EmployeeId && x.Month == list.Date.Month && x.Year == list.Date.Year && x.IsDeleted == false);

                        EmployeeMonthlyAttendance xEmployeeMonthlyAttendance = new EmployeeMonthlyAttendance();

                        //If Employee record is present in monthly attendance table then 
                        if (xEmployeeMonthlyAttendanceRecord != null)
                        {

                            //if Employee is absent without any leave
                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                xEmployeeMonthlyAttendance.AbsentHours = xEmployeeMonthlyAttendance.AbsentHours == null ? 0 : xEmployeeMonthlyAttendance.AbsentHours;
                                xEmployeeMonthlyAttendance.AbsentHours += officeDailyHour;
                            }

                            //update total attendance hours
                            if ((workingHours != 0 || workingMinutes !=0) && overtime == 0)
                            {
                                xEmployeeMonthlyAttendanceRecord.AttendanceHours += totalworkhour.Value.Hours;
                                xEmployeeMonthlyAttendanceRecord.AttendanceMinutes += workingMinutes.Value;
                                xEmployeeMonthlyAttendanceRecord.OverTimeMinutes += overtimeMinutes.Value;

                            }
                            //update total attendance hours and also add overtime hours
                            else if ((workingHours != 0 || workingMinutes !=0) && overtime != 0)
                            {
                                xEmployeeMonthlyAttendanceRecord.AttendanceHours += totalworkhour.Value.Hours;
                                xEmployeeMonthlyAttendanceRecord.OvertimeHours = xEmployeeMonthlyAttendanceRecord.OvertimeHours ?? 0;
                                xEmployeeMonthlyAttendanceRecord.OvertimeHours += overtime;
                                xEmployeeMonthlyAttendanceRecord.AttendanceMinutes += workingMinutes.Value;
                                xEmployeeMonthlyAttendanceRecord.OverTimeMinutes += overtimeMinutes.Value;
                            }

                            //updating employee monthly attendance record
                            _dbContext.EmployeeMonthlyAttendance.Update(xEmployeeMonthlyAttendanceRecord);
                            await _dbContext.SaveChangesAsync();
                        }
                        else// if employee monthly attendance record does not exists then add a record
                        {
                            // int monthDays = GetMonthDays(modellist.FirstOrDefault().Date.Month, modellist.FirstOrDefault().Date.Year);

                            int monthDays = DateTime.DaysInMonth(list.Date.Year, list.Date.Month);

                            //if employee is absent without any leave
                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                xEmployeeMonthlyAttendance.IsDeleted = false;
                                xEmployeeMonthlyAttendance.EmployeeId = list.EmployeeId;
                                xEmployeeMonthlyAttendance.Month = list.Date.Month;
                                xEmployeeMonthlyAttendance.Year = list.Date.Year;
                                xEmployeeMonthlyAttendance.AttendanceHours = 0;
                                xEmployeeMonthlyAttendance.OvertimeHours = 0;
                                xEmployeeMonthlyAttendance.AbsentHours = officeDailyHour;
                                xEmployeeMonthlyAttendance.OfficeId = list.OfficeId;
                                xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;
                            }
                            else
                            {
                                xEmployeeMonthlyAttendance.IsDeleted = false;
                                xEmployeeMonthlyAttendance.EmployeeId = list.EmployeeId;
                                xEmployeeMonthlyAttendance.Month = list.Date.Month;
                                xEmployeeMonthlyAttendance.Year = list.Date.Year;
                                xEmployeeMonthlyAttendance.AttendanceHours = workingHours;
                                xEmployeeMonthlyAttendance.OvertimeHours = overtime;
                                xEmployeeMonthlyAttendance.OfficeId = list.OfficeId;
                                xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;
                                xEmployeeMonthlyAttendance.AttendanceMinutes = list.WorkTimeMinutes.Value;
                                xEmployeeMonthlyAttendance.OverTimeMinutes = list.OvertimeMinutes.Value;
                            }

                            Advances xAdvances = await _dbContext.Advances.Where(x => x.IsDeleted == false && x.IsApproved == true
                                                                            && x.EmployeeId == list.EmployeeId && x.OfficeId == list.OfficeId && x.IsDeducted == false
                                                                            && x.AdvanceDate <= DateTime.Now).FirstOrDefaultAsync();
                            if (xAdvances != null)
                            {
                                xEmployeeMonthlyAttendance.AdvanceId = xAdvances.AdvancesId;
                                xEmployeeMonthlyAttendance.IsAdvanceApproved = xAdvances.IsApproved;
                                xEmployeeMonthlyAttendance.AdvanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                            }

                            await _dbContext.EmployeeMonthlyAttendance.AddAsync(xEmployeeMonthlyAttendance);
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    //If Employee is not present then check the leave table and update leave record accordingly
                    List<EmployeeApplyLeave> xEmployeeLeaveDetailList = await _dbContext.EmployeeApplyLeave
                                                                                  .Include(x => x.EmployeeDetails.EmployeeProfessionalDetail)
                                                                                  .Where(x => x.FromDate.Date >= request.EmployeeAttendance.First().Date.Date &&
                                                                                    x.ToDate.Date <= request.EmployeeAttendance.First().Date.Date &&
                                                                                    x.EmployeeDetails.EmployeeProfessionalDetail.OfficeId == request.EmployeeAttendance.First().OfficeId
                                                                                    && x.IsDeleted == false)
                                                                                  .ToListAsync();

                    //If leave Exists then check the status of leave if approved then add the working hours of the day in attendance hours
                    if (xEmployeeLeaveDetailList.Count != 0)
                    {
                        int monthDays = DateTime.DaysInMonth(request.EmployeeAttendance.First().Date.Year, request.EmployeeAttendance.First().Date.Month);

                        foreach (EmployeeApplyLeave xEmployeeApplyLeave in xEmployeeLeaveDetailList)
                        {
                            EmployeeMonthlyAttendance xEmployeeMonthlyAttendanceRecord = await _dbContext.EmployeeMonthlyAttendance.FirstOrDefaultAsync(x => x.EmployeeId == xEmployeeApplyLeave.EmployeeId && x.Month == request.EmployeeAttendance.First().Date.Date.Month && x.Year == request.EmployeeAttendance.First().Date.Date.Year && x.IsDeleted == false);

                            if (xEmployeeMonthlyAttendanceRecord == null)
                            {

                                EmployeeMonthlyAttendance xEmployeeMonthlyAttendance = new EmployeeMonthlyAttendance();

                                if (xEmployeeApplyLeave.ApplyLeaveStatusId == 1)
                                {
                                    //remove hardcoded attendance hours once all office hours are available in master table
                                    xEmployeeMonthlyAttendance.IsDeleted = false;
                                    xEmployeeMonthlyAttendance.OfficeId = request.EmployeeAttendance.First().OfficeId;
                                    xEmployeeMonthlyAttendance.EmployeeId = xEmployeeApplyLeave.EmployeeId;
                                    xEmployeeMonthlyAttendance.Month = request.EmployeeAttendance.First().Date.Month;
                                    xEmployeeMonthlyAttendance.Year = request.EmployeeAttendance.First().Date.Year;
                                    xEmployeeMonthlyAttendance.AttendanceHours = xEmployeeMonthlyAttendance.AttendanceHours != null ? xEmployeeMonthlyAttendance.AttendanceHours.Value : 0;
                                    //xEmployeeMonthlyAttendance.AttendanceHours += officeDailyHour;
                                    xEmployeeMonthlyAttendance.LeaveHours += xEmployeeMonthlyAttendance.LeaveHours != null ? xEmployeeMonthlyAttendance.LeaveHours : 0;
                                    xEmployeeMonthlyAttendance.LeaveHours += officeDailyHour;
                                    xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;

                                    Advances xAdvances = await _dbContext.Advances.Where(x => x.IsDeleted == false && x.IsApproved == true && x.IsDeducted == false
                                                                               && x.EmployeeId == xEmployeeApplyLeave.EmployeeId && x.OfficeId == request.EmployeeAttendance.First().OfficeId
                                                                               && x.AdvanceDate <= DateTime.Now).FirstOrDefaultAsync();
                                    if (xAdvances != null)
                                    {
                                        xEmployeeMonthlyAttendance.AdvanceId = xAdvances.AdvancesId;
                                        xEmployeeMonthlyAttendance.IsAdvanceApproved = xAdvances.IsApproved;
                                        xEmployeeMonthlyAttendance.AdvanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                                    }

                                    await _dbContext.EmployeeMonthlyAttendance.AddAsync(xEmployeeMonthlyAttendance);
                                    await _dbContext.SaveChangesAsync();
                                }
                                else
                                {
                                    xEmployeeMonthlyAttendance.IsDeleted = false;
                                    xEmployeeMonthlyAttendance.OfficeId = request.EmployeeAttendance.First().OfficeId;
                                    xEmployeeMonthlyAttendance.EmployeeId = xEmployeeApplyLeave.EmployeeId;
                                    xEmployeeMonthlyAttendance.Month = request.EmployeeAttendance.First().Date.Month;
                                    xEmployeeMonthlyAttendance.Year = request.EmployeeAttendance.First().Date.Year;
                                    xEmployeeMonthlyAttendance.AbsentHours = xEmployeeMonthlyAttendance.AbsentHours == null ? 0 : xEmployeeMonthlyAttendance.AbsentHours;
                                    xEmployeeMonthlyAttendance.AbsentHours += officeDailyHour;
                                    xEmployeeMonthlyAttendance.TotalDuration = officeMonthlyHour;

                                    Advances xAdvances = await _dbContext.Advances.Where(x => x.IsDeleted == false && x.IsApproved == true && x.IsDeducted == false
                                                                              && x.EmployeeId == xEmployeeApplyLeave.EmployeeId && x.OfficeId == request.EmployeeAttendance.First().OfficeId
                                                                              && x.AdvanceDate < DateTime.Now).FirstOrDefaultAsync();
                                    if (xAdvances != null)
                                    {
                                        xEmployeeMonthlyAttendance.AdvanceId = xAdvances.AdvancesId;
                                        xEmployeeMonthlyAttendance.AdvanceAmount = xAdvances.AdvanceAmount - xAdvances.RecoveredAmount;
                                        xEmployeeMonthlyAttendance.IsAdvanceApproved = xAdvances.IsApproved;
                                    }

                                    await _dbContext.EmployeeMonthlyAttendance.AddAsync(xEmployeeMonthlyAttendance);
                                    await _dbContext.SaveChangesAsync();
                                }
                            }
                            else//if Employee Monthly Attendance Record is present then add leave hours
                            {
                                if (xEmployeeApplyLeave.ApplyLeaveStatusId == (int)ApplyLeaveStatus.Approve)
                                {
                                    xEmployeeMonthlyAttendanceRecord.LeaveHours = officeDailyHour;
                                    _dbContext.EmployeeMonthlyAttendance.Update(xEmployeeMonthlyAttendanceRecord);
                                    await _dbContext.SaveChangesAsync();
                                }
                            }
                        }
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Office Hours Not Set";

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