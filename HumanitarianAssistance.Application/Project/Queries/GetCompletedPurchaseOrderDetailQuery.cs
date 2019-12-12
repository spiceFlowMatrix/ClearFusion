using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCompletedPurchaseOrderDetailQuery  : BaseModel, IRequest<ApiResponse>
    {       
        public long requestId { get; set; }
    }
}