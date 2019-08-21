using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class HiringCandidateDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int HiringCandidateID { get; set; }
        [Key, ForeignKey("HiringDetail")]
        public int? HiringID { get; set; }
        public int? ProjectNumber { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string EducationLevel { get; set; }
        public DateTime? BirthDate { get; set; }
        [StringLength(10)]
        public string Sex { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string CVAttachment { get; set; }
        [StringLength(100)]
        public string FormAttachment { get; set; }
        [StringLength(100)]
        public string Interview { get; set; }
        [StringLength(100)]
        public string Committee { get; set; }
        public DateTime? InterviewDate { get; set; }
        public int? OralTestMarks { get; set; }
        public int? WrittenTestMarks { get; set; }
        public int? TotalMarks { get; set; }
        public byte?[] Percentage { get; set; }
        public byte?[] Status { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(50)]
        public string Profession { get; set; }
        public float? RelatedYearsOfExperience { get; set; }
    }
}
