using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.FileManagement.Queries
{
    public class GetDocumentFilesQuery : IRequest<ApiResponse>
    {
        public int PageId { get; set; }
        public long? RecordId { get; set; }
        public long? DocumentFileId { get; set; }
    }
}
