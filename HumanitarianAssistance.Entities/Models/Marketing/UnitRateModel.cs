using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class UnitRateModel
    {
        public long? UnitRateId { get; set; }
        public double UnitRate { get; set; }
        public long? CurrencyId { get; set; }
        public long? MediumId { get; set; }
        public long? TimeCategoryId { get; set; }
        public long? NatureId { get; set; }
        public long? QualityId { get; set; }
        public long? ActivityTypeId { get; set; }
        public string ActivityName { get; set; }
    }

    public class UnitRateDetailsModel
    {
        public long? UnitRateId { get; set; }
        public double UnitRates { get; set; }
        public long ActivityTypeId { get; set; }
        public string ActivityName { get; set; }
        public long? CurrencyId { get; set; }
        public long? MediumId { get; set; }
        public long? TimeCategoryId { get; set; }
        public long? NatureId { get; set; }
        public long? QualityId { get; set; }
    }
}
