using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeAppraisalDetails: BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int EmployeeAppraisalDetailsId { get; set; }
		public int EmployeeId { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
		public string EmployeeCode { get; set; }
		public string EmployeeName { get; set; }
		public string FatherName { get; set; }
		public string Position { get; set; }
		public string Department { get; set; }
		public string Qualification { get; set; }
		public string DutyStation { get; set; }
		public DateTime RecruitmentDate { get; set; }
		public int AppraisalPeriod { get; set; }
		public DateTime CurrentAppraisalDate { get; set; }		
	}
}
