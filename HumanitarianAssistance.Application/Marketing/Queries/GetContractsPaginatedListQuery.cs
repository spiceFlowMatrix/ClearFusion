using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetContractsPaginatedListQuery : IRequest<ApiResponse>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
