using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEducationDegreeCommand: BaseModel, IRequest<object>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}