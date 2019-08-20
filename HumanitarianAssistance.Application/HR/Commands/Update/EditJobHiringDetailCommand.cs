using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditJobHiringDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long JobId { get; set; }
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public int? ProfessionId { get; set; }
        public string ProfessionName { get; set; }
        public int Unit { get; set; }
        public int OfficeId { get; set; }
        public int? GradeId { get; set; }
        public string GradeName { get; set; }
        public bool IsActive { get; set; }
        public int? ApprovedInterviews { get; set; }
    }
}
