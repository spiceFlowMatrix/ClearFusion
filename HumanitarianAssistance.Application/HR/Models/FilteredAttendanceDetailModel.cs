using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class FilteredAttendanceDetailModel
    {
        public FilteredAttendanceDetailModel()
        {
            attendanceList = new List<AttendanceListModel>();
        }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
        public List<AttendanceListModel> attendanceList { get; set; }
    }
    public class AttendanceListModel
    {
        public string Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string Attended { get; set; }
    }
}