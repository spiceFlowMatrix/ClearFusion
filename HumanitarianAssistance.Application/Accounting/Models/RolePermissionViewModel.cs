namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class RolePermissionViewModel
    {
        public int RolesPermissionId { get; set; }
        public string RoleId { get; set; }
        public bool IsGrant { get; set; }
        public string CurrentPermissionId { get; set; }
        public int? PageId { get; set; }
        public int ModuleId { get; set; }
        public bool View { get; set; }
        public bool Edit { get; set; }
        public string PageName { get; set; }
    }
}