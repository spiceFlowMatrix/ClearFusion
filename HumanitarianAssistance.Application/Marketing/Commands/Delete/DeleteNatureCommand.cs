using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteNatureCommand :BaseModel, IRequest<ApiResponse>
    {
        public int NatureId { get; set; }
    }
}