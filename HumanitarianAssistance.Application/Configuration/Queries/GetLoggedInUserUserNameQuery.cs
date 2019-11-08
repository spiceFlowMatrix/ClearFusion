using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetLoggedInUserUserNameQuery: IRequest<string>
    {
        public string Id { get; set; }
    }
}