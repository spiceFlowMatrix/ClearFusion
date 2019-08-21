using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
   public class AddEditFinancialProjectDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? FinancialProjectDetailId { get; set; }
        public long ProjectId { get; set; }
        public int? ProjectSelectionId { get; set; }
        public string ProjectName { get; set; }
    }
}
