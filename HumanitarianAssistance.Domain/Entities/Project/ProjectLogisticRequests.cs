using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectLogisticRequests : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long LogisticRequestsId { get; set; }
        public string RequestName { get; set; }
        public int Status { get; set; }
        public double TotalCost { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual ProjectDetail ProjectDetail { get; set; }
    }
}