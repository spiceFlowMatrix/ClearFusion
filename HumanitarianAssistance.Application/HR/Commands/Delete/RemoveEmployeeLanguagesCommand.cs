using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class RemoveEmployeeLanguagesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int SpeakLanguageId { get; set; }

    }
}
