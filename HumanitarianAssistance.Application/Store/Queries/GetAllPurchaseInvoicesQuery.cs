﻿using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllPurchaseInvoicesQuery:IRequest<ApiResponse>
    {
        public long PurchaseId { get; set; }
    }
}
