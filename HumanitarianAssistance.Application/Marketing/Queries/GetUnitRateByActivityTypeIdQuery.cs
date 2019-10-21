using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetUnitRateByActivityTypeIdQuery : IRequest<ApiResponse>
    {
        public long? UnitRateId { get; set; }
        public double UnitRates { get; set; }
        public long? CurrencyId { get; set; }
        public long? MediumId { get; set; }
        public long? TimeCategoryId { get; set; }
        public long? NatureId { get; set; }
        public long? QualityId { get; set; }
        public long? ActivityTypeId { get; set; }
        public long? MediaCategoryId { get; set; }
        public string ActivityName { get; set; }
    }
}
