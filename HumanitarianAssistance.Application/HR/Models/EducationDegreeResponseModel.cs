using System.Collections.Generic;
using HumanitarianAssistance.Application.Configuration.Models;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EducationDegreeResponseModel
    {
        public int TotalCount { get; set; }
        public List<EducationDegreeModel> EducationDegreeList { get; set; }
    }
}