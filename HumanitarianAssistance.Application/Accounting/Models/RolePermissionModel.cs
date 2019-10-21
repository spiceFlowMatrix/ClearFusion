namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class RolePermissionModel
    {
        public int RolesPermissionId { get; set; }
        public int? PageId { get; set; }
        public int ModuleId { get; set; }
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
    }
}