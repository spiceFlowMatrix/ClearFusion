using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteMediaCategoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public int MediaCategoryId { get; set; }
    }
}