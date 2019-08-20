using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class DocumentFileDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DocumentFileId { get; set; }
        public int ModuleId { get; set; }
        public int PageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RawFileMimeType { get; set; }
        public long RawFileSizeBytes { get; set; }
        public string StorageDirectoryPath { get; set; }
        public bool? Active { get; set; }
    }
}
