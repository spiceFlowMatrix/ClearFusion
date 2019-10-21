using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class DownloadFileFromBucketQuery : IRequest<ApiResponse>
    {
        public string ObjectName { get; set; }
        public string FileName { get; set; }
    }
}
