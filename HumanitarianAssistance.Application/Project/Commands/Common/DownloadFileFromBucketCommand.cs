using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class DownloadFileFromBucketCommand : BaseModel, IRequest<ApiResponse>
    {
        public string ObjectName { get; set; }
        public string FileName { get; set; }
    }
}
