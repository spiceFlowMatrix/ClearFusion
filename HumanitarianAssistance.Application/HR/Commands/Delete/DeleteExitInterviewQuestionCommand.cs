using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteExitInterviewQuestionCommand: BaseModel, IRequest<object>
    {
        public int Id { get; set; }
    }
}