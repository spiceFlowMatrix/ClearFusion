namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class ApproveRejectPermissionModel
    {
        public long Id { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public bool Approve { get; set; }
        public bool Reject { get; set; }
        public string RoleId { get; set; }
    }
}