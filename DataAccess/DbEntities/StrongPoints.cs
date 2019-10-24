using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class StrongandWeakPoints: BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int StrongPointsId { get; set; }
		public DateTime CurrentAppraisalDate { get; set; }
		public int EmployeeId { get; set; }
        public int EmployeeAppraisalDetailsId { get; set; }

        public string Point { get; set; }
		public int Status { get; set; }					// 1 for strong and 2 for weak (Managed at backend)

	}
}
