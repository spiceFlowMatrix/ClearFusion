﻿using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class UpdateInvoiceCommand:BaseModel,IRequest<ApiResponse>
    {
        public long PurchaseId { get; set; }
        public string Invoice { get; set; }
    }
}
