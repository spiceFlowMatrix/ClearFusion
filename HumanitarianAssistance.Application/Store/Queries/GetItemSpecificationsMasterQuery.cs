using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetItemSpecificationsMasterQuery:IRequest<ApiResponse>
    {
        public int ItemTypeId { get; set; }
        public int OfficeId { get; set; }
    }
}
