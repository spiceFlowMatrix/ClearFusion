using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
  public  class AddEmployeeHealthQuestionCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeHealthQuestionId { get; set; }
        public int EmployeeId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
