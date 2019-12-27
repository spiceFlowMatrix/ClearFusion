using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetTenderProposalDocumentQuery : IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
    }

    public class TenderProposalDocumentModel
    {
        public int DocumentType { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentUrl { get; set; }
        public long DocumentFileId { get; set; }
    }
}
