using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeProfessionalDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long EmployeeProfessionalId { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        //public EmployeeDetail EmployeeDetails { get; set; }
        public int? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public int? OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public OfficeDetail OfficeDetail { get; set; }
        public int? DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public DesignationDetail DesignationDetails { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime? HiredOn { get; set; }
        public DateTime? FiredOn { get; set; }
        public string FiredReason { get; set; }
        public DateTime? ResignationOn { get; set; }
        public string ResignationReason { get; set; }
        public string JobDescription { get; set; }
        public string TrainingBenefits { get; set; }
		public string MembershipSupportInPoliticalParty { get; set; }			// New field added

		public int? EmployeeContractTypeId { get; set; }
        [ForeignKey("EmployeeContractTypeId")]
		public EmployeeContractType EmployeeContractType { get; set; }
        public string Designation { get; set; }
        public string Profession { get; set; }
        public string Departments { get; set; }
        public string WorkType { get; set; }
        public long? ProjectId { get; set; }
        public string RegCode { get; set; }
        public string ContractStatus { get; set; }
        public string TinNumber { get; set; }
        public int? ProfessionId { get; set; }
        public long? AttendanceGroupId { get; set; }
        [ForeignKey("AttendanceGroupId")]
        public AttendanceGroupMaster AttendanceGroupMaster { get; set; }
        [ForeignKey("ProfessionId")]
        public ProfessionDetails professionDetails { get; set; }
    }
}
