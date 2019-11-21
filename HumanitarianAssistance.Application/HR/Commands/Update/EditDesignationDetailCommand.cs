using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditDesignationDetailCommand: BaseModel, IRequest<object>
    {
        public int? Id { get; set; }
        public string DesignationName { get; set; }
        public string Description { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }

    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }
}