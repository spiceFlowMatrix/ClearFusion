using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditPayrollMonthlyHourCommand: BaseModel, IRequest<ApiResponse>
    {
        public int PayrollMonthlyHourID { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public int? PayrollMonth { get; set; }
        public int? PayrollYear { get; set; }
        public int? Hours { get; set; }
        public int? WorkingTime { get; set; }
        public DateTime? InTime { get; set; }
		public DateTime? OutTime { get; set; }
		public bool SaveForAllOffice { get; set; }
        public long? AttendanceGroupId { get; set; }
    }
}