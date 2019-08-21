using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class UpdatePurchaseImageCommand:BaseModel,IRequest<ApiResponse>
    {
        public string PurchaseId { get; set; }
        public string Invoice { get; set; }
    }
}
