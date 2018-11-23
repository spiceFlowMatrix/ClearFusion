using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class ContractDetailsModel
    {
        public long ContractId { get; set; }
        public string ContractCode { get; set; }
        public string ClientName { get; set; }
        public long? ActivityTypeId { get; set; }
        public double UnitRate { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long? LanguageId { get; set; }
        public long? MediumId { get; set; }
        public long? NatureId { get; set; }
        public long? TimeCategoryId { get; set; }
        public long? MediaCategoryId { get; set; }
        public long QualityId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
