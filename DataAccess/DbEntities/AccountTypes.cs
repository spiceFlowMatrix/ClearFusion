using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class AccountType
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int AccountTypeId { get; set; }
        [StringLength(100)]
        public string AccountTypeName { get; set; }
        public int? AccountCategory { get; set; }
		public int AccountNote { get; set; }
		public string BalanceType { get; set; }
		//public ChartAccountDetail ChartAccountDetail { get; set; }
	}
}
