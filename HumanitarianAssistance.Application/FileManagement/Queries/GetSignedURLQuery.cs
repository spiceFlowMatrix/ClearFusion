using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.FileManagement.Queries
{
    public class GetSignedURLQuery : IRequest<ApiResponse>
    {
        public string ObjectName { get; set; }
        public string FileName { get; set; }
    }
}
