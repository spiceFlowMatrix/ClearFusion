using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllItemsOrderQuery:IRequest<ApiResponse>
    {
        public string ItemId { get; set; }
    }
}
