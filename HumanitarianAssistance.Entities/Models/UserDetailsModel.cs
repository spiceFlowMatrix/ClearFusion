using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
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
		public int? OfficeId { get; set; }
		public List<UserOfficesModel> UserOfficesModelList { get; set; }
	}

    public class ChangePasswordModel : BaseModel
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string AspNetUserId { get; set; }
    }

    public class ResetPassword : BaseModel
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string AspNetUserId { get; set; }
    }
}
