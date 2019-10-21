using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditSelectedDistrictsCommand: BaseModel, IRequest<ApiResponse>
    {
        public long DistrictMultiSelectId { get; set; }
        public long ProjectId { get; set; }
        public List<long?> DistrictID { get; set; }
        public long? DistrictSelectionId { get; set; }
        public List<int> ProvinceId { get; set; }
    }
}