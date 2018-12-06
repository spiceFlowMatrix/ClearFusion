using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class ProposalDocModel
    {
        public long? ProjectId { get; set; }
        public DateTime? ProposalStartDate { get; set; }
        public string ProposalBudget { get; set; }
        public DateTime? ProposalDueDate { get; set; }
        public int? ProjectAssignTo { get; set; }
        public bool? IsProposalAccept { get; set; }
        public int? CurrencyId { get; set; }
        public int? UserId { get; set; }
    }
}
