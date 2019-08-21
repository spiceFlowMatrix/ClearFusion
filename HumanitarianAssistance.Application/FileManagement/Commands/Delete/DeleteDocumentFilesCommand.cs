using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.FileManagement.Commands.Delete
{
    public class DeleteDocumentFilesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int PageId { get; set; }
        public long? RecordId { get; set; }
        public long? DocumentFileId { get; set; }
    }
}
