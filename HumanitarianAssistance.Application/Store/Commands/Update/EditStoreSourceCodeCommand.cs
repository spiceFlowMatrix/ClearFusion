using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditStoreSourceCodeCommand : BaseModel, IRequest<ApiResponse>
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
