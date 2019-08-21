using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetClientDetailsByIdQuery : IRequest<ApiResponse>
    {
            public int ClientId { get; set; }
    }
}
 