using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeDetailListQuery: BaseModel, IRequest<object>
    {
        public int EmploymentStatusFilter { get; set; }
        public string FirstNameFilter { get; set; }
        public string LastNameFilter { get; set; }
        public int GenderFilter { get; set; }
        public string EmployeeIdFilter { get; set; }
        public int OfficeId { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
