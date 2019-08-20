using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class UnitRateDetailsModel
    {
        public bool IsDeleted { get; set; }
        public long? UnitRateId { get; set; }
        public double UnitRates { get; set; }
        public long ActivityTypeId { get; set; }
        public string ActivityName { get; set; }
        public long? CurrencyId { get; set; }
        public long? MediumId { get; set; }
        public long? TimeCategoryId { get; set; }
        public long? MediaCategoryId { get; set; }
        public long? NatureId { get; set; }
        public long? QualityId { get; set; }
    }
}
