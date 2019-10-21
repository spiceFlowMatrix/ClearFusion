using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PayrollValidateDetail : BaseEntity
    {
        public DateTime? PaymentDate { get; set; }
        public int? PayrollMonth { get; set; }
        public int? PayrollYear { get; set; }
        [StringLength(10)]
        public string RegCode { get; set; }
        [StringLength(10)]
        public string CurrencyCode { get; set; }
        public int? Status { get; set; }
        public DateTime? ValidateDate { get; set; }
    }
}
