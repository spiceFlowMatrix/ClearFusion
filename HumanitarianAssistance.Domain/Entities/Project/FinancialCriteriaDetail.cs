using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class FinancialCriteriaDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long FinancialCriteriaDetailId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long ProjectId { get; set; }
        public double? Total { get; set; }
        public double? ProjectActivities { get; set; }
        public double? Operational { get; set; }
        public double? Overhead_Admin { get; set; }
        public double? Lump_Sum { get; set; }
    }
}
