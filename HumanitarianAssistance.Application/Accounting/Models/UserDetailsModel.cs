using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class UserDetailsModel : BaseModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte? UserType { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Id { get; set; }
        public byte? Status { get; set; }
        public string Phone { get; set; }
        public List<int> OfficeId { get; set; }
        public List<UserOfficesModel> UserOfficesList { get; set; }
    }
}