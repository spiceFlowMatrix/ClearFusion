using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class HiringDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int HiringID { get; set; }
        public int? ProjectNumber { get; set; }
        [StringLength(50)]
        public string Position { get; set; }
        [StringLength(5)]
        public string Grade { get; set; }
        public int? Unit { get; set; }
        public float? BasicPay { get; set; }
        [StringLength(10)]
        public string CurrencyCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(10)]
        public string OfficeCode { get; set; }
        [StringLength(10)]
        public string BudgetLine { get; set; }
        [StringLength(20)]
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public string JobAnnouncement { get; set; }
        [StringLength(20)]
        public string JobAnnouncementCode { get; set; }
        [StringLength(100)]
        public string Attachment { get; set; }
        public byte? Status { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(50)]
        public string EducationLevel { get; set; }
        [StringLength(50)]
        public string Profession { get; set; }
        public float? RelatedYearsOfExperience { get; set; }
        [StringLength(10)]
        public string ExistingEmployeeCode { get; set; }
        [StringLength(100)]
        public string ExistingEmployeeName { get; set; }

        public float? Percentage { get; set; }
        [StringLength(50)]
        public string FromDesignation { get; set; }
        [StringLength(50)]
        public string ToDesignation { get; set; }
        public string FromBLine { get; set; }
        [StringLength(10)]
        public string ToBLine { get; set; }
        [StringLength(10)]
        public string FromJob { get; set; }
        [StringLength(10)]
        public string ToJob { get; set; }
        [StringLength(10)]
        public string FromProject { get; set; }
        [StringLength(10)]
        public string ToProject { get; set; }
        public float? FromSalary { get; set; }
        public float? ToSalary { get; set; }
        public byte? ExistingStatus { get; set; }
        [StringLength(10)]
        public string ToCurrency { get; set; }
        [StringLength(10)]
        public string FromCurrency { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
