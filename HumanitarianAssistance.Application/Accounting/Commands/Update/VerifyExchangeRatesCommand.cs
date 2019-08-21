using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Update
{
    public class VerifyExchangeRatesCommand: BaseModel, IRequest<ApiResponse>
    {
        public DateTime ExchangeRateDate { get; set; }
    }
}