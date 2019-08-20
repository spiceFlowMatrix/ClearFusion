using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
   public class DeleteAgeGroupDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long AgeGroupOtherDetailId { get; set; }
    }
}
