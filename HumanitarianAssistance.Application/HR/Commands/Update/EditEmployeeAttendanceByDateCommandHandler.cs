using System;
using System.Linq;
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
    public class EditEmployeeAttendanceByDateCommandHandler: IRequestHandler<EditEmployeeAttendanceByDateCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditEmployeeAttendanceByDateCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditEmployeeAttendanceByDateCommand model, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                TimeSpan? totalworkhour;
                TimeSpan? totalovertime;
                int? overtime = 0, workingHours = 0;
                DateTime OfficeInTime = new DateTime();
                DateTime OfficeOutTime = new DateTime();
                
                var existrecord = await _dbContext.EmployeeAttendance.FirstOrDefaultAsync(x => x.AttendanceId == model.AttendanceId);
                
                if (existrecord != null)
                {
                    PayrollMonthlyHourDetail payrollMonthlyHourDetail = await _dbContext.PayrollMonthlyHourDetail.FirstOrDefaultAsync(x => x.OfficeId == model.OfficeId && x.PayrollYear == model.Date.Year && x.PayrollMonth == model.Date.Month);
                    
                    int officeDailyHour = payrollMonthlyHourDetail.Hours.Value;
                    int officeMonthlyHour = payrollMonthlyHourDetail.WorkingTime.Value;

                    OfficeInTime = new DateTime(model.InTime.Value.Year, model.InTime.Value.Month, model.InTime.Value.Day, payrollMonthlyHourDetail.InTime.Value.Hour, payrollMonthlyHourDetail.InTime.Value.Minute, payrollMonthlyHourDetail.InTime.Value.Second);
                    OfficeOutTime = new DateTime(model.OutTime.Value.Year, model.OutTime.Value.Month, model.OutTime.Value.Day, payrollMonthlyHourDetail.OutTime.Value.Hour, payrollMonthlyHourDetail.OutTime.Value.Minute, payrollMonthlyHourDetail.OutTime.Value.Second);

                    if (model.InTime < OfficeInTime)
                    {
                        totalovertime = OfficeInTime - model.InTime;
                        overtime += totalovertime.Value.Hours;
                        if (model.OutTime <= OfficeOutTime)
                        {
                            totalworkhour = model.OutTime - OfficeInTime;
                            workingHours += totalworkhour.Value.Hours;
                        }
                        if (model.OutTime > OfficeOutTime)
                        {
                            totalovertime = model.OutTime - OfficeOutTime;
                            overtime += totalovertime.Value.Hours;
                            totalworkhour = OfficeOutTime - OfficeInTime;
                            workingHours += totalworkhour.Value.Hours;
                        }
                    }

                    if (model.InTime >= OfficeInTime && model.InTime < OfficeOutTime)
                    {
                        if (model.OutTime <= OfficeOutTime)
                        {
                            totalworkhour = model.OutTime - model.InTime;
                            workingHours += totalworkhour.Value.Hours;
                        }
                        if (model.OutTime > OfficeOutTime)
                        {
                            totalovertime = model.OutTime - OfficeOutTime;
                            overtime += totalovertime.Value.Hours;
                            totalworkhour = OfficeOutTime - model.InTime;
                            workingHours += totalworkhour.Value.Hours;
                        }
                    }

                    if (model.InTime >= OfficeOutTime)
                    {
                        workingHours = 0;
                        totalovertime = model.OutTime - model.InTime;
                        overtime += totalovertime.Value.Hours;
                    }

                    if (model.OutTime <= OfficeInTime)
                    {
                        workingHours = 0;
                        totalovertime = model.OutTime - model.InTime;
                        overtime += totalovertime.Value.Hours;
                    }


                    totalworkhour = model.OutTime - model.InTime;
                    if (totalworkhour.ToString() == "00:00:00" || existrecord.AttendanceTypeId == (int)AttendanceType.A)
                    {
                        existrecord.AttendanceTypeId = (int)AttendanceType.A;
                        existrecord.InTime = model.Date;
                        existrecord.OutTime = model.Date;
                        totalworkhour = model.Date.Date - model.Date.Date;
                    }
                    else
                    {
                        existrecord.TotalWorkTime = workingHours.ToString();
                        existrecord.HoverTimeHours = overtime;
                    }


                    existrecord.InTime = model.InTime;
                    existrecord.OutTime = model.OutTime;
                    existrecord.AttendanceTypeId = model.AttendanceTypeId;
                    existrecord.ModifiedById = model.ModifiedById;
                    existrecord.ModifiedDate = model.ModifiedDate;
                    existrecord.IsDeleted = model.IsDeleted;

                    _dbContext.EmployeeAttendance.Update(existrecord);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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