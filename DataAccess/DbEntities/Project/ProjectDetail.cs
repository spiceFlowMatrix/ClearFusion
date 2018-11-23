using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
   public class ProjectDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ProjectPhaseDetailsId { get; set; }
        [ForeignKey("ProjectPhaseDetailsId")]
        public ProjectPhaseDetails ProjectPhaseDetails { get; set; }
        public bool IsProposalComplate { get; set; }
        public bool IsActive { get; set; }
    }
}
