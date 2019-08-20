using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteMediumCommand : BaseModel,IRequest<ApiResponse>
    {
        public int MediumId { get; set; }
    }
}