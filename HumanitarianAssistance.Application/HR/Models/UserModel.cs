using System.Collections.Generic;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class UserModel: BaseModel
    {

        public UserModel()
        {
            OfficeId = new List<int>();
        }
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
        public int EmployeeId { get; set; }
        public List<int> OfficeId { get; set; }

        public List<UserOfficesModel> UserOfficesList { get; set; }
    }
}