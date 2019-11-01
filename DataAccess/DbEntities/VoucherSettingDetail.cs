using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class VoucherSettingDetail : BaseEntity
    {
        [Key, Column(Order = 1)]
        public int VoucherSettingID { get; set; }
        public int? CurrentVoucherYear { get; set; }
    }
}
