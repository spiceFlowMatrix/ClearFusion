using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create {
    public class AddNewCandidateDetailCommand : BaseModel, IRequest<ApiResponse> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? AccountStatus { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? EducationDegree { get; set; }
        public int? Grade { get; set; }
        public int? Profession { get; set; }
        public int? Office { get; set; }
        public int? Country { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? TotalExperienceInYear { get; set; }
        public int? RelevantExperienceInYear { get; set; }
        public int? IrrelevantExperienceInYear { get; set; }
    }
}