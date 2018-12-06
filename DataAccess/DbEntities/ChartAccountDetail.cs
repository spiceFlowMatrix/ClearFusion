//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace DataAccess.DbEntities
//{
//    public partial class ChartAccountDetail : BaseEntityWithoutId
//    {
     
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        [Column(Order = 1, TypeName = "serial")]
//        public int AccountCode { get; set; }
//        public long ChartOfAccountCode { get; set; }
//        [StringLength(100)]
//        public string AccountName { get; set; }
//        public AccountLevel AccountLevels { get; set; }
//        public int AccountLevelId { get; set; }
//		public AccountType AccountType { get; set; }
//		public int? AccountTypeId { get; set; }
//		public long ParentID { get; set; }
//		//[ForeignKey("ParentID")]
//		//public ChartAccountDetail CreditAccountDetails { get; set; }
//		public List<VoucherTransactions> CreditAccountlist { get; set; }
//		//public List<VoucherTransactionDetails> CreditAccountlist { get; set; }
//        public List<VoucherTransactions> DebitAccountlist { get; set; }
//        public List<VoucherDetail> VoucherList { get; set; }
        
//        public float DepRate { get; set; }
//        //[StringLength(50)]
//        public string DepMethod { get; set; }
//        public int? AccountNote { get; set; }
//        //[StringLength(50)]
//        public string MDCode { get; set; }
//        //public Boolean? Show { get; set; }
//		public bool Show { get; set; }
//	}
//}
