using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllAccountBalancesByCategoryQuery : IRequest<ApiResponse>
    {
        public int id { get; set; }
        public DateTime asOfDate { get; set; }
        public DateTime upToDate { get; set; }
        public int currency { get; set; }
    }
}