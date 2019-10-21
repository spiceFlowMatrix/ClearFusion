using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
   public class EditAgeGroupDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? AgeGroupOtherDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public int MyProperty { get; set; }
    }
}
