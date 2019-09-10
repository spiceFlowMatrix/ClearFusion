using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Logger.Commands.Create
{
    public class LogErrorCommand : BaseModel, IRequest<ApiResponse>
    {
        public string Path { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
    }
}