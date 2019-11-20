using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddDesignationDetailCommand: BaseModel, IRequest<object>
    {
        public int Id { get; set; }
        public string DesignationName { get; set; }
        public string Description { get; set; }
        public List<TechnicalQuestions> Questions { get; set; }
    }

    public class TechnicalQuestions
    {
        public long QuestionId { get; set; }
        public string Question { get; set; }
    }
}