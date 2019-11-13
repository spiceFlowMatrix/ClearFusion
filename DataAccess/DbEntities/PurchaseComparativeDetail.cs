using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PurchaseComparativeDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int PurchaseComparativeID { get; set; }
        [StringLength(50)]
        public string ItemName { get; set; }
        [StringLength(100)]
        public string ItemDescription { get; set; }
        public float? Unit { get; set; }
        public float? Qty { get; set; }
        public DateTime? Date { get; set; }
        [StringLength(100)]
        public string Comment { get; set; }
        [StringLength(100)]
        public string PreparedBy { get; set; }
        public int? PurchaseRequestID { get; set; }
        public string ComparativeSuppliers { get; set; }
    }
}
