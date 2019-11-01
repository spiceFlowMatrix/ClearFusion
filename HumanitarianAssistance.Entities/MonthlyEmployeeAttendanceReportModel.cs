using HumanitarianAssistance.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels
{
    public class MonthlyEmployeeAttendanceReportModel: BaseModel
	{
		public int employeeid { get; set; }
		public int year { get; set; }
		public int month { get; set; }
		public int OfficeId { get; set; }
	}
}
