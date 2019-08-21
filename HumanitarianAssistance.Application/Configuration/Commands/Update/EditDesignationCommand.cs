using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditDesignationCommand : BaseModel, IRequest<ApiResponse>
    {
        public int DesignationId { get; set; }
        public string Designation { get; set; }
    }
}