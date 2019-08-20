using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
   public class EditDonorEligibilityDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? DonorEligibilityDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
    }
}
