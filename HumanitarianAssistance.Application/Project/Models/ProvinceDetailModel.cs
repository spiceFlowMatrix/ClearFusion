using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProvinceDetailModel: BaseModel
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int? CountryId { get; set; }
    }
}