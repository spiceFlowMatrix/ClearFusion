using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    public  class VoucherDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long VoucherNo { get; set; }
        [StringLength(5)]
        public CurrencyDetails CurrencyDetail { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime VoucherDate { get; set; }
        [StringLength(10)]
        public string ChequeNo { get; set; }
        [StringLength(20)]
        public string ReferenceNo { get; set; }
        public string Description { get; set; }
        public JournalDetail JournalDetails { get; set; }
        public int? JournalCode { get; set; }
        public VoucherType VoucherTypes { get; set; }
        public int? VoucherTypeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }
        public int? OfficeId { get; set; }
        public long? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long? BudgetLineId { get; set; }
        public ProjectBudgetLineDetail ProjectBudgetLineDetail { get; set; }

        public int? FinancialYearId { get; set; }
        public FinancialYearDetail FinancialYearDetails { get; set; }

        public List<VoucherTransactions> VoucherTransactionDetails { get; set; }
        public string CurrencyCode { get; set; }
        public string VoucherType { get; set; }
        public string VoucherMode { get; set; }
        public string OfficeCode { get; set; }
        public bool IsExchangeGainLossVoucher { get; set; } = false;
        public bool IsVoucherVerified { get; set; } = false;
    }
}
