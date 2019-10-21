using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddProfessionCommand : BaseModel, IRequest<ApiResponse>
    {
        public int ProfessionId { get; set; }
        public string ProfessionName { get; set; }
    }
}
