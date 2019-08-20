using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities
{
    public class NotesMaster : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int NoteId { get; set; }
        public long ChartOfAccountNewId { get; set; }
        [ForeignKey("ChartOfAccountNewId")]
        public ChartOfAccountNew ChartOfAccountNew { get; set; }
        public string Narration { get; set; }
        public int Notes { get; set; }
        public int BlanceType { get; set; } // 1 = Sum , 2 = CR , 3 = DR
        public int FinancialReportTypeId { get; set; } // 1 = Balance Sheet , 2 = Income and Expance
        public int? AccountTypeId { get; set; }
        //[ForeignKey("AccountTypeId")]
        public AccountType AccountType { get; set; }
    }
}
