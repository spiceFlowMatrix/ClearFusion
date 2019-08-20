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
    public class GetAllWeeklyHolidaysQueryHandler: IRequestHandler<GetAllWeeklyHolidaysQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetAllWeeklyHolidaysQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllWeeklyHolidaysQuery request, CancellationToken cancellationToken)
        {
           ApiResponse response = new ApiResponse();

            try
            {
                var financialyear = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDefault == true);

                var queryResult = await _dbContext.HolidayWeeklyDetails
                                                  .Where(x => x.IsDeleted == false && x.OfficeId == request.OfficeId 
                                                         && x.FinancialYearId == financialyear.FinancialYearId)
                                                  .ToListAsync();

                var list = queryResult.Select(x => new RepeatWeeklyDay
                {
                    Day = x.Day
                }).ToList();

                response.data.HolidayWeeklyDetailsList = list;
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