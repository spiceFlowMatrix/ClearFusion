using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProjectProposalModel
    {
        public long ProjectProposaldetailId { get; set; }
        public string FolderName { get; set; }
        public string FolderId { get; set; }
        public string ProposalFileName { get; set; }
        public long ProjectId { get; set; }      
        public string ProposalFileId { get; set; }
        public string EDIFileName { get; set; }
        public string EdiFileId { get; set; }
        public string BudgetFileName { get; set; }
        public string BudgetFileId { get; set; }
        public string ConceptFileName { get; set; }
        public string ConceptFileId { get; set; }
        public string PresentationFileName { get; set; }
        public string PresentationFileId { get; set; }
        public string FIleResponseMsg { get; set; }
        public string FolderResponseMsg { get; set; }
        public int StatusCode { get; set; }
        public string ProposalWebLink { get; set; }

    }
}
