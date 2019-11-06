//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace DataAccess.DbEntities
//{
//    public partial class BudgetLineEmployees : BaseEntityWithoutId
//    {
//		[Key]
//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[Column(Order = 1, TypeName = "serial")]
//		public int BudgetLineEmployeesId { get; set; }
//		public int OfficeId { get; set; }
//		public int ProjectId { get; set; }		
//		public long BudgetLineId { get; set; }
//		public ProjectBudgetLine ProjectBudgetLine { get; set; }
//		public int EmployeeId { get; set; }
//		public EmployeeDetail EmployeeDetail { get; set; }
//		public string EmployeeName { get; set; }
//		public bool IsActive { get; set; }
//		public DateTime? StartDate { get; set; }
//		public DateTime? EndDate { get; set; }
//		public double? ProjectPercentage { get; set; }
//	}
//}
