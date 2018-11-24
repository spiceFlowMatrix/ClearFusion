using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.AccountingNew
{
    public class ChartOfAccountNew : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ChartOfAccountNewId { get; set; }
        public string ChartOfAccountNewCode { get; set; }
        [StringLength(100)]
        public string AccountName { get; set; }
        public long ParentID { get; set; }

        public int AccountLevelId { get; set; }
        [ForeignKey("AccountLevelId")]
        public AccountLevel AccountLevels { get; set; }

        public int? AccountTypeId { get; set; }
        [ForeignKey("AccountTypeId")]
        public AccountType AccountType { get; set; }

        public int AccountHeadTypeId { get; set; }

        public int? AccountFilterTypeId { get; set; }
        [ForeignKey("AccountFilterTypeId")]
        public AccountFilterType AccountFilterType { get; set; }

        //public List<VoucherTransactions> CreditAccountlist { get; set; }
        //public List<VoucherTransactions> DebitAccountlist { get; set; }
        //public List<VoucherDetail> VoucherList { get; set; }
    }
}
