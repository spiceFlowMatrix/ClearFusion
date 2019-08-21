using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetCurrencyByCurrencyCodeQuery: IRequest<ApiResponse>
    {
        public string CurrencyCode { get; set; }
    }
}