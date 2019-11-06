using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class PermissionsModel :BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Id { get; set; }


        

    }

    public class UserRolePermissionsModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolePermissionModel> RolePagePermission { get; set; }

    }

    public class RolePermissionModel
    {
        public int RolesPermissionId { get; set; }
        public int? PageId { get; set; }
        public int ModuleId { get; set; }
        public Boolean CanView { get; set; }
        public Boolean CanEdit { get; set; }
    }
}
