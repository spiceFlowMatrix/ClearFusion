using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetGoodsRecievedNoteQuery : IRequest<ApiResponse>
    {
        public long requestId { get; set; }
    }

    public class GoodsRecievedNoteModel {
        public string AttachmentName { get; set; }
        public string AttachmentUrl { get; set; }
        public string UploadedBy { get; set; }
    }
}
