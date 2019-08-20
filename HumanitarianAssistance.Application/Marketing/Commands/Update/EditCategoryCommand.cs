using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class EditCategoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
