using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class ProjectProposalDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
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

    }
}
