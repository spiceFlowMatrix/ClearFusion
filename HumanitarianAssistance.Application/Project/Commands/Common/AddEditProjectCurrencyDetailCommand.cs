using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectCurrencyDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public int? CurrencyId { get; set; }
        public long? ProjectId { get; set; }
    }
}
