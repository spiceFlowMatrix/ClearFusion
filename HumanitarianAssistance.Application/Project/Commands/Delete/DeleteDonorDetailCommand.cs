using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteDonorDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long DonorId { get; set; }
    }
}