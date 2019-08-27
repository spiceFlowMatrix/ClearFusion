
namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class ChartOfAccountModel
    {
        public long ChartOfAccountId { get; set; }
        public string ChartOfAccountCode { get; set; }
        public string AccountName { get; set; }
        public long ParentID { get; set; }
        public int AccountLevelId { get; set; }
    }
}