using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeDetailModel : BaseModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public int? EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string EmployeeName { get; set; }
        public string IDCard { get; set; }
        public string FatherName { get; set; }
        //public int? OfficeId { get; set; }
        //public string OfficeName { get; set; }
        //public int? DepartmentId { get; set; }
        //public string DepartmentName { get; set; }
        public int? GradeId { get; set; }
        //public int? DesignationId { get; set; }
        //public string DesignationName { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ReferBy { get; set; }
        public string Passport { get; set; }
        public int? NationalityId { get; set; }
        public string NationalityName { get; set; }
        public string Language { get; set; }
        public int? SexId { get; set; }
        public string SexName { get; set; }
        public string DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? HigherQualificationId { get; set; }
        public string HigherQualificationName { get; set; }
        public int? ProfessionId { get; set; }
        public string ProfessionName { get; set; }
        public string PreviousWork { get; set; }
        public string Remarks { get; set; }
        public int? ExperienceYear { get; set; }
        public int? ExperienceMonth { get; set; }
        public string Resume { get; set; }
        public string EmployeePhoto { get; set; }
		public string DocumentGUID { get; set; }
		public int OfficeId { get; set; }

		public int MaritalStatus { get; set; }
		public string PassportNo { get; set; }
		public string University { get; set; }
		public string BirthPlace { get; set; }
		public string IssuePlace { get; set; }

		//public IList<EmployeeProfessionalDetailModel> EmployeeProfessionalList { get; set; }
		//public IList<EmployeeDocumentDetailModel> EmployeeDocumentDetailList { get; set; }
		//public IList<EmployeeHistoryDetailModel> EmployeeHistoryDetailList { get; set; }
		//public IList<AssignLeaveToEmployeeModel> AssignLeaveToEmployeeList { get; set; }
		//public IList<EmployeeHealthInformationModel> EmployeeHealthInfoList { get; set; }
	}

    public class EmployeeDetailsAllModel
    {
        public int? EmployeeTypeId { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePhoto { get; set; }
        public int? Age { get; set; }
        public string SexName { get; set; }
		public string DocumentGUID { get; set; }
        public string EmployeeDOB{ get; set; }

        public DateTime? HiredOn { get; set; }
		public string Email { get; set; }
		public string Profession { get; set; }
		public int? DesignationId { get; set; }
		public int? ExperienceYear { get; set; }
		public int? ExperienceMonth { get; set; }
		public int MaritalStatus { get; set; }
		public string PassportNo { get; set; }
		public string University { get; set; }
		public string BirthPlace { get; set; }
		public string IssuePlace { get; set; }

	}

    public class InterviewScheduleForProspectiveEmployeeModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNo { get; set; }

        public int? ProfessionId { get; set; }
        public string ProfessionName { get; set; }

        public string Resume { get; set; }
    }

    public class ChangeEmployeeImage : BaseModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeImage { get; set; }
    }
}
