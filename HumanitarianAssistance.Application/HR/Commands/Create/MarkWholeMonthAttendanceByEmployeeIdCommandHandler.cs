using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class MarkWholeMonthAttendanceByEmployeeIdCommandHandler : IRequestHandler<MarkWholeMonthAttendanceByEmployeeIdCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public MarkWholeMonthAttendanceByEmployeeIdCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<object> Handle(MarkWholeMonthAttendanceByEmployeeIdCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {   
                EmployeeProfessionalDetail officedetails = await _dbContext.EmployeeProfessionalDetail
                                                                    .Include(x => x.OfficeDetail)
                                                                    .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false);

                int officeId;
                if (officedetails != null)
                {
                    officeId = (int)officedetails.OfficeId;
                }
                else
                {
                    throw new Exception("Office Not Found");
                }
                var financiallist = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);
                PayrollMonthlyHourDetail payrollDetail = await _dbContext.PayrollMonthlyHourDetail.FirstOrDefaultAsync(x => x.OfficeId == officeId && x.PayrollYear == financiallist.StartDate.Year && x.PayrollMonth == request.Month && x.IsDeleted == false);

                if (payrollDetail == null)
                {
                    throw new Exception(String.Format(StaticResource.PayrollDailyHoursNotSaved, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month),
                                                                                                    officedetails.OfficeDetail.OfficeName
                                                                                              ));
                }

                int monthDays = DateTime.DaysInMonth(financiallist.StartDate.Year, request.Month);
                DateTime date = new DateTime();
                int workTime = payrollDetail.OutTime.Value.Subtract(payrollDetail.InTime.Value).Hours;
                for (int i = 1; i <= monthDays; i++)
                {
                    date = new DateTime(financiallist.StartDate.Year, request.Month, i);
                    await _dbContext.EmployeeAttendance.AddAsync(new EmployeeAttendance
                    {
                        CreatedById = request.CreatedById,
                        CreatedDate = request.CreatedDate,
                        IsDeleted = false,
                        EmployeeId = request.EmployeeId,
                        InTime = payrollDetail.InTime,
                        OutTime = payrollDetail.OutTime,
                        AttendanceTypeId = (int)AttendanceType.P,
                        Date = date,
                        TotalWorkTime= workTime.ToString(),
                        FinancialYearId = financiallist.FinancialYearId
                    });
                }

                await _dbContext.SaveChangesAsync();
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