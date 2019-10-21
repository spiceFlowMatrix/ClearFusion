using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
        public class AddEditUnitRateCommandHandler : IRequestHandler<AddEditUnitRateCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddEditUnitRateCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddEditUnitRateCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            UnitRate unitRateDetails = new UnitRate();
            ActivityType activityDetail = await _dbContext.ActivityTypes.FirstOrDefaultAsync(x => x.ActivityTypeId == request.ActivityTypeId);
            string activity = activityDetail.ActivityName;
            if (activity == "Broadcasting")
            {
                unitRateDetails = await _dbContext.UnitRates.FirstOrDefaultAsync(x => x.ActivityTypeId == request.ActivityTypeId
                && x.CurrencyId == request.CurrencyId && x.MediumId == request.MediumId && x.TimeCategoryId
                == request.TimeCategoryId && x.MediaCategoryId == request.MediaCategoryId && x.QualityId == request.QualityId && x.IsDeleted == false);
            }
            if (activity == "Production")
            {
                unitRateDetails = await _dbContext.UnitRates.FirstOrDefaultAsync(x => x.ActivityTypeId == request.ActivityTypeId
                && x.CurrencyId == request.CurrencyId && x.QualityId == request.QualityId && x.MediumId
                == request.MediumId && x.NatureId == request.NatureId && x.MediaCategoryId == request.MediaCategoryId && x.IsDeleted == false);
            }
            try
            {
                if (request.UnitRateId == 0 || request.UnitRateId == null)
                {
                    if (unitRateDetails == null)
                    {
                        UnitRate obj = new UnitRate();                        
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = request.CreatedDate;
                        obj.IsDeleted = false;
                        obj.ActivityTypeId = request.ActivityTypeId;
                        obj.CurrencyId = request.CurrencyId;
                        obj.UnitRates = request.UnitRates;
                        obj.MediumId = request.MediumId;
                        obj.NatureId = request.NatureId;
                        obj.QualityId = request.QualityId;
                        obj.TimeCategoryId = request.TimeCategoryId;
                        obj.MediaCategoryId = request.MediaCategoryId;
                        _mapper.Map(request,obj);
                        await _dbContext.UnitRates.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                        request.ActivityName = activity;
                        request.UnitRateId = obj.UnitRateId;
                        response.data.unitRateDetails = request;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Unit Rate Added Successfully";
                    }
                    else
                    {
                        var obj = await _dbContext.UnitRates.FirstOrDefaultAsync(x => x.UnitRateId == request.UnitRateId && x.IsDeleted == false);
                        if (obj == null)
                        {
                            response.Message = StaticResource.unitRateExists;
                        }
                    }
                }
                else
                {
                    var obj1 = await _dbContext.UnitRates.FirstOrDefaultAsync(x => x.UnitRateId == request.UnitRateId && x.IsDeleted == false);
                    if (unitRateDetails != null)
                    {
                        if (obj1 != null)
                        {
                            if (obj1.UnitRates == request.UnitRates)
                            {
                                response.Message = StaticResource.unitRateExists;
                            }
                            else
                            {
                                obj1.UnitRates = request.UnitRates;
                                await _dbContext.SaveChangesAsync();
                                response.data.unitRateDetailsById = obj1;
                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Unit Rate updated Successfully";
                            }

                        }
                    }
                    else
                    {
                        obj1.ModifiedById = request.ModifiedById;
                        obj1.ModifiedDate = request.ModifiedDate;
                        obj1.ActivityTypeId = request.ActivityTypeId;
                        obj1.CurrencyId = request.CurrencyId;
                        obj1.MediumId = request.MediumId;
                        obj1.NatureId = request.NatureId;
                        obj1.QualityId = request.QualityId;
                        obj1.TimeCategoryId = request.TimeCategoryId;
                        obj1.MediaCategoryId = request.MediaCategoryId;
                        obj1.UnitRates = request.UnitRates;
                        await _dbContext.SaveChangesAsync();
                        response.data.unitRateDetailsById = obj1;
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Unit Rate updated Successfully";
                    }
                    var activityDetails = await _dbContext.ActivityTypes.FirstOrDefaultAsync(x => x.ActivityTypeId == request.ActivityTypeId);
                    request.ActivityName = activityDetails.ActivityName;
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