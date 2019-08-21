using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class UploadFileDemoCommand : BaseModel, IRequest<ApiResponse>
    {
        public string activityId { get; set; }
        public string statusId { get; set; }
        public IFormFile fileData { get; set; }
    }
}
