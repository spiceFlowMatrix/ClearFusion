using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditItemSpecificationsDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int ItemSpecificationMasterId { get; set; }
        public string ItemId { get; set; }
        public string ItemSpecificationValue { get; set; }
        public string ItemSpecificationField { get; set; }
    }
}
