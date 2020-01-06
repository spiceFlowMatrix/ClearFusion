using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetLogisticGoodsNoteReportPdfQuery : IRequest<byte[]>
    {
        public long LogisticRequestId { get; set; }
    }
}