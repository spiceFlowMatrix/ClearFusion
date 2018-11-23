using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class ChartOfAccountNewModel : BaseModel
    {
        public long ChartOfAccountNewId { get; set; }
        public string AccountName { get; set; }
        public long ParentID { get; set; }
        public int AccountLevelId { get; set; }
        public int? AccountTypeId { get; set; }
        public int? AccountFilterTypeId { get; set; }

    }
}
