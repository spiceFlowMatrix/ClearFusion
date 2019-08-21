using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetBudgetLineTypesQueryHandler : IRequestHandler<GetBudgetLineTypesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetBudgetLineTypesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetBudgetLineTypesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<BudgetLineType> list = await _dbContext.BudgetLineType.Where(x => x.IsDeleted == false).ToListAsync();

                var budgetlinetypelist = list.Select(x => new BudgetLineTypeModel
                {
                    BudgetLineTypeId = x.BudgetLineTypeId,
                    BudgetLineTypeName = x.BudgetLineTypeName
                }).OrderBy(x => x.BudgetLineTypeName).ToList();
                response.data.BudgetLineTypeList = budgetlinetypelist;
                response.Message = "Success";
                response.StatusCode = StaticResource.successStatusCode;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.StatusCode = StaticResource.failStatusCode;
            }
            return response;
        }
    }
}
