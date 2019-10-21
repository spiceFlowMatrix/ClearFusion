using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteQualityCommand:BaseModel, IRequest<ApiResponse>
    {
        public int QualityId { get; set; }
    }
}