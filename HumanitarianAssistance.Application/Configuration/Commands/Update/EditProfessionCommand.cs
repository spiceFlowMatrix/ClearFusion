using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
   public class EditProfessionCommand : BaseModel, IRequest<ApiResponse>
    {
        public int ProfessionId { get; set; }
        public string ProfessionName { get; set; }
    }
}
