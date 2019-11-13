using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class AssignLeaveToEmployeeModel : BaseModel
    {
        public long LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveReasonId { get; set; }
        public string LeaveReasonName { get; set; }
        public int Unit { get; set; }
        public int? AssignUnit { get; set; }
        public int? BlanceLeave { get; set; }
        public int FinancialYearId { get; set; }
        public string Description { get; set; }
    }

    public class EditAssignLeaveToEmployeeModel : BaseModel
    {
        public long LeaveId { get; set; }
        //public string LeaveReasonName { get; set; }
        //public int? Unit { get; set; }
        public int? AssignUnit { get; set; }
        //public int? BlanceLeave { get; set; }
    }
}
