using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class RolesWithPagePermissionsModel
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public List<ApplicationPagesModel> Permissions { get; set; }
    }

    public class ApplicationPagesModel
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string ModuleName { get; set; }
        public int ModuleId { get; set; }
        public bool Edit { get; set; }
        public bool View { get; set; }
    }

    public class RolePermissionViewModel {
        public int RolesPermissionId { get; set; }
        public string RoleId { get; set; }
        public Boolean IsGrant { get; set; }
        public string CurrentPermissionId { get; set; }
        public int? PageId { get; set; }
        public int ModuleId { get; set; }
        public Boolean View { get; set; }
        public Boolean Edit { get; set; }
        public string PageName { get; set; }
    }
}
