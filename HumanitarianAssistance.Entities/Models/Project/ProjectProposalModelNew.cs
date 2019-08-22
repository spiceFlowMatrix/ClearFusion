using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ProjectProposalModelNew
    {
        public long ProjectProposaldetailId { get; set; }
        public string FolderName { get; set; }
        public string ProposalFileName { get; set; }
        public long ProjectId { get; set; }
        public string ProposalWebLink { get; set; }
        public string FileType { get; set; }
        public string ProposalExtType { get; set; }
        public DateTime? ProposalStartDate { get; set; }
        public double? ProposalBudget { get; set; }
        public DateTime? ProposalDueDate { get; set; }
        public int? ProjectAssignTo { get; set; }
        public bool? IsProposalAccept { get; set; }
        public int? CurrencyId { get; set; }
        public int? UserId { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }  
    }
}
