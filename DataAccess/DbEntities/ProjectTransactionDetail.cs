//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace DataAccess.DbEntities
//{
//    public partial class ProjectTransactionDetail : BaseEntity
//    {
//        [StringLength(10)]
//        public string ProjectCode { get; set; }
//        [StringLength(200)]
//        public string ProjectDescription { get; set; }
//        [StringLength(10)]
//        public string OfficeCode { get; set; }
//        [StringLength(200)]
//        public string Sector { get; set; }
//        public DateTime? StartDate { get; set; }
//        public DateTime? EndDate { get; set; }
//        [StringLength(10)]
//        public string BLCurrencyCode { get; set; }
//        [StringLength(10)]
//        public string Status { get; set; }
//        public float? ReceivedAmount { get; set; }
//        public float? Budget { get; set; }
//        public float? AFGBudget { get; set; }
//        public float? USDBudget { get; set; }
//        public float? EURBudget { get; set; }
//        public float? AFGReceivedAmount { get; set; }
//        public float? USDReceivedAmount { get; set; }
//        public float? EURReceivedAmount { get; set; }
//        public float? AFGExpenditure { get; set; }
//        public float? USDExpenditure { get; set; }
//        public float? EURExpenditure { get; set; }
//    }
//}
