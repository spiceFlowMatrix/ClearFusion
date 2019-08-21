using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class UserRolePermissionsModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolePermissionModel> RolePagePermission { get; set; }
    }
}