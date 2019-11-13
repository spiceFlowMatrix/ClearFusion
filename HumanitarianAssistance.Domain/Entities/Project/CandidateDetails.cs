using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project {
    public class CandidateDetails : BaseEntity {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        [Column (Order = 1)]
        public long CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AccountStatus { get; set; }
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
        public double TotalExperienceInYear { get; set; }
        public double RelevantExperienceInYear { get; set; }
        public double IrrelevantExperienceInYear { get; set; }
        public bool IsShortlisted { get; set; }
        public bool IsSelected { get; set; }
    }
}