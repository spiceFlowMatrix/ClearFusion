using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeResignationDetail:BaseEntity
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1)]
		public long EmployeeResignationId { get; set; }
		public DateTime ResignDate { get; set; }
		public bool IsIssueUnresolved { get; set; }
		public string CommentsIssues { get; set; }
		public int EmployeeID { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
	}
}
