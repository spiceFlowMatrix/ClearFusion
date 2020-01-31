namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeMonthLeavesModel
    {
        public string Month { get; set; }
        public string LeaveType { get; set; }
        public int LeaveHours { get; set; }
        //Assigned Leave in Hours(no. of hours)
        public int? AssignedHours { get; set; }
        public int AppliedLeave { get; set; }
        public int? BalanceLeave { get; set; }
        //Assigned Leave in Days(no. of days)
        public int AssignedLeaveInDays { get; set; }
        public string Remarks { get; set; }
        public int LeaveReasonId { get; set; }
        public int LeaveMonthNumber { get; set; }
    }
}