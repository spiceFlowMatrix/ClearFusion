using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class Advances: BaseEntityWithoutId
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column(Order = 1, TypeName = "serial")]
		public int AdvancesId { get; set; }
		public DateTime AdvanceDate { get; set; }
		public int EmployeeId { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }
		public string EmployeeCode { get; set; }
		public int CurrencyId { get; set; }
		public CurrencyDetails CurrencyDetails { get; set; }
		public string VoucherReferenceNo { get; set; }
		public string Description { get; set; }
		public string ModeOfReturn { get; set; }
		public string ApprovedBy { get; set; }
		public double RequestAmount { get; set; }
		public double AdvanceAmount { get; set; }
		public int OfficeId { get; set; }
		public bool IsApproved { get; set; }        // Is advance approved by manager or not (false means not approved)
		public bool IsDeducted { get; set; }        // Is advance amount deducted from next month salary when defining monthly salary payroll (false means not distributed)
	}
}
