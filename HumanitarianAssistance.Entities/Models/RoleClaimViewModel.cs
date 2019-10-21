using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class RoleClaimViewModel
    {
        public string RoleName { get; set; }

        public List<Claims> ClaimValues { get; set; }
    }


    public class DeleteRoleClaimViewModel
    {
        public string Id { get; set; }
        public List<Claims> ClaimValues { get; set; }
    }

    public class Claims
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
