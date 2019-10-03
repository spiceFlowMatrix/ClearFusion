using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetItemDetailByPurchaseIdQuery:IRequest<ItemDetailModel>
    {
        public long PurchaseId { get; set; }
    }
}