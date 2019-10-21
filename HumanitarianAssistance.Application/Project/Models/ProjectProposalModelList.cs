using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectProposalModelList
    {
        public long ProjectProposaldetailId { get; set; }
        public string ProposalDocumentName { get; set; }
        public long ProjectId { get; set; }
        public string ProposalWebLink { get; set; }
        public string ProposalExtType { get; set; }
        public string UserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ProposalDocumentTypeId { get; set; }
    }
}
