using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
   public class FinancialCriteriaDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long FinancialCriteriaDetailId { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public double? Total { get; set; }
        public double? ProjectActivities { get; set; }
        public double? Operational { get; set; }
        public double? Overhead_Admin { get; set; }
        public double? Lump_Sum { get; set; }
    }
}
