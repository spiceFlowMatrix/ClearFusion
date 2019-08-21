using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditHiringRequestCandidateCommand : BaseModel, IRequest<ApiResponse>
    {
        public int? EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int GradeId { get; set; }
        public int? EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public long HiringRequestId { get; set; }
        public string Gender { get; set; }
        public bool IsInterViewed { get; set; }
        public bool IsShortListed { get; set; }
        public bool IsSelected { get; set; }
        public long? CandidateId { get; set; }
    }
}
