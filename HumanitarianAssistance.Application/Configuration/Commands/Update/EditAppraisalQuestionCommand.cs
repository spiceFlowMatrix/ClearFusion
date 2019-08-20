using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditAppraisalQuestionCommand: BaseModel, IRequest<ApiResponse>
    {
        public int? EmployeeAppraisalQuestionsId { get; set; }
        public int AppraisalGeneralQuestionsId { get; set; }
		public int SequenceNo { get; set; }
		public string Question { get; set; }
		public string DariQuestion { get; set; }
	    public int? OfficeId { get; set; }
    }
}