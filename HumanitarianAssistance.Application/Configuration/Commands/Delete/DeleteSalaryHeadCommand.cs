using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
   public class DeleteSalaryHeadCommand :BaseModel, IRequest<ApiResponse>
    {
        public int SalaryHeadId { get; set; }

    }
}
