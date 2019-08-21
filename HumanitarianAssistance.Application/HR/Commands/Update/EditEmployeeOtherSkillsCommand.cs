using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
   public class EditEmployeeOtherSkillsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeOtherSkillsId { get; set; }
        public string TypeOfSkill { get; set; }
        public string AbilityLevel { get; set; }
        public string Experience { get; set; }
        public string Remarks { get; set; }
        public int EmployeeID { get; set; }
    }
}
