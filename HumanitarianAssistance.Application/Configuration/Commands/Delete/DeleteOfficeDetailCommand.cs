using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteOfficeDetailCommand: BaseModel, IRequest<ApiResponse>
    {
         public int OfficeId { get; set; }
        
    }
}