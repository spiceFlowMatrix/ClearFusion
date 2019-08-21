using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
   public class EditDepartmentCommand : BaseModel, IRequest<ApiResponse>
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string OfficeCode { get; set; }
        public int? OfficeId { get; set; }
        public string OfficeName { get; set; }
    }
}
