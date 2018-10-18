using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class JournalVoucherFilterModel : BaseModel
    {
        public int? JournalNo { get; set; }
        public int? OfficeId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
