using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteTimeCategoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public int TimeCategoryId { get; set; }
    }
}