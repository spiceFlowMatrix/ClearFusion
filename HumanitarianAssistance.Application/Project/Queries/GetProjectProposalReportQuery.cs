using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectProposalReportQuery : IRequest<ApiResponse>
    {
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int StartDateFilterOption { get; set; }
        public int DueDateFilterOption { get; set; }
        public int CurrencyId { get; set; }
        public double Amount { get; set; }
        public int AmountFilterOption { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsLate { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}
