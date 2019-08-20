using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string RegCode { get; set; }
        public string EmployeeName { get; set; }
        public string IDCard { get; set; }
        public string FatherName { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ReferBy { get; set; }
        public string Passport { get; set; }
        public string Language { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PreviousWork { get; set; }
        public string Remarks { get; set; }
        public string EmployeePhoto { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Qualification { get; set; }
        public string Profession { get; set; }
        public string Experience { get; set; }
        public string Nationality { get; set; }
        public string SpeakLanguageList { get; set; }
        public string CloseRelativeList { get; set; }
        public string RefereeList { get; set; }
        public string EducationList { get; set; }
        public string NationalEmploymentList { get; set; }
        public string InternationalEmploymentList { get; set; }
        public string OtherSkillList { get; set; }
        public string MaritalStatus { get; set; }
        public long? ProjectId { get; set; }

        //======
        public int? CountryId { get; set; }
        public CountryDetails CountryDetails { get; set; }
        public int? GradeId { get; set; }
        public int? NationalityId { get; set; }
        public NationalityDetails NationalityDetails { get; set; }

        public int? HigherQualificationId { get; set; }

        [ForeignKey("HigherQualificationId")]
        public QualificationDetails QualificationDetails { get; set; }

        public List<InterviewScheduleDetails> InterviewScheduleDetails { get; set; }
        public List<EmployeeAttendance> EmployeeAttendance { get; set; }
        public EmployeeProfessionalDetail EmployeeProfessionalDetail { get; set; }
        public EmployeeSalaryDetails EmployeeSalaryDetails { get; set; }
        public List<EmployeePayroll> EmployeePayrollList { get; set; }
        public string Extension { get; set; }
        public string DocumentGUID { get; set; }
        public int? DocumentType { get; set; }
        public int? SexId { get; set; }
        public int? EmployeeTypeId { get; set; }
        public int? Age { get; set; }
        public int? ExperienceYear { get; set; }
        public int? ExperienceMonth { get; set; }
        public string PassportNo { get; set; }
        public string University { get; set; }
        public string BirthPlace { get; set; }
        public string IssuePlace { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public ProvinceDetails ProvinceDetails { get; set; }
        public string Resume { get; set; }
        public int? MaritalStatusId { get; set; }
        public  virtual ICollection<InterviewDetails> InterviewDetails { get; set; }
    }
}
