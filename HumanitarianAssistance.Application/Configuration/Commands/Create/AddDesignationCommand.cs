using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddDesignationCommand: BaseModel, IRequest<ApiResponse>
    {

        public int DesignationId { get; set; }
        public string Designation { get; set; }
        
    }
}