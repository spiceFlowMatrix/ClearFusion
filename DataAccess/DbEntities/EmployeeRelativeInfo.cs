using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeRelativeInfo:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int EmployeeRelativeInfoId { get; set; }
		public string Name { get; set; }
		public string Relationship { get; set; }
		public string Position { get; set; }
		public string Organization { get; set; }
		public int EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
	}
}
