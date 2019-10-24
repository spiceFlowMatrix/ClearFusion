using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class StoreItemDetail : BaseEntity
    {
        [Key, Column(Order = 1)]
        [StringLength(20)]
        public string StoreItemCode { get; set; }
        [StringLength(10)]
        public string MasterCode { get; set; }
        public byte? StoreType { get; set; }
        [StringLength(300)]
        public string StoreDescription { get; set; }
    }
}
