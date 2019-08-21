using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollMonthlyHourQueryHandler : IRequestHandler<GetPayrollMonthlyHourQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetPayrollMonthlyHourQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetPayrollMonthlyHourQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request != null)
                {
                    List<PayrollMonthlyHourDetailModel> payrollHours = await _dbContext.PayrollMonthlyHourDetail
                                                                            .Where(x => x.PayrollYear == request.Year && x.OfficeId == request.OfficeId && x.IsDeleted == false
                                                                                    && x.AttendanceGroupId == request.AttendanceGroupId)
                                                                            .Select(x => new PayrollMonthlyHourDetailModel
                                                                            {
                                                                                PayrollMonthlyHourID = x.PayrollMonthlyHourID,
                                                                                OfficeId = x.OfficeId,
                                                                                OfficeName = x.OfficeDetails.OfficeName,
                                                                                PayrollMonth = x.PayrollMonth,
                                                                                PayrollYear = x.PayrollYear,
                                                                                Hours = x.Hours,  //total working hours a Day
                                                                                WorkingTime = x.WorkingTime,   //total working hours for Month
                                                                                InTime = x.InTime,
                                                                                OutTime = x.OutTime,
                                                                                AttendanceGroupId= x.AttendanceGroupId
                                                                            }).ToListAsync();

                    response.data.PayrollMonthlyHourList = payrollHours;
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