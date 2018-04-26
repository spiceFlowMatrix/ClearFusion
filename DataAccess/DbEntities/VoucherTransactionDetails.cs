using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class VoucherTransactionDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int TransactionId { get; set; }
        public long VoucherNo { get; set; }
        [ForeignKey("VoucherNo")]
        public VoucherDetail VoucherDetails { get; set; }
        
        public int? CreditAccount { get; set; }
        public int? DebitAccount { get; set; }        
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        [ForeignKey("CreditAccount")]
        public ChartAccountDetail CreditAccountDetails { get; set; }
        [ForeignKey("DebitAccount")]
        public ChartAccountDetail DebitAccountDetails { get; set; }

        public int? CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }

        public int?OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }

        public int? FinancialYearId { get; set; }
        public FinancialYearDetail FinancialYearDetails { get; set; }
    }
}
