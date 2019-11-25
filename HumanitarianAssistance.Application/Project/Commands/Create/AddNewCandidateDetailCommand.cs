using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create {
    public class AddNewCandidateDetailCommand : BaseModel, IRequest<ApiResponse> {
        public long ProjectId { get; set; }
        public long HiringRequestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int EducationDegree { get; set; }
        public int Profession { get; set; }
        public int Country { get; set; }
        public int Province { get; set; }
        public int ExperienceYear { get; set; }
        public int ExperienceMonth { get; set; }
        public long District { get; set; }
        public double RelevantExperienceInYear { get; set; }
        public double IrrelevantExperienceInYear { get; set; }
        public string PreviousWork { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
    }
}