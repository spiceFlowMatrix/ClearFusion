using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    public class VoucherTransactionDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int TransactionId { get; set; }
        public long? VoucherNo { get; set; }
        [ForeignKey("VoucherNo")]
        public VoucherDetail VoucherDetails { get; set; }

        public int? CreditAccount { get; set; }										// NOT IN USE NOW
        public int? DebitAccount { get; set; }                                      // NOT IN USE NOW
        public double? Amount { get; set; }                                         // NOT IN USE NOW

        public string Description { get; set; }
        public DateTime? TransactionDate { get; set; }
        [ForeignKey("ChartOfAccountNewId")]
        public ChartOfAccountNew CreditAccountDetails { get; set; }
        //[ForeignKey("DebitAccount")]
        //public ChartAccountDetail DebitAccountDetails { get; set; }

        public int? CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }

        public int? OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }

        public int? FinancialYearId { get; set; }
        //public FinancialYearDetail FinancialYearDetails { get; set; }
        public long? ChartOfAccountNewId { get; set; }
        public string Donor { get; set; }
        public string Area { get; set; }
        public string Sector { get; set; }
        public string Program { get; set; }
        public string Project { get; set; }
        public string Job { get; set; }
        public string CostBook { get; set; }
        public float? Debit { get; set; }
        public float? Credit { get; set; }

        public double? AFGAmount { get; set; }
        public double? EURAmount { get; set; }
        public double? USDAmount { get; set; }
        public double? PKRAmount { get; set; }
    }
}
