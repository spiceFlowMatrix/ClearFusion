using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEducationDegreeCommand: BaseModel, IRequest<object>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}