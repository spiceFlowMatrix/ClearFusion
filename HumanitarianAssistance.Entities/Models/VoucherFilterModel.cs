using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class VoucherFilterModel
    {
        public DateTime? Date { get; set; }
        public List<int> OfficesList { get; set; }
    }
}
