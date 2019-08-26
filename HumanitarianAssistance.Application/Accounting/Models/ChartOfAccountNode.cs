using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class ChartOfAccountNode
    {
        // public ChartOfAccountNode(ChartOfAccountModel baseObject, IList<ChartOfAccountNode> children)
        // {
        //     this.ChartOfAccountId = baseObject.ChartOfAccountId;
        //     this.ChartOfAccountCode = baseObject.ChartOfAccountCode;
        //     this.AccountName = baseObject.AccountName;
        //     this.ParentID = baseObject.ParentID;
        //     this.AccountLevelId = baseObject.AccountLevelId;
        // }
        public long ChartOfAccountId { get; set; }
        public string ChartOfAccountCode { get; set; }
        public string AccountName { get; set; }
        public long ParentID { get; set; }
        public int AccountLevelId { get; set; }
        public List<ChartOfAccountNode> ChildAccounts { get; set; }
    }
}