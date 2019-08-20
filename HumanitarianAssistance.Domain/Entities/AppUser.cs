using Microsoft.AspNetCore.Identity;
using System;

namespace HumanitarianAssistance.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
