using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetMasterPagesValuesQueryHandler : IRequestHandler<GetMasterPagesValuesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetMasterPagesValuesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetMasterPagesValuesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ICollection<TimeCategory> timeCategories = await _dbContext.TimeCategories.Where(x => x.IsDeleted == false && x.TimeCategoryName != null && x.TimeCategoryName != "").ToListAsync();
                ICollection<MediaCategory> mediaCategories = await _dbContext.MediaCategories.Where(x => x.IsDeleted == false && x.CategoryName != null && x.CategoryName != "").ToListAsync();
                ICollection<Medium> Mediums = await _dbContext.Mediums.Where(x => x.IsDeleted == false && x.MediumName != null && x.MediumName != "").ToListAsync();
                ICollection<LanguageDetail> languages = await _dbContext.LanguageDetail.Where(x => x.IsDeleted == false && x.LanguageName != null && x.LanguageName != "").ToListAsync();
                ICollection<Nature> jobNature = await _dbContext.Natures.Where(x => x.IsDeleted == false && x.NatureName != null && x.NatureName != "").ToListAsync();
                ICollection<CurrencyDetails> currency = await _dbContext.CurrencyDetails.Where(x => x.IsDeleted == false && x.CurrencyName != null && x.CurrencyName != "").ToListAsync();
                ICollection<Quality> quality = await _dbContext.Qualities.Where(x => x.IsDeleted == false && x.QualityName != null && x.QualityName != "").ToListAsync();
                ICollection<ActivityType> activityType = await _dbContext.ActivityTypes.Where(x => x.IsDeleted == false).ToListAsync();
                ICollection<Producer> producer = await _dbContext.Producers.Where(x => x.IsDeleted == false).ToListAsync();
                response.data.Qualities = quality;
                response.data.Mediums = Mediums;
                response.data.Currencies = currency;
                response.data.Languages = languages;
                response.data.Natures = jobNature;
                response.data.ActivityTypes = activityType;
                response.data.MediaCategories = mediaCategories;
                response.data.TimeCategories = timeCategories;
                response.data.Producers = producer;
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
