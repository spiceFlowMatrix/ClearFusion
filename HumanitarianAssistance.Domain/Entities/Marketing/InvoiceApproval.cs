using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class InvoiceApproval : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long InvoiceApprovalId { get; set; }
        [ForeignKey("JobId")]
        public long? JobId { get; set; }
        public virtual JobDetails JobDetails { get; set; }
        public bool IsInvoiceApproved { get; set; }
    }
}
