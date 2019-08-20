using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteSectorDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long SectorId { get; set; }
        public string SectorName { get; set; }
        public string SectorCode { get; set; }
    }
}