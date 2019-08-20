using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
   public class DeleteQualificationDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int QualificationId { get; set; }
    }
}
