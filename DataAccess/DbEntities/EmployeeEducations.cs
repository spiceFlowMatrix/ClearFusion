using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeEducations:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int EmployeeEducationsId { get; set; }
		public DateTime? EducationFrom { get; set; }
		public DateTime? EducationTo { get; set; }
		public string FieldOfStudy { get; set; }
		public string Institute { get; set; }
		public string Degree { get; set; }
		public int? EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
	}
}
