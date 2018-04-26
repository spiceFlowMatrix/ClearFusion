using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class OfficePermissionDetail : BaseEntity
    {
        [Key, Column(Order = 1)]
        public int UserID { get; set; }
        [StringLength(5)]
        [Key, Column(Order = 2)]
        public string OfficeCode { get; set; }
    }
}
