using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities
{
    public class PensionPaymentHistory : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int PensionPaymentId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string VoucherReferenceNo { get; set; }
        public long VoucherNo { get; set; }
        [ForeignKey("VoucherNo")]
        public VoucherDetail VoucherDetail { get; set; }
    }
}
