using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditPayrollMonthlyHourCommandHandler : IRequestHandler<EditPayrollMonthlyHourCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditPayrollMonthlyHourCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditPayrollMonthlyHourCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var payrollmonthlyinfo = await _dbContext.PayrollMonthlyHourDetail
                                                         .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                     x.PayrollMonthlyHourID == request.PayrollMonthlyHourID
                                                                    );
                if (payrollmonthlyinfo != null)
                {
                    TimeSpan hours;

                    hours = Convert.ToDateTime(request.OutTime) - Convert.ToDateTime(request.InTime);

                    payrollmonthlyinfo.OfficeId = request.OfficeId;
                    payrollmonthlyinfo.PayrollMonth = request.PayrollMonth;
                    payrollmonthlyinfo.PayrollYear = request.PayrollYear;
                    payrollmonthlyinfo.Hours = Convert.ToInt32(hours.ToString().Substring(0, 2));
                    payrollmonthlyinfo.WorkingTime = request.WorkingTime;
                    payrollmonthlyinfo.InTime = request.InTime;
                    payrollmonthlyinfo.OutTime = request.OutTime;
                    payrollmonthlyinfo.ModifiedById = request.ModifiedById;
                    payrollmonthlyinfo.ModifiedDate = request.ModifiedDate;
                    payrollmonthlyinfo.IsDeleted = false;
                    payrollmonthlyinfo.AttendanceGroupId = request.AttendanceGroupId;

                    _dbContext.PayrollMonthlyHourDetail.Update(payrollmonthlyinfo);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    throw new Exception("Record not found to update");
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