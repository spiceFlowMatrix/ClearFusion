using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditTimeCategoryCommand:BaseModel, IRequest<ApiResponse>
    {
        public long? TimeCategoryId { get; set; }
        public string TimeCategoryName { get; set; }
    }
}