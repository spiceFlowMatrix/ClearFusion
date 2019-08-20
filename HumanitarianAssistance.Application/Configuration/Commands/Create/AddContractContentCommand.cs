using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddContractContentCommand: BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeContractTypeId { get; set; }
		public string ContentEnglish { get; set; }
		public string ContentDari { get; set; }
		public int OfficeId { get; set; }
    }
}