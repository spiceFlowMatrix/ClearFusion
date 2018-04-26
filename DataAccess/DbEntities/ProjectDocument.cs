using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
	public partial class ProjectDocument : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProjectDocumentId { get; set; }
		[Required]
		public string DocumentName { get; set; }
		[Required]
		public DateTime DocumentDate { get; set; }
		[Required]
		public long ProjectId { get; set; }		
		[Required]
		public ProjectDetails ProjectDetail { get; set; }
		//[Required]
		//public long UploadedBy { get; set; }
		//[Required]
		//public int DocumentType { get; set; }
		[Required]
		public byte[] FilePath { get; set; }
        public string DocumentGUID { get; set; }
        public string Extension { get; set; }

    }
}
