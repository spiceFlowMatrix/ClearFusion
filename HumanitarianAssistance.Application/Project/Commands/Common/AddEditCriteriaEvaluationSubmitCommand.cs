using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditCriteriaEvaluationSubmitCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime? StartData { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ProjectPhaseDetailsId { get; set; }
        public bool? IsProposalComplate { get; set; }
        public string ProjectPhase { get; set; }
        public string TotalDaysinHours { get; set; }
        public bool? IsWin { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsProposalSubmit { get; set; }
        public bool? IsCriteriaEvaluationSubmit { get; set; }
        public int? ReviewerId { get; set; }
        public int? DirectorId { get; set; }
        public bool? IsDelete { get; set; }
    }
}
