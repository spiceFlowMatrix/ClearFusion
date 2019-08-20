using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchaseUnitTypeCommand:BaseModel,IRequest<ApiResponse>
    {
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
    }
}
