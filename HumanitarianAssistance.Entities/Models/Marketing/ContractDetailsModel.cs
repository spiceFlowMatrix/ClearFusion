﻿using System;
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
        public long? QualityId { get; set; }
        public bool IsCompleted { get; set; }
        public long? ClientId { get; set; }
        public long? UnitRateId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeclined { get; set; }
        public string Type { get; set; }
        public int? Count { get; set; }
    }

    public class ApproveContractModel
    {
        public long? ContractId { get; set; }
        public string Type { get; set; }
    }

    public class FilterContractModel
    {
        public long? ContractId { get; set; }
        public string FilterType { get; set; }
        public string ClientName { get; set; }
        public long? ClientId { get; set; }
        public long? ActivityTypeId { get; set; }
        public long? CurrencyId { get; set; }
        public int? UnitRate { get; set; }
        public bool IsApproved { get; set; }
        public string YesOrNo { get; set; }
    }

    public class ContractByClient
    {
        public long? ContractId { get; set; }
        public long? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ContractByClients { get; set; }
        public double UnitRate { get; set; }
        public int? CurrencyId { get; set; }
    }

    public class ContractPaginationModel
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
