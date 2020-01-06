using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Configuration.Models
{
    public class EducationDegreeModel: BaseModel
    {
        public long EducationDegreeId { get; set; }
        public string EducationDegreeName { get; set; }
    }
}