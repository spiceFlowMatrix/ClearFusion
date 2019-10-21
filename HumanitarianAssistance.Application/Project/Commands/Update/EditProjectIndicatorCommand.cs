using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditProjectIndicatorCommand : BaseModel, IRequest<ApiResponse>
    {
        public EditProjectIndicatorCommand()
        {
            IndicatorQuestions = new List<IndicatorQuestions>();
        }

        public new bool? IsDeleted { get; set; }
        public long ProjectIndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string IndicatorCode { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public List<IndicatorQuestions> IndicatorQuestions { get; set; }
    }
}
