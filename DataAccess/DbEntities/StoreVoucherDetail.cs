using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class StoreVoucherDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int VoucherNo { get; set; }
        [StringLength(5)]
        public string OrgCurrencyCode { get; set; }
        public byte? StoreVoucherType { get; set; }
        [StringLength(10)]
        public string RegCode { get; set; }
        [StringLength(5)]
        public string CurrencyCode { get; set; }
        [StringLength(20)]
        public string ReferenceNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string Description { get; set; }
        public Boolean? Status { get; set; }
        public Boolean? Sent { get; set; }
        public byte? VoucherType { get; set; }
    }
}
