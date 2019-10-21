using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class HolidayDetailsModel : BaseModel
    {
        public long HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
		public int? FinancialYearId { get; set; }
        public int? OfficeId { get; set; }
        public int HolidayType { get; set; }
        public List<RepeatWeeklyDay> RepeatWeeklyDay { get; set; }
    }

    public class RepeatWeeklyDay
    {
        public string Day { get; set; }
    }

    public class DeleteHolidayDetailModel
    {
        public long HolidayId { get; set; }
        public DateTime HolidayDate { get; set; }
        public string UserId { get; set; }
    }
}
