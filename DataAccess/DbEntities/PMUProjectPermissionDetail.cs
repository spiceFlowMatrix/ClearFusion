using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PMUProjectPermissionDetail : BaseEntity
    {
        [Key, Column(Order = 1)]
        public int PMUProjectID { get; set; }
        [Key, Column(Order = 2)]
        public int UserID { get; set; }
        public Boolean? IsViewPermission { get; set; }
        public Boolean? IsUploadPermission { get; set; }
        public Boolean? IsDownloadPermission { get; set; }
    }
}
