using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class StoreMasterItemDetail : BaseEntity
    {
        [Key, Column(Order = 1)]
        [StringLength(10)]
        public string StoreNumber { get; set; }
        [Key, Column(Order = 2)]
        public byte StoreType { get; set; }
        [StringLength(10)]
        public string AccountCode { get; set; }
        [StringLength(100)]
        public string StoreDescription { get; set; }
    }
}
