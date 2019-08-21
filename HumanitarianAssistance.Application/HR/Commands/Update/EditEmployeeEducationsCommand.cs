using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
  public  class EditEmployeeEducationsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeEducationsId { get; set; }
        public DateTime EducationFrom { get; set; }
        public DateTime EducationTo { get; set; }
        public string FieldOfStudy { get; set; }
        public string Institute { get; set; }
        public string Degree { get; set; }
        public int EmployeeID { get; set; }
    }
}
