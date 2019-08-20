using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllAccountByAccountHeadTypeIdQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }  
    }
}
