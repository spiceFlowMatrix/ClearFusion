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

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetUnitRateByActivityTypeIdQueryHandler : IRequestHandler<GetUnitRateByActivityTypeIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetUnitRateByActivityTypeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetUnitRateByActivityTypeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var activity = await _dbContext.ActivityTypes.AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.IsDeleted == false && x.ActivityTypeId == request.ActivityTypeId);
                UnitRate unitRateById = new UnitRate();
                if (activity.ActivityName == "Broadcasting")
                {
                    unitRateById = await _dbContext.UnitRates.AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.ActivityTypeId == request.ActivityTypeId && x.TimeCategoryId == request.TimeCategoryId && x.MediumId == request.MediumId && x.CurrencyId == request.CurrencyId && x.IsDeleted == false);
                }
                if (activity.ActivityName == "Production")
                {
                    unitRateById = await _dbContext.UnitRates.AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.ActivityTypeId == request.ActivityTypeId && x.MediumId == request.MediumId && x.NatureId == request.NatureId && x.QualityId == request.QualityId && x.CurrencyId == request.CurrencyId && x.IsDeleted == false);
                }
                if (unitRateById == null)
                {
                    response.StatusCode = StaticResource.notFoundCode;
                    response.data.UnitRateByActivityId = unitRateById;
                    response.Message = StaticResource.unitRateNotFound;
                }
                else
                {
                    response.StatusCode = StaticResource.successStatusCode;
                    response.data.UnitRateByActivityId = unitRateById;
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
