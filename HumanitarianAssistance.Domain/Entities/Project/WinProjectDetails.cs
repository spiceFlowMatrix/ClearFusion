using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class WinProjectDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long WinProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        public string CommentText { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool? IsWin { get; set; }
        public byte[] UploadedFile { get; set; }
    }
}
