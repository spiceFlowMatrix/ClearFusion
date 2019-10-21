using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddSectorDetailsCommand: BaseModel, IRequest<ApiResponse>
    {
        public long SectorId { get; set; }
        public string SectorName { get; set; }
        public string SectorCode { get; set; }
        public long ProjectId { get; set; }
    }
}