using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddStoreSourceCodeCommand : BaseModel, IRequest<ApiResponse>
    {
        public long SourceCodeId { get; set; }
        public int CodeTypeId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string Guarantor { get; set; }
    }
}
