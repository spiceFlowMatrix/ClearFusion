using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddRatingBasedCriteriaQuestionsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int QuestionsId { get; set; }
        public string Question { get; set; }
        public int OfficeId { get; set; }
    }
}
