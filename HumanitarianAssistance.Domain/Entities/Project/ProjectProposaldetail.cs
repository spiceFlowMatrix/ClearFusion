using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectProposalDetail : BaseEntity
    {
        public ProjectProposalDetail()
        {
            ProposalBudget = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProjectProposaldetailId { get; set; }
        public string FolderName { get; set; }
        public string FolderId { get; set; }
        public string ProposalFileName { get; set; }

        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }

        public string ProposalFileId { get; set; }
        public string EDIFileName { get; set; }
        public string EdiFileId { get; set; }
        public string BudgetFileName { get; set; }
        public string BudgetFileId { get; set; }
        public string ConceptFileName { get; set; }
        public string ConceptFileId { get; set; }
        public string PresentationFileName { get; set; }
        public string PresentationFileId { get; set; }
        public string ProposalWebLink { get; set; }
        public string EDIFileWebLink { get; set; }
        public string BudgetFileWebLink { get; set; }
        public string ConceptFileWebLink { get; set; }
        public string PresentationFileWebLink { get; set; }
        public string ProposalExtType { get; set; }
        public string EDIFileExtType { get; set; }
        public string BudgetFileExtType { get; set; }
        public string ConceptFileExtType { get; set; }
        public string PresentationExtType { get; set; }
        public DateTime? ProposalStartDate { get; set; }
        public double? ProposalBudget { get; set; }
        public DateTime? ProposalDueDate { get; set; }
        public int? ProjectAssignTo { get; set; }
        public bool? IsProposalAccept { get; set; }
        public int? CurrencyId { get; set; }
        public int? UserId { get; set; }
    }
}
