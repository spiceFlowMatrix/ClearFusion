using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllFinancialYearDetailQueryHandler : IRequestHandler<GetAllFinancialYearDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllFinancialYearDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllFinancialYearDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<FinancialYearDetail> yearlist = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted == false).OrderBy(x => x.FinancialYearId).ToListAsync();


                var financialyearlist = yearlist.Select(x => new FinancialYearDetailModel
                {
                    FinancialYearId = x.FinancialYearId,
                    FinancialYearName = x.FinancialYearName,
                    StartDate = x.StartDate.Date.ToLocalTime(),
                    EndDate = x.EndDate.Date.ToLocalTime(),
                    Description = x.Description,
                    IsDefault = x.IsDefault
                }).ToList();
                response.data.FinancialYearDetailList = financialyearlist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

    }
}
