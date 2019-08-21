using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddPayrollMonthlyHourCommandHandler : IRequestHandler<AddPayrollMonthlyHourCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddPayrollMonthlyHourCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddPayrollMonthlyHourCommand request, CancellationToken cancellationToken)
        {
           ApiResponse response = new ApiResponse();

            try
            {
                TimeSpan hours;
                hours = Convert.ToDateTime(request.OutTime) - Convert.ToDateTime(request.InTime);
                request.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));

                if (request.SaveForAllOffice)
                {
                    List<PayrollMonthlyHourDetail> payrollMonthlyHourDetailsAdd = new List<PayrollMonthlyHourDetail>();
                    List<PayrollMonthlyHourDetail> payrollMonthlyHourDetailsUpdate = new List<PayrollMonthlyHourDetail>();

                    List<int> officeIds = _dbContext.OfficeDetail.Where(x => x.IsDeleted == false).Select(x => x.OfficeId).ToList();

                    foreach (int officeId in officeIds)
                    {
                        var payrollinfo = await _dbContext.PayrollMonthlyHourDetail
                                                    .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                    x.OfficeId == officeId &&
                                                    x.PayrollMonth == request.PayrollMonth &&
                                                    x.PayrollYear == request.PayrollYear &&
                                                    x.AttendanceGroupId== request.AttendanceGroupId);

                        if (payrollinfo == null)
                        {
                            PayrollMonthlyHourDetail obj = new PayrollMonthlyHourDetail();
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = request.CreatedDate;
                            obj.Hours = request.Hours;
                            obj.WorkingTime = request.WorkingTime; //total working hours
                            obj.InTime = request.InTime;
                            obj.OutTime = request.OutTime;
                            obj.PayrollMonth = request.PayrollMonth;
                            obj.PayrollYear = request.PayrollYear;
                            obj.IsDeleted = false;
                            obj.OfficeId = officeId;
                            obj.AttendanceGroupId = request.AttendanceGroupId;
                            payrollMonthlyHourDetailsAdd.Add(obj);
                        }
                        else
                        {
                            payrollinfo.ModifiedDate = DateTime.UtcNow;
                            payrollinfo.Hours = request.Hours;
                            payrollinfo.WorkingTime= request.WorkingTime;
                            payrollinfo.InTime = request.InTime;
                            payrollinfo.OutTime = request.OutTime;
                            payrollinfo.PayrollMonth = request.PayrollMonth;
                            payrollinfo.PayrollYear = request.PayrollYear;
                            payrollinfo.OfficeId = officeId;
                            payrollinfo.AttendanceGroupId = request.AttendanceGroupId;
                            payrollMonthlyHourDetailsUpdate.Add(payrollinfo);
                        }
                    }

                    if (payrollMonthlyHourDetailsAdd.Any())
                    {
                        await _dbContext.AddRangeAsync(payrollMonthlyHourDetailsAdd);
                        await _dbContext.SaveChangesAsync();
                    }

                    if (payrollMonthlyHourDetailsUpdate.Any())
                    {
                        _dbContext.UpdateRange(payrollMonthlyHourDetailsUpdate);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                else
                {
                    var payrollinfo = await _dbContext.PayrollMonthlyHourDetail
                                                      .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                        x.OfficeId == request.OfficeId &&
                                                        x.PayrollMonth == request.PayrollMonth &&
                                                        x.PayrollYear == request.PayrollYear &&
                                                        x.AttendanceGroupId == request.AttendanceGroupId
                                                        );

                    if (payrollinfo == null)
                    {
                        PayrollMonthlyHourDetail obj = new PayrollMonthlyHourDetail();
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.Hours = request.Hours;
                        obj.WorkingTime = request.WorkingTime; //total working hours
                        obj.InTime = request.InTime;
                        obj.OutTime = request.OutTime;
                        obj.PayrollMonth = request.PayrollMonth;
                        obj.PayrollYear = request.PayrollYear;
                        obj.IsDeleted = false;
                        obj.OfficeId = request.OfficeId;
                        obj.AttendanceGroupId = request.AttendanceGroupId;
                        await _dbContext.PayrollMonthlyHourDetail.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = StaticResource.HoursAlreadySet;
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