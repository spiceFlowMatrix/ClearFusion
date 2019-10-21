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
    public class GetCurrentFinancialYearQueryHandler : IRequestHandler<GetCurrentFinancialYearQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetCurrentFinancialYearQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetCurrentFinancialYearQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<FinancialYearDetail> yearlist = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted == false && x.IsDefault == true).ToListAsync();



                var currentfinanciallist = yearlist.Select(x => new CurrentFinancialYearModel
                {
                    FinancialYearId = x.FinancialYearId,
                    FinancialYearName = x.FinancialYearName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList();
                response.data.CurrentFinancialYearList = currentfinanciallist;
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
