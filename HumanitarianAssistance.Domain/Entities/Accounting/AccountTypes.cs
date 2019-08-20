using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    // This class/model is for storing notes.
    public class AccountType : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int AccountTypeId { get; set; }
        [StringLength(100)]
        public string AccountTypeName { get; set; }

        // represents whether the it's a balance sheet or income/expense note
        public int? AccountCategory { get; set; }

        // this refers to the note category in financial report
        // e.g assets, liabilities, income, expense
        public int AccountHeadTypeId { get; set; }
        [ForeignKey("AccountHeadTypeId")]
        public AccountHeadType AccountHeadType { get; set; }
    }
}
