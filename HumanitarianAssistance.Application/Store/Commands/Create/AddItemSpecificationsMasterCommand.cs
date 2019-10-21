using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddItemSpecificationsMasterCommand:BaseModel,IRequest<ApiResponse>
    {
        public int ItemSpecificationMasterId { get; set; }
        public string ItemSpecificationField { get; set; }
        public int OfficeId { get; set; }
        public int ItemTypeId { get; set; }
    }
}
