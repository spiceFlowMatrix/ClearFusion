using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditNatureCommand :BaseModel, IRequest<ApiResponse>
    {
        public long? NatureId { get; set; }
        public string NatureName { get; set; }
    }
}