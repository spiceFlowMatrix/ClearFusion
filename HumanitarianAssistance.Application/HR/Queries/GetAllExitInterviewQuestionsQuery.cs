using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllExitInterviewQuestionsQuery: IRequest<object>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}