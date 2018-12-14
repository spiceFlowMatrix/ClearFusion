using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class RolesWithPagePermissionsModel
    {
        public string RoleName { get; set; }
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
}
