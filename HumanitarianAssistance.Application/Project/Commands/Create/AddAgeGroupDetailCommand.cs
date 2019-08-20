using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
  public  class AddAgeGroupDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long? AgeGroupOtherDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
        public int MyProperty { get; set; }
    }
}
