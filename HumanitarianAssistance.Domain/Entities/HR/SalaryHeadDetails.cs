using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class SalaryHeadDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int SalaryHeadId { get; set; }
        public int HeadTypeId { get; set; }
        [StringLength(50)]
        public string HeadName { get; set; }
        public string Description { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        [ForeignKey("AccountNo")]
        public ChartOfAccountNew ChartOfAccountNew { get; set; }
    }
}
