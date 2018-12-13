using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class FinancialProjectDetail: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long FinancialProjectDetailId { get; set; }
        [ForeignKey("ProjectId")]
        public  ProjectDetail ProjectDetail { get; set; }
        public long ProjectId { get; set; }
        public long? ProjectSelectionId { get; set; }
        public string ProjectName { get; set; }
    }
}
