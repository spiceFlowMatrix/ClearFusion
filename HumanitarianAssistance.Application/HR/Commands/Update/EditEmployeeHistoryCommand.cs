using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
  public  class EditEmployeeHistoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public long HistoryID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? HistoryDate { get; set; }
        public string Description { get; set; }
    }
}
