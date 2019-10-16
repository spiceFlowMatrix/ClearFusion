using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    public class VoucherTransactions : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long TransactionId { get; set; }

        public long? VoucherNo { get; set; }
        [ForeignKey("VoucherNo")]
        public VoucherDetail VoucherDetails { get; set; }

        public long? CreditAccount { get; set; }                                     // NOT IN USE NOW
        public long? DebitAccount { get; set; }                                      // NOT IN USE NOW
        public double? Amount { get; set; }                                         // NOT IN USE NOW

        public string Description { get; set; }
        public DateTime? TransactionDate { get; set; }


        public long? ChartOfAccountNewId { get; set; }
        [ForeignKey("ChartOfAccountNewId")]
        public ChartOfAccountNew ChartOfAccountDetail { get; set; }


        //[ForeignKey("DebitAccount")]
        //public ChartAccountDetail DebitAccountDetails { get; set; }

        public int? CurrencyId { get; set; }
        public CurrencyDetails CurrencyDetails { get; set; }

        public int? OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }

        public int? FinancialYearId { get; set; }
        //public FinancialYearDetail FinancialYearDetails { get; set; }
        public string Donor { get; set; }
        public string Area { get; set; }
        public string Sector { get; set; }
        public string Program { get; set; }
        public string Project { get; set; }
        public string Job { get; set; }
        public string CostBook { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }

        public double? AFGAmount { get; set; }
        public double? EURAmount { get; set; }
        public double? USDAmount { get; set; }
        public double? PKRAmount { get; set; }
        public long? ProjectId { get; set; }
        public virtual ProjectDetail ProjectDetail { get; set; }

        public long? BudgetLineId { get; set; }
        public virtual ProjectBudgetLineDetail ProjectBudgetLineDetail { get; set; }

        public long? JobId { get; set; }
        [ForeignKey("JobId")]
        public ProjectJobDetail ProjectJobDetail { get; set; }
    }
}
