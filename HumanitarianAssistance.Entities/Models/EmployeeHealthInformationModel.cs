using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeHealthInformationModel : BaseModel
    {
        public long HealthInfoId { get; set; }
        public int EmployeeId { get; set; }
        public string BloodGroup { get; set; }
        public string MedicalHistory { get; set; }
        public bool SmokeAndDrink { get; set; }
        public bool Insurance { get; set; }
        public string MedicalInsurance { get; set; }
        public bool MeasureDieases { get; set; }
        public bool AllergicSubstance { get; set; }
        public bool FamilyHistory { get; set; }
    }
}
