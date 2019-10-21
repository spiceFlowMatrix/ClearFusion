using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditMediumCommand : BaseModel,IRequest<ApiResponse>
    {
        public long? MediumId { get; set; }
        public string MediumName { get; set; }
    }
}