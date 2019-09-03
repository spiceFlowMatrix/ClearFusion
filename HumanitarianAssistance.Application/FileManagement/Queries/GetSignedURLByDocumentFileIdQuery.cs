using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.FileManagement.Queries
{
    public class GetSignedURLByDocumentFileIdQuery: IRequest<ApiResponse>
    {
         public long DocumentFileId { get; set; }
    }
}