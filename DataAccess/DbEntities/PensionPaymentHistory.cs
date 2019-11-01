using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PensionPaymentHistory : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int PensionPaymentId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set;}
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string VoucherReferenceNo { get; set; }
        public long VoucherNo { get; set; }
        [ForeignKey("VoucherNo")]
        public VoucherDetail VoucherDetail { get; set; }
    }
}
