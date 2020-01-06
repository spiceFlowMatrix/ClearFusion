using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Common
{
    public class UpsertExitInterviewQuestionCommand: BaseModel, IRequest<object>
    {
        public long? Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestionType { get; set; }
        public int SequencePosition { get; set; }
    }
}