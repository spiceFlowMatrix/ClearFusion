using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities
{
    public class PensionDebitAccountMaster : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int PensionDebitAccountId { get; set; }
        public long ChartOfAccountNewId { get; set; }
        [ForeignKey("ChartOfAccountNewId")]
        public ChartOfAccountNew ChartOfAccountNew { get; set; }
    }
}
