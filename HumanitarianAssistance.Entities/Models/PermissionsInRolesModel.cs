using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class PermissionsInRolesModel : BaseModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string PermissionId { get; set; }
        public string PermissionName { get; set; }
        public Boolean IsGrant { get; set; }
    }
}
