using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditInterviewTechnicalQuestionsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int InterviewTechnicalQuestionsId { get; set; }
        public string Question { get; set; }
        public int OfficeId { get; set; }
    }
}
