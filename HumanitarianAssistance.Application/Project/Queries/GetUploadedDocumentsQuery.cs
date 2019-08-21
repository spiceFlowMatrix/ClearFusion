using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetUploadedDocumentsQuery : IRequest<ApiResponse>
    {
        public long ActivityId { get; set; }
        public long? MonitoringId { get; set; }
        public long? ProjectPhaseId { get; set; }
    }
}
