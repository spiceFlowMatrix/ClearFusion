using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class RoleViewModel
    {
        public string RoleName { get; set; }
    }

    public class EditRoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }

    public class EditRoleModel
    {
        public string RoleId { get; set; }
    }
}
