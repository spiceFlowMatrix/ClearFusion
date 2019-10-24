using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class ModulePermissionDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int ModuleID { get; set; }
        [Key, Column(Order =2)]
        [ForeignKey("UserDetails")]
        public int? UserID { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public Boolean? IsViewPermission { get; set; }
        public Boolean? IsAddNewPermission { get; set; }
        public Boolean? IsEditPermission { get; set; }
        public Boolean? IsValidatePermissio { get; set; }
        public Boolean? IsInvalidatePermission { get; set; }
        public Boolean? IsExportToExcelPermission { get; set; }
        public Boolean IsDeletePermission { get; set; }
    }
}
