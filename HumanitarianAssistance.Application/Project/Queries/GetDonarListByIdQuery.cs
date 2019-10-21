using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetDonarListByIdQuery: IRequest<ApiResponse>
    {
        public long DonorId { get; set; }
    }
}