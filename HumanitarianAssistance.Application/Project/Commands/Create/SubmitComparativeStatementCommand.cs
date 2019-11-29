using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class SubmitComparativeStatementCommand: BaseModel, IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
        public long[] SupplierIds { get; set; }
        public string Description { get; set; }
    }
}