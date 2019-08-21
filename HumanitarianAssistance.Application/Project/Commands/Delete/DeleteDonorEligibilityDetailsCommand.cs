using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
  public  class DeleteDonorEligibilityDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long DonorEligibilityDetailId { get; set; }
    }
}
