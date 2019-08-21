using System;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditEligibilityCriteriaDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long EligibilityId { get; set; }
        public long ProjectId { get; set; }
        public bool? DonorCriteriaMet { get; set; }
        public bool? EligibilityDealine { get; set; }
        public bool? CoPartnership { get; set; }
    }
}
