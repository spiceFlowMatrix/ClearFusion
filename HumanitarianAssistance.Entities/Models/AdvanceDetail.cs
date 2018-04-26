using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HumanitarianAssistance.Entities.Models
{
    public partial class AdvanceDetail
    {   [Key]
        public long AdvanceId { get; set; }
        public string CurrencyCode { get; set; }
        public string RegCode { get; set; }
        public string VoucherReferenceNo { get; set; }
        public DateTime? AdvanceDate { get; set; }
        public long? EmployeeId { get; set; }
        public string Description { get; set; }
        public string ApprovedBy { get; set; }
        public double? RequestAmount { get; set; }
        public double? AdvanceAmount { get; set; }
    }
}
