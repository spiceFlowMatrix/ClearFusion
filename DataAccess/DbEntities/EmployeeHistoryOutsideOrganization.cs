using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeHistoryOutsideOrganization:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int EmployeeHistoryOutsideOrganizationId { get; set; }
		public DateTime EmploymentFrom { get; set; }
		public DateTime EmploymentTo { get; set; }
		public string Organization { get; set; }
		public double MonthlySalary { get; set; }
		public string ReasonForLeaving { get; set; }		
		public int EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
	}
}
