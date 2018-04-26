using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Models
{
    public class RegistrationModel
    {
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string EmployeeName { get; set; }

    public int UserType { get; set; }

    public string OfficeCode { get; set; }

    public int Status { get; set; }

    public string Password { get; set; }


  }
}
