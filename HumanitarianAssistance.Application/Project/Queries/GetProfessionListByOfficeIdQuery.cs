using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProfessionListByOfficeIdQuery : IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
    } 
}
