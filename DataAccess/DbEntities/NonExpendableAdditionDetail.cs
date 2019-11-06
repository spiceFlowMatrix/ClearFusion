using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class NonExpendableAdditionDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int StoreAdditionID { get; set; }
        [Key, ForeignKey("NonExpendableStoreItemDetail")]
        public long? StoreItemID { get; set; }
        public float? Addition { get; set; }
        public DateTime? AdditionDate { get; set; }
        public byte? EntryType { get; set; }
    }
}
