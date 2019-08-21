using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddItemSpecificationsDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public List<ItemSpecificationDetailModel> ItemSpecificationDetail { get; set; }
    }
}
