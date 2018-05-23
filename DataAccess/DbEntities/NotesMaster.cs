using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class NotesMaster : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int NoteId { get; set; }
        public int? AccountCode { get; set; }
        [ForeignKey("AccountCode")]
        public ChartAccountDetail ChartAccountDetails { get; set; }
        public string Narration { get; set; }
        public int Notes { get; set; }
        public int BlanceType { get; set; } // 1 = Sum , 2 = CR , 3 = DR
        public int FinancialReportTypeId { get; set; } // 1 = Balance Sheet , 2 = Income and Expance
        public int? AccountTypeId { get; set; }
		//[ForeignKey("AccountTypeId")]
		public AccountType AccountType { get; set; }
	}
}
