using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class CandidateDetails : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public long CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        // public int AccountStatus { get; set; }
        public int GenderId { get; set; }

        [ForeignKey ("CountryId")]
        public int CountryId { get; set; }
        public CountryDetails CountryDetail { get; set; }

        [ForeignKey ("ProvinceId")]
        public int ProvinceId { get; set; }
        public ProvinceDetails ProvinceDetail { get; set; }

        [ForeignKey ("DistrictID")]
        public long DistrictID { get; set; }
        public DistrictDetail DistrictDetail { get; set; }
        // public double TotalExperienceInYear { get; set; }
        public double RelevantExperienceInYear { get; set; }
        public double IrrelevantExperienceInYear { get; set; }
        public string Password {get; set;}

        public int EducationDegreeId { get; set; }
        [ForeignKey ("EducationDegreeId")]
        public EducationDegreeMaster EducationDegreeMaster { get; set; }
        public DateTime DateOfBirth { get; set; }

        // [ForeignKey ("GradeId")]
        // public int GradeId { get; set; }
        // public JobGrade JobGrade { get; set; }

        // [ForeignKey ("OfficeId")]
        // public int OfficeId { get; set; }
        // public OfficeDetail OfficeDetail { get; set; }

        [ForeignKey ("ProfessionId")]
        public int ProfessionId { get; set; }
        public ProfessionDetails ProfessionDetails { get; set; }

        public string PreviousWork { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public int ExperienceYear { get; set; }
        public int ExperienceMonth { get; set; }
        public string Remarks { get; set; }
    }
}