using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetSerialNumberQuery : IRequest<ApiResponse>
    {
        public string serialNumber { get; set; }  
    }
}
