using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class JournalVoucherFilterModel : BaseModel
    {
        public int? JournalNo { get; set; }
        public List<int?> OfficeIdList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
