using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Domain.Entities.Marketing;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Marketing.Models;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetUnitRatePaginatedListQueryHandler : IRequestHandler<GetUnitRatePaginatedListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetUnitRatePaginatedListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetUnitRatePaginatedListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.UnitRates.AsNoTracking().AsQueryable().Where(x => x.IsDeleted == false).Skip((request.pageSize * request.pageIndex)).Take(request.pageSize).ToListAsync();
                response.data.UnitRates = list;
                response.StatusCode = 200;
                var unitRateList = await (from ur in _dbContext.UnitRates.AsNoTracking().AsQueryable()
                                          join at in _dbContext.ActivityTypes on ur.ActivityTypeId equals at.ActivityTypeId
                                          where !ur.IsDeleted && !at.IsDeleted
                                          select (new UnitRateDetailsModel
                                          {
                                              UnitRateId = ur.UnitRateId,
                                              ActivityTypeId = at.ActivityTypeId, 
                                              ActivityName = at.ActivityName,
                                              UnitRates = ur.UnitRates,
                                              CurrencyId = ur.CurrencyId,
                                              MediumId = ur.MediumId,
                                              NatureId = ur.NatureId,
                                              QualityId = ur.QualityId,
                                              TimeCategoryId = ur.TimeCategoryId,
                                              MediaCategoryId = ur.MediaCategoryId
                                          })).Skip((request.pageSize * request.pageIndex)).Take(request.pageSize).ToListAsync();
                response.data.TotalCount = unitRateList.Count(x => x.IsDeleted == false);
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
