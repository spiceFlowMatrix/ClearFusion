using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class ProjectActivityExtensions : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ExtensionId { get; set; }

        public long ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public ProjectActivityDetail ProjectActivityDetail { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
