using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAppliedLeaveHoursQueryHandler: IRequestHandler<GetEmployeeAppliedLeaveHoursQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAppliedLeaveHoursQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeAppliedLeaveHoursQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                EmployeeDetail employee = await _dbContext.EmployeeDetail
                                                          .Include(x=> x.EmployeeProfessionalDetail)
                                                          .ThenInclude(x=> x.AttendanceGroupMaster)
                                                          .Include(x=> x.EmployeeProfessionalDetail)
                                                          .ThenInclude(x=> x.OfficeDetail)
                                                          .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.EmployeeID == request.EmployeeId);

                PayrollMonthlyHourDetail officeHours = await _dbContext.PayrollMonthlyHourDetail
                                                                 .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                                 x.PayrollYear == request.StartDate.Year && x.PayrollMonth == request.StartDate.Month &&
                                                                 x.AttendanceGroupId == employee.EmployeeProfessionalDetail.AttendanceGroupId
                                                                 && x.OfficeId == employee.EmployeeProfessionalDetail.OfficeId);

                if(officeHours == null)
                {
                    throw new Exception(string.Format(StaticResource.PayrollDailyHoursNotSetForAttendanceGroup,
                    employee.EmployeeProfessionalDetail.OfficeDetail.OfficeName, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.StartDate.Month), request.StartDate.Year,
                    employee.EmployeeProfessionalDetail.AttendanceGroupMaster.Name
                    ));
                }

                int days = request.EndDate.Subtract(request.StartDate).Days + 1;
                int hours = officeHours.OutTime.Value.Subtract(officeHours.InTime.Value).Hours;
                int appliedLeaveHour = days * hours;

                response.Add("AppliedHours", appliedLeaveHour);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}