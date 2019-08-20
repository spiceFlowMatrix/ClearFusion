using System;
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
    public class GetDisabledCalenderDatesQueryHandler: IRequestHandler<GetDisabledCalenderDatesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetDisabledCalenderDatesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetDisabledCalenderDatesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);
                var list = await _dbContext.HolidayDetails.Where(x => x.OfficeId == request.OfficeId && x.FinancialYearId == financialyear.FinancialYearId).ToListAsync();
                
                var applyleavelist = list.Select(x => new ApplyLeaveModel
                {
                    Date = x.Date
                }).ToList();
                
                response.data.ApplyLeaveList = applyleavelist;
                
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