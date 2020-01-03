using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetExchangeRateVerifiedQuery: IRequest<ApiResponse>
    {
        public DateTime ExchangeRateDate { get; set; }
        
    }
}