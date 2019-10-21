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
    public class GetUnitRateByIdQueryHandler : IRequestHandler<GetUnitRateByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetUnitRateByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetUnitRateByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var unitRateDetails = await (from ur in _dbContext.UnitRates.AsNoTracking().AsQueryable()
                                             join at in _dbContext.ActivityTypes on ur.ActivityTypeId equals at.ActivityTypeId
                                             where !ur.IsDeleted && !at.IsDeleted && ur.UnitRateId == Convert.ToInt32(request.UnitRateId)
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
                                             })).FirstOrDefaultAsync();

                response.data.rateDetailsById = unitRateDetails;
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
