using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditRatingBasedCriteriaQuestionsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int QuestionsId { get; set; }
        public string Question { get; set; }
        public int OfficeId { get; set; }

    }
}
