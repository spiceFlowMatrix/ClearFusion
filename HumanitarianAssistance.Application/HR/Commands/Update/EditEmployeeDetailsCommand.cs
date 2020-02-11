using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update {
    public class EditEmployeeDetailsCommand : BaseModel, IRequest<ApiResponse> {
        public EmployeeBasicDetail EmployeeBasicDetail { get; set; }
        public EmployeeProfessionalDetails EmployeeProfessionalDetails { get; set; }
        public EmployeePensionDetail EmployeePensionDetail { get; set; }
    }
    public class EmployeeBasicDetail {
        public int? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public int? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? MaritalStatus { get; set; }
        public int? Country { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public string BirthPlace { get; set; }
        public string TinNumber { get; set; }
        public string PassportNumber { get; set; }
        public string University { get; set; }
        public int? Profession { get; set; }
        public int? Qualification { get; set; }
        public int? ExperienceYear { get; set; }
        public int? ExperienceMonth { get; set; }
        public string IssuePlace { get; set; }
        public string ReferBy { get; set; }
        public string PreviousWork { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
    }
    public class EmployeeProfessionalDetails {
        public int? EmployeeType { get; set; }
        public int? JobGrade { get; set; }
        public int Office { get; set; }
        public int? Department { get; set; }
        public int? Designation { get; set; }
        public int? EmployeeCotractType { get; set; }
        public DateTime HiredOn { get; set; }
        public int? AttendanceGroup { get; set; }
        public int? DutyStation { get; set; }
        public string TrainingAndBenefits { get; set; }
        public string JobDescription { get; set; }
    }
    public class EmployeePensionDetail {
        public DateTime? PensionDate { get; set; }
        public List<PensionList> PensionList { get; set; }
    }
    public class PensionList {
        public int PensionId { get; set; }
        public int Currency { get; set; }
        public double? Amount { get; set; }
    }
}