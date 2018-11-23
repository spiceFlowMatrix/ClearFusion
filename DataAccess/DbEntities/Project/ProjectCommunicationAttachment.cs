using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class ProjectCommunicationAttachment : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long PCAId { get; set; }
        public long PCId { get; set; }
        [ForeignKey("PCId")]
        public ProjectCommunication ProjectCommunication { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public string FileName  { get; set; }
        public string FilePath  { get; set; }

    }
}
