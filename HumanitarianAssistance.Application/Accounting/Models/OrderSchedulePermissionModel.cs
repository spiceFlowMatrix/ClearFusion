namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class OrderSchedulePermissionModel
    {
        public long Id { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public bool OrderSchedule { get; set; }
        public string RoleId { get; set; }
    }
}