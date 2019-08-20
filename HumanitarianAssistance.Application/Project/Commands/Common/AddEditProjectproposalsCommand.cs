using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectproposalsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? ProjectId { get; set; }
        public DateTime? ProposalStartDate { get; set; }
        public double? ProposalBudget { get; set; }
        public DateTime? ProposalDueDate { get; set; }
        public int? ProjectAssignTo { get; set; }
        public bool? IsProposalAccept { get; set; }
        public int? CurrencyId { get; set; }
        public int? UserId { get; set; }
        public string logginUserEmailId { get; set; }
        public string Id { get; set; }
    }
}
