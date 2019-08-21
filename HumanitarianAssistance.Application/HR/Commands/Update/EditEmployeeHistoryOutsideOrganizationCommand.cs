using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeHistoryOutsideOrganizationCommand: BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeHistoryOutsideOrganizationId { get; set; }
		public int EmployeeHistoryOutsideCountryId { get; set; }
		public DateTime? EmploymentFrom { get; set; }
		public DateTime? EmploymentTo { get; set; }
		public string Organization { get; set; }
		public string MonthlySalary { get; set; }
		public string ReasonForLeaving { get; set; }
		public int EmployeeID { get; set; }
        public string Position { get; set; }
    }
}