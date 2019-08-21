using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeOtherSkills:BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public int EmployeeOtherSkillsId { get; set; }
		public string TypeOfSkill { get; set; }
		public string AbilityLevel { get; set; }
		public string Experience { get; set; }
		public string Remarks { get; set; }
		public int EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
	}
}
