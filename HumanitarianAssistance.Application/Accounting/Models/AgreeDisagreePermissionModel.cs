namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class AgreeDisagreePermissionModel
    {
        public long Id { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public bool Agree { get; set; }
        public bool Disagree { get; set; }
        public string RoleId { get; set; }
    }
}