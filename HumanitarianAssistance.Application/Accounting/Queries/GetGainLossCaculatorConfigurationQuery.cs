using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetGainLossCaculatorConfigurationQuery: IRequest<object>
    {
        public string UserId { get; set; }
    }
}