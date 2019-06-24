using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Models
{
  public class UserDetails : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string EmployeeName { get; set; }

    public int UserType { get; set; }

    public string OfficeCode { get; set; }

    public int Status { get; set; }

  }
}
