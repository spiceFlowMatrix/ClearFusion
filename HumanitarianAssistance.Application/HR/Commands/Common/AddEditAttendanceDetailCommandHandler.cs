using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.HR;
using System.Linq;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using AutoMapper;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Commands.Common
{
    public class AddEditAttendanceDetailCommandHandler : IRequestHandler<AddEditAttendanceDetailCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddEditAttendanceDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<object> Handle(AddEditAttendanceDetailCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            DateTime OfficeInTime = new DateTime();
            DateTime OfficeOutTime = new DateTime();
            try
            {
                if(!request.EmployeeAttendance.Any() && request.EmployeeAttendance== null)
                {

                    throw new Exception(StaticResource.NoAttendanceToAdd);
                }

                var existrecord = await _dbContext.EmployeeAttendance.Where(x => x.Date.Date == request.EmployeeAttendance[0].Date.Date).ToListAsync();

                //gets the total working hours in a day for an office
                List<PayrollMonthlyHourDetail> payrollMonthlyHourDetail = await _dbContext.PayrollMonthlyHourDetail
                                                                              .Where(x => request.OfficeIds.Contains(x.OfficeId)
                                                                              && x.PayrollYear == request.AttendanceDate.Year 
                                                                              && x.PayrollMonth == request.AttendanceDate.Month
                                                                              && request.EmployeeAttendance.Select(y=>y.AttendanceGroupId).Distinct().Contains(x.AttendanceGroupId))
                                                                            .ToListAsync();
                // int? officeDailyHour = payrollMonthlyHourDetail.Hours;
                // int? officeMonthlyHour = payrollMonthlyHourDetail.WorkingTime;


                // if (payrollMonthlyHourDetail.Count() == request.EmployeeAttendance.Select(y=>y.AttendanceGroupId).Distinct().Count())
                // {

                    var financiallist = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);

                    foreach (var list in request.EmployeeAttendance)
                    {
                        int? officeDailyHour = payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.Hours).FirstOrDefault();
                        int? officeMonthlyHour = payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.WorkingTime).FirstOrDefault();

                        TimeSpan? totalworkhour;
                        TimeSpan? totalovertime;
                        int? overtime = 0, workingHours = 0, workingMinutes = 0, overtimeMinutes = 0; 

                        var isemprecord = existrecord.FirstOrDefault(x => x.EmployeeId == list.EmployeeId && x.Date.Date == list.Date.Date && x.IsDeleted == false);
                        // var existingEmpRecord = isemprecord;
                        totalworkhour = list.OutTime - list.InTime;

                        if (isemprecord == null)
                        {

                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                list.AttendanceTypeId = 2;
                                // list.InTime = list.Date;
                                // list.OutTime = list.Date;
                                // totalworkhour = list.Date.Date - list.Date.Date;
                            }

                            OfficeInTime = new DateTime(list.InTime.Value.Year, list.InTime.Value.Month, list.InTime.Value.Day, 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.InTime.Value.Hour).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.InTime.Value.Minute).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.InTime.Value.Second).FirstOrDefault());
                            OfficeOutTime = new DateTime(list.OutTime.Value.Year, list.OutTime.Value.Month, list.OutTime.Value.Day,
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.OutTime.Value.Hour).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.OutTime.Value.Minute).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.OutTime.Value.Second).FirstOrDefault());

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
                        else 
                        {
                            if (totalworkhour.ToString() == "00:00:00" || list.AttendanceTypeId == (int)AttendanceType.A)
                            {
                                list.AttendanceTypeId = 2;
                                // list.InTime = list.Date;
                                // list.OutTime = list.Date;
                                // totalworkhour = list.Date.Date - list.Date.Date;
                            }

                            OfficeInTime = new DateTime(list.InTime.Value.Year, list.InTime.Value.Month, list.InTime.Value.Day, 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.InTime.Value.Hour).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.InTime.Value.Minute).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.InTime.Value.Second).FirstOrDefault());
                            OfficeOutTime = new DateTime(list.OutTime.Value.Year, list.OutTime.Value.Month, list.OutTime.Value.Day,
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.OutTime.Value.Hour).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.OutTime.Value.Minute).FirstOrDefault(), 
                                            payrollMonthlyHourDetail.Where(x=>x.AttendanceGroupId == list.AttendanceGroupId && x.OfficeId == list.OfficeId).Select(x=> x.OutTime.Value.Second).FirstOrDefault());

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
                            list.ModifiedById = list.CreatedById;
                            list.ModifiedDate = DateTime.UtcNow;
                            list.IsDeleted = false;
                            list.AttendanceId = isemprecord.AttendanceId;
                            _mapper.Map(list, isemprecord);
                            await _dbContext.SaveChangesAsync();
                        }

                        
                    }

                    success = true;
                // }
                // else
                // {
                //     throw new Exception("Office Hours Not Set");
                // }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return success;
        }
    }
}