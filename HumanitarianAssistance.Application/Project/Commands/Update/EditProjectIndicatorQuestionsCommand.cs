using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities.Project;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectIndicatorQuestionsCommand :BaseModel, IRequest<ApiResponse>
    {
         public EditProjectIndicatorQuestionsCommand()
        {
            VerificationSources = new List<VerificationSources>();
        }
        public long IndicatorQuestionId { get; set; }
        public string IndicatorQuestion { get; set; }
        public int? QuestionType { get; set; }
        public long ProjectIndicatorId { get; set; }
        public List<VerificationSources> VerificationSources { get; set; }
    }
}