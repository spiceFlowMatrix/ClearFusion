using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class EmployeeOtherSkills:BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int EmployeeOtherSkillsId { get; set; }
		public string TypeOfSkill { get; set; }
		public string AbilityLevel { get; set; }
		public string Experience { get; set; }
		public string Remarks { get; set; }
		public int EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
	}
}
