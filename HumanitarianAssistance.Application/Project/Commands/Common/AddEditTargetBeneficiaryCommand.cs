using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
   public class AddEditTargetBeneficiaryCommand : BaseModel, IRequest<ApiResponse>
    {
        public long TargetId { get; set; }
        public long ProjectId { get; set; }
        public int TargetType { get; set; }
        public string TargetName { get; set; }
    }
}
