using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class PayrollMonthlyHourDetailModel : BaseModel
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
    }
}
