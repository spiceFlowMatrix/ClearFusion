using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    public class ChartOfAccountNew : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ChartOfAccountNewId { get; set; }
        public string ChartOfAccountNewCode { get; set; }

        // App logic must always set this during add/edit actions
        public bool? IsCreditBalancetype { get; set; }

        [StringLength(100)]
        public string AccountName { get; set; }
        public long ParentID { get; set; }

        public int AccountLevelId { get; set; }
        [ForeignKey("AccountLevelId")]
        public AccountLevel AccountLevels { get; set; }

        // This field is for storing the financial report type - income, assets,
        // liabilities etc that the account is connected to.
        public int AccountHeadTypeId { get; set; }

        // This field is for storing the financial report "note" 
        // that this account is connected to.
        public int? AccountTypeId { get; set; }
        [ForeignKey("AccountTypeId")]
        public AccountType AccountType { get; set; }

        // Field for storing the type of transactions that must be attributed
        // towards this account. Such as inventory account, salary account.
        public int? AccountFilterTypeId { get; set; }
        [ForeignKey("AccountFilterTypeId")]
        public AccountFilterType AccountFilterType { get; set; }

        public virtual List<VoucherTransactions> VoucherTransactionsList { get; set; }
    }
}
