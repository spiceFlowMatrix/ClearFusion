namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class AccountDetailModel
    {
        public long AccountCode { get; set; }
        public string AccountName { get; set; }
        // public string AccountCodeAndName { get; set; }
        // public string AccountName
        // {
        //     get => AccountCodeAndName;
        //     set => AccountCodeAndName = AccountCode + " - " + ChartOfAccountNewCode;
        // }
        public string ChartOfAccountNewCode { get; set; }
        public int AccountLevelId { get; set; }
    }
}