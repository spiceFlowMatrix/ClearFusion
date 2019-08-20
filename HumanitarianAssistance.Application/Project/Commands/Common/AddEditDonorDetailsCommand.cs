using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditDonorDetailsCommand: BaseModel, IRequest<ApiResponse>
    {
        public long? DonorId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonCell { get; set; }
    }
}