using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.OnlyForDT
{
    public class EmployeeDetailDT : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        public string CurrencyCode { get; set; }
        public string RegCode { get; set; }
        public float? BasicPay { get; set; }
        public Double? FoodAllowance { get; set; }
        public Double? MedicalAllowance { get; set; }
        public Double? TrAllowance { get; set; }
        public Double? OtherAllowance { get; set; }
        public Double? Other1Allowance { get; set; }
        public Double? Other2Allowance { get; set; }
        public string Other1Description { get; set; }
        public string Other2Description { get; set; }

        [StringLength(50)]
        public string Designation { get; set; }

        [StringLength(100)]
        public string FatherName { get; set; }

        [StringLength(5)]
        public string sex { get; set; }

        [StringLength(200)]
        public string PermanentAddress { get; set; }

        [StringLength(200)]
        public string CurrentAddress { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string Province { get; set; }

        [StringLength(50)]
        public string District { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Village { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string ReferBy { get; set; }

        public DateTime? HireOn { get; set; }

        public DateTime? FireOn { get; set; }

        public DateTime? ResignedOn { get; set; }

        [StringLength(200)]
        public string FireReason { get; set; }

        [StringLength(200)]
        public string ResignedReason { get; set; }

        [StringLength(5)]
        public string ContractStatus { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public Double? ContractNumber { get; set; }

        public Double? ContractPeriod { get; set; }

        [StringLength(10)]
        public string PeriodType { get; set; }

        [StringLength(50)]
        public string Profession { get; set; }

        [StringLength(50)]
        public string Qualification { get; set; }

        [StringLength(10)]
        public string grade { get; set; }

        public string Remarks { get; set; }

        [StringLength(50)]
        public string Experience { get; set; }

        [StringLength(50)]
        public string PreviousWork { get; set; }

        [StringLength(50)]
        public string PreviousExperience { get; set; }

        [StringLength(50)]
        public string Nationality { get; set; }

        [StringLength(50)]
        public string Passport { get; set; }

        [StringLength(20)]
        public string IdCard { get; set; }

        [StringLength(30)]
        public string Language { get; set; }

        public int Age { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string PlaceOfBirth { get; set; }

        [StringLength(50)]
        public string Department { get; set; }

        public string JobDescription { get; set; }

        public string Training { get; set; }

        public Boolean? Extended { get; set; }

        public Double? CapacityBuildingDeductibles { get; set; }

        public Double? SecurityDeductibles { get; set; }

        public Double? FinesDeductibles { get; set; }

        public Double? OtherDeductibles { get; set; }

        [StringLength(200)]
        public string FineReason { get; set; }

        public Double? PensionDeductibles { get; set; }

        //[StringLength(200)]
        public int EmployeePhoto { get; set; }

        [StringLength(20)]
        public string WorkType { get; set; }

        public Double? MonthlyBasicPay { get; set; }

        [StringLength(20)]
        public string TinNo { get; set; }

        [StringLength(100)]
        public string BankAccount { get; set; }

        public int? CasualLeaveAllowance { get; set; }

        public int? MedicalLeaveAllowance { get; set; }

        public int? EmergencyLeaveAllowance { get; set; }

        public int? MaternityLeaveAllowance { get; set; }

        [StringLength(100)]
        public string EmployeeNameDari { get; set; }

        [StringLength(20)]
        public string EmployeeCodeDari { get; set; }

        [StringLength(100)]
        public string FatherNameDari { get; set; }

        [StringLength(50)]
        public string DesignationDari { get; set; }

        [StringLength(50)]
        public string ProvinceDari { get; set; }

        [StringLength(50)]
        public string CityDari { get; set; }

        [StringLength(50)]
        public string ContractStartDateDari { get; set; }

        [StringLength(50)]
        public string ContractEndDateDari { get; set; }

        public Double? ContractPeriodDari { get; set; }

        [StringLength(10)]
        public string PeriodTypeDari { get; set; }

        public int? NoOfChildren { get; set; }

        [StringLength(15)]
        public string MaritalStatus { get; set; }

        public string SpeakLanguageList { get; set; }

        public string CloseRelativeList { get; set; }

        public string RefereeList { get; set; }

        public string EducationList { get; set; }

        public string NationalEmploymentList { get; set; }

        public string InternationalEmploymentList { get; set; }

        public string OtherSkillList { get; set; }

        public long? ProjectId { get; set; }

        [StringLength(10)]
        public string ETN { get; set; }

        [StringLength(10)]
        public string SECT { get; set; }

        public string Comments { get; set; }

        public string PoliticalPartyMembership { get; set; }

        public DateTime? TingenerateDate { get; set; }

    }
}
