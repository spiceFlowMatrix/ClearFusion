using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeHealthDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long HealthInfoId { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeDetail EmployeeDetails { get; set; }
        [StringLength(20)]
        public string BloodGroup { get; set; }
        public string MedicalHistory { get; set; }
        public bool SmokeAndDrink { get; set; }
        public bool Insurance { get; set; }
        public string MedicalInsurance { get; set; }
        public bool MeasureDiseases { get; set; }
        public bool AllergicSubstance { get; set; }
        public bool FamilyHistory { get; set; }
    }
}
