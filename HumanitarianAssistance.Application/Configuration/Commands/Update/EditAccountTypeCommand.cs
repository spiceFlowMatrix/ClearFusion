using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditAccountTypeCommand: BaseModel, IRequest<ApiResponse>
    {
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public int? AccountCategory { get; set; }
        public int? AccountHeadTypeId { get; set; }
    }
}