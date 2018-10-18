using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ChartAccountDetailModel : BaseModel
    {
        public int AccountCode { get; set; }
        public string AccountName { get; set; }
        public int AccountLevelId { get; set; }
        public int? AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public long ParentID { get; set; }
        public float? DepRate { get; set; }
        public string DepMethod { get; set; }
        public int? AccountNote { get; set; }
        public string MDCode { get; set; }
        public Boolean? Show { get; set; }
        public long ChartOfAccountCode { get; set; }
    }

    public class AccountLevelModel
    {
        public int AccountLevelId { get; set; }
        public string AccountLevelName { get; set; }
    }

    public class AccountTypeModel
    {
        public int AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
    }
}
