using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Domain.Entities.Project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
  public class AddProjectIndicatorQuestionsCommand : BaseModel, IRequest<ApiResponse> 
    {
        public AddProjectIndicatorQuestionsCommand()
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
