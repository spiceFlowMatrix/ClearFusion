using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Create
{
    public class AddCategoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
