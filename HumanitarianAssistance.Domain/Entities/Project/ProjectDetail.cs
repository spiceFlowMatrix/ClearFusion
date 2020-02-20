using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ProjectPhaseDetailsId { get; set; }
        [ForeignKey("ProjectPhaseDetailsId")]
        public ProjectPhaseDetails ProjectPhaseDetails { get; set; }
        public bool? IsProposalComplate { get; set; }
        public bool IsActive { get; set; }
        public bool? IsCriteriaEvaluationSubmit { get; set; }

        public int? ReviewerId { get; set; }
        public int? DirectorId { get; set; }
        public List<ProjectSector> ProjectSector { get; set; }
        public List<ProjectProgram> ProjectProgram { get; set; }
        public List<ProjectBudgetLineDetail> ProjectBudgetLineDetails { get; set; }
        public ProjectProposalDetail ProjectProposalDetail { get; set; }
        public List<ProjectJobDetail> ProjectJobDetail { get; set; }
        public ProjectOtherDetail ProjectOtherDetail { get; set; }
        public List<CountryMultiSelectDetails> CountryMultiSelectDetails { get; set; }
        public List<ProvinceMultiSelect> ProvinceMultiSelect { get; set; }
        public List<DistrictMultiSelect> DistrictMultiSelect { get; set; }
        public List<ProjectActivityDetail> ProjectActivityDetail { get; set; }

    }
}
