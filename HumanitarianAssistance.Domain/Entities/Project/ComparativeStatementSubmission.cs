using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ComparativeStatementSubmission : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long SubmissionId { get; set; }
        public string Description { get; set; }
        public long[] SupplierIds { get; set; }
        public long LogisticRequestsId { get; set; }
        [ForeignKey("LogisticRequestsId")]
        public ProjectLogisticRequests ProjectLogisticRequests { get; set; }
    }
}
