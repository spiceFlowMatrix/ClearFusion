using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCriteriaEvaluationDetailReportPdfQuery: IRequest<byte[]>
    {
        public long ProjectId { get; set; }
        public double? TotalScore { get; set; }
    }
}