using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PermissionsInRoles : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int PermissionsInRolesId { get; set; }
        [Key, ForeignKey("AspNetUserRoles")]
        public string RoleId { get; set; }
        [Key, ForeignKey("Permissions")]
        public string PermissionId { get; set; }
        public Boolean IsGrant { get; set; }
        public string CurrentPermissionId { get; set; }
    }
}
