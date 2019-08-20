using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetNatureByIdQuery : IRequest<ApiResponse>
    {
        public int NatureId { get; set; }
    }
}
