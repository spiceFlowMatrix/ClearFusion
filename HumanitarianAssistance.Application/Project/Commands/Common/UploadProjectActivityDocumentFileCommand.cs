using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class UploadProjectActivityDocumentFileCommand : BaseModel, IRequest<ApiResponse>
    {
        public IFormFile File { get; set; }
        public long ActivityID { get; set; }
        public string FileName { get; set; }
        public string LogginUserEmailId { get; set; }
        public string Ext { get; set; }
        public int StatusID { get; set; }
        public long MonitoringID { get; set; }
    }
}
