using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProvinceByProjectIdCommand: BaseModel, IRequest<ApiResponse>
    {
        public long ProvinceMultiSelectId { get; set; }
        public long ProjectId { get; set; }
        public List<int> ProvinceId { get; set; }
        public long? ProvinceSelectionId { get; set; }
    }
}