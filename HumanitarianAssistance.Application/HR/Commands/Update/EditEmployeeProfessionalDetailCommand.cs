using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
   public class EditEmployeeProfessionalDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? EmployeeProfessionalId { get; set; }
        public int? EmployeeId { get; set; }
        public int? EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string Status { get; set; }
        public int? OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int? DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? HiredOn { get; set; }
        public DateTime? FiredOn { get; set; }
        public string FiredReason { get; set; }
        public DateTime? ResignationOn { get; set; }
        public string ResignationReason { get; set; }
        public string JobDescription { get; set; }
        public string TrainingBenefits { get; set; }
        public int? EmployeeContractTypeId { get; set; }
        public string MembershipSupportInPoliticalParty { get; set; }           // New field added
        public string BirthPlace { get; set; }
        public string PassportNo { get; set; }
        public int? ProfessionId { get; set; }
        public string TinNumber { get; set; }
        public long? AttendanceGroupId { get; set; }
        public string AttendanceGroupName { get; set; }
    }
}
