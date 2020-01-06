using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetSequenceNumberQuery: IRequest<object>
    {
        public int QuestionType { get; set; }
        public long Id { get; set; }
    }
}