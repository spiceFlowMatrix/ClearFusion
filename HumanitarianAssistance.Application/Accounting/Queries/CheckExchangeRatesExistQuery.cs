using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class CheckExchangeRatesExistQuery: IRequest<ApiResponse>
    {
        public DateTime ExchangeRateDate { get; set; }
        public int OfficeId { get; set; }
    }
}