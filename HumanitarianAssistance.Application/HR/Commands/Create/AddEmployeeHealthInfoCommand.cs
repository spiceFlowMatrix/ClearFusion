using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
  public class AddEmployeeHealthInfoCommand : BaseModel, IRequest<ApiResponse>
    {
        public long EmployeeHealthInfoId { get; set; }
        public int EmployeeId { get; set; }

        //HealthDetails
        public string PhysicanName { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }

        //Physical Exams
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public float? BloodPressure { get; set; }
        public float? VisualWithoutGlassesR { get; set; }
        public float? VisualWithoutGlassesL { get; set; }
        public float? VisualWithGlassesR { get; set; }
        public float? VisualWithGlassesL { get; set; }
        public float? HearingR { get; set; }
        public string HearingRType { get; set; }
        public float? HearingL { get; set; }
        public string HearingLType { get; set; }

        public string HistoryOfPastIllness { get; set; }
        public string HealthPresentCondition { get; set; }
        public string ResultOfChestXRay { get; set; }

        // Laboratory Test
        public string BloodGroup { get; set; }
        public string Hbs { get; set; }
        public string Hcv { get; set; }
        public string OverallHealthCondition { get; set; }


        public long HealthInfoId { get; set; }
        public string MedicalHistory { get; set; }
        public bool SmokeAndDrink { get; set; }
        public bool Insurance { get; set; }
        public string MedicalInsurance { get; set; }
        public bool MeasureDieases { get; set; }
        public bool AllergicSubstance { get; set; }
        public bool FamilyHistory { get; set; }
    }
}
