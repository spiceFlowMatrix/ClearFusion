using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class JobDetailsModel
    {
        public long JobId { get; set; }
        public string JobName { get; set; }
        public string Description { get; set; }
        public string JobCode { get; set; }
        public int? ContractId { get; set; }
        public long? JobPhaseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public double UnitRate { get; set; }
        public int Units { get; set; }
        public double FinalRate { get; set; }
        public double FinalPrice { get; set; }
        public double Discount { get; set; }
        public float DiscountPercent { get; set; }
        public double TotalPrice { get; set; }
        public bool IsInvoiceApproved { get; set; }
        public bool IsDeleted { get; set; }
        public long Minutes { get; set; }
    }

    public class JobFilterModel
    {
        public bool IsApproved { get; set; }
        public string YesOrNo { get; set; }
        public double UnitRate { get; set; }
        public double TotalPrice { get; set; }
        public long ContractId { get; set; }
        public string FilterType { get; set; }
        public string JobName { get; set; }
        public long JobId { get; set; }
    }

    public class JobPaginationModel
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
