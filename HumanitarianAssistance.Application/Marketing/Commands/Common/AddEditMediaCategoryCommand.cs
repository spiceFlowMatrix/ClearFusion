using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditMediaCategoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? MediaCategoryId { get; set; }
        public string CategoryName { get; set; }   
    }
}