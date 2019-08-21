using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditCountryByProjectIdCommand: BaseModel, IRequest<ApiResponse>
    {
        public long CountryMultiSelectId { get; set; }
        public long ProjectId { get; set; }
        public List<int?> CountryId { get; set; }
        public long? CountrySelectionId { get; set; }
    }
}