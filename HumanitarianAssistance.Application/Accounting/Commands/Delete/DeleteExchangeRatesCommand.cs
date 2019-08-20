using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Delete
{
    public class DeleteExchangeRatesCommand: BaseModel, IRequest<ApiResponse>
    {
        public DateTime ExchangeRateDate { get; set;}
    }
}