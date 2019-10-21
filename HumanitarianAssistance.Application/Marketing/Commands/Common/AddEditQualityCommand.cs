using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditQualityCommand:BaseModel, IRequest<ApiResponse>
    {
        public long? QualityId { get; set; }
        public string QualityName { get; set; }
    }
}