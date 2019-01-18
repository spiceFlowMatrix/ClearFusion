﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class ApproveRejectPermission : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long Id { get; set; }

        public int PageId { get; set; }
        public bool Approve { get; set; }
        public bool Reject { get; set; }
        [ForeignKey("AspNetUserRoles")]
        public string RoleId { get; set; }
        [ForeignKey("PageId")]
        public ApplicationPages ApplicationPages { get; set; }

    }
}
