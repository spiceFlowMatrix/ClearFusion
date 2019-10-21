using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEmployeeLanguagesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int SpeakLanguageId { get; set; }
        public int LanguageId { get; set; }
        public int Reading { get; set; }
        public int Writing { get; set; }
        public int Speaking { get; set; }
        public int Listening { get; set; }
        public int EmployeeId { get; set; }
    }
}
