using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.FileManagement.Commands.Create
{
    public class SaveUploadedFileInfoCommand : BaseModel, IRequest<ApiResponse>
    {
        public string FileType { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public int PageId { get; set; }
        public long RecordId { get; set; }
        public string FilePath { get; set; }
    }
}
