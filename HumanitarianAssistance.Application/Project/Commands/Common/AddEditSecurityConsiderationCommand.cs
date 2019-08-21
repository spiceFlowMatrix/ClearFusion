using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditSecurityConsiderationCommand: BaseModel, IRequest<ApiResponse>
    {
        public long? SecurityConsiderationMultiSelectId { get; set; }
        public long ProjectId { get; set; }
        public List<long?> SecurityConsiderationId { get; set; }
        public long? SecurityConsiderationSelectedId { get; set; }
    }
}