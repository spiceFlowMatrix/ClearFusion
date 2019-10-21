using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class AdvanceDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long AdvanceId { get; set; }
        [StringLength(10)]
        public string CurrencyCode { get; set; }
        [StringLength(10)]
        public string RegCode { get; set; }
        [StringLength(10)]
        public string VoucherReferenceNo { get; set; }
        public DateTime? AdvanceDate { get; set; }
        public long? EmployeeId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [StringLength(50)]
        public string ApprovedBy { get; set; }
        [StringLength(50)]
        public string ModeOfReturn { get; set; }
        public float? RequestAmount { get; set; }
        public float? AdvanceAmount { get; set; }
    }
}
