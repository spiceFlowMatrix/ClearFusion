using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class Roles
    {
        public string RoleName { get; set; }
        public string Id { get; set; }
        public IList<PermissionsModel> PermissionsList { get; set; }
    }
}