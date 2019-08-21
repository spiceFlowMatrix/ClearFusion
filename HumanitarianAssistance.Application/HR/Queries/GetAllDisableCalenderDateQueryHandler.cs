using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllDisableCalenderDateQueryHandler: IRequestHandler<GetAllDisableCalenderDateQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllDisableCalenderDateQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllDisableCalenderDateQuery request, CancellationToken cancellationToken)
        {
           ApiResponse response = new ApiResponse();

            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);

                if (financialyear != null)
                {
                    var list = await _dbContext.EmployeeApplyLeave.Where(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId && x.ApplyLeaveStatusId != (int)ApplyLeaveStatus.Reject && x.FinancialYearId == financialyear.FinancialYearId).ToListAsync();
                    var applyleavelist = list.Select(x => new ApplyLeaveModel
                    {
                        Date = x.FromDate
                    }).ToList();
                    response.data.ApplyLeaveList = applyleavelist;
                    var holidaylist = await _dbContext.HolidayDetails.Where(x => x.OfficeId == request.OfficeId && x.FinancialYearId == financialyear.FinancialYearId).ToListAsync();
                    foreach (var list1 in holidaylist)
                    {
                        ApplyLeaveModel obj = new ApplyLeaveModel();
                        obj.Date = list1.Date;
                        response.data.ApplyLeaveList.Add(obj);
                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Financial Year Not Found";
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