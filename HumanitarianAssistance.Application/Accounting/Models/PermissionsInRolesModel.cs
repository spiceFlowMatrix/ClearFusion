using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class PermissionsInRolesModel : BaseModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string PermissionId { get; set; }
        public string PermissionName { get; set; }
        public bool IsGrant { get; set; }

    }
}