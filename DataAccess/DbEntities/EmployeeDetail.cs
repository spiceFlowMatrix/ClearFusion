using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class EmployeeDetail : BaseEntityWithoutId
    {
		public EmployeeDetail()
		{
			EmployeeAttendance = new List<DbEntities.EmployeeAttendance>();
		}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int EmployeeID { get; set; }
        [StringLength(20)]
        public string EmployeeCode { get; set; }
        public int? EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(20)]
        public string IDCard { get; set; } 
        [StringLength(100)]
        public string FatherName { get; set; }
        //public int? OfficeId { get; set; }
        //public OfficeDetail OfficeDetails { get; set; }
        //[StringLength(50)]
        //public int? DepartmentId { get; set; }
        //public Department DepartmentDetails { get; set; }
        //[StringLength(10)]
        public int? GradeId { get; set; }
        //public int? DesignationId { get; set; }
        //public DesignationDetail DesignationDetails { get; set; }
        [StringLength(200)]
        public string PermanentAddress { get; set; }
        [StringLength(200)]
        public string CurrentAddress { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string District { get; set; }
        [StringLength(50)]
        public int? ProvinceId { get; set; }
        public ProvinceDetails ProvinceDetails { get; set; }
        [StringLength(50)]
        public int? CountryId { get; set; }
        public CountryDetails CountryDetails { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string Fax { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string ReferBy { get; set; }
        [StringLength(50)]
        public string Passport { get; set; }
        [StringLength(50)]
        public int? NationalityId { get; set; }
        public NationalityDetails NationalityDetails { get; set; }
        [StringLength(30)]
        public string Language { get; set; }
        [StringLength(5)]
        public int? SexId { get; set; }
        [StringLength(30)]
        public string DateOfBirth { get; set; }
        [StringLength(50)]
        public int? Age { get; set; }
        public string PlaceOfBirth { get; set; }
        [StringLength(50)]
        public int? HigherQualificationId { get; set; }    
        [ForeignKey("HigherQualificationId")]
        public QualificationDetails QualificationDetails { get; set; }
        [StringLength(50)]
        public int? ProfessionId { get; set; }
        public ProfessionDetails ProfessionDetails { get; set; }
        [StringLength(50)]
        public string PreviousWork { get; set; }
        public string Remarks { get; set; }
        [StringLength(200)]
        public int? ExperienceYear { get; set; }
        public int? ExperienceMonth { get; set; }
        public string Resume { get; set; }
        public string EmployeePhoto { get; set; }
		public string Extension { get; set; }
		public string DocumentGUID { get; set; }
		public int? DocumentType { get; set; }	
		public List<InterviewScheduleDetails> InterviewScheduleDetails { get; set; }
        public List<EmployeeAttendance> EmployeeAttendance { get; set; }

        public EmployeeProfessionalDetail EmployeeProfessionalDetail { get; set; }
        public EmployeeSalaryDetails EmployeeSalaryDetails { get; set; }
		public List<EmployeePayroll> EmployeePayrollList { get; set; }
		//public EmployeePaymentTypes EmployeePaymentTypes { get; set; }
		public EmployeePensionRate EmployeePensionRate { get; set; }

		//[StringLength(5)]
		//public string ContractStatus { get; set; }
		//public int ContactType { get; set; }
		//public DateTime? ContractStartDate { get; set; }
		//public DateTime? ContractEndDate { get; set; }
		//public int ContractNumber { get; set; }
		//public float? ContractPeriod { get; set; }
		//[StringLength(10)]
		//public string PeriodType { get; set; }
		//public DateTime? HireOn { get; set; }
		//public DateTime? FireOn { get; set; }
		//[StringLength(200)]
		//public string FireReason { get; set; }
		//public DateTime? ResignedOn { get; set; }
		//[StringLength(200)]
		//public string ResignedReason { get; set; }
		//public int? CurrencyId { get; set; }
		//public CurrencyDetails CurrencyDetails { get; set; }
		//public float? MonthlyBasicPay { get; set; }
		//public float? HourlyRate { get; set; }
		//public float? FoodAllowance { get; set; }
		//public float? TrAllowance { get; set; }
		//public float? MedicalAllowance { get; set; }
		//public float? OtherAllowance { get; set; }
		//public float? Other1Allowance { get; set; }
		//public float? Other2Allowance { get; set; }
		//[StringLength(200)]
		//public string Other1Description { get; set; }
		//[StringLength(200)]
		//public string Other2Description { get; set; }
		//public float? CapacityBuildingDeductibles { get; set; }
		//public float? SecurityDeductibles { get; set; }
		//public float? FinesDeductibles { get; set; }
		//public float? OtherDeductibles { get; set; }
		//[StringLength(200)]
		//public string FineReason { get; set; }
		//public string JobDescription { get; set; }
		//public string Training { get; set; }


		//public int? Age { get; set; } 
		//public Boolean? Extended { get; set; }
		//public float? PensionDeductibles { get; set; }
		//[StringLength(20)]
		//public string WorkType { get; set; }
		//[StringLength(10)]
		//public string RegCode { get; set; }
		//public float? BasicPay { get; set; }


	}
}
