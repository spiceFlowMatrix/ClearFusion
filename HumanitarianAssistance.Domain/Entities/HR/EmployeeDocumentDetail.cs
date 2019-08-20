using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class EmployeeDocumentDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int DocumentID { get; set; }
        [StringLength(100)]
        public string DocumentName { get; set; }
        public DateTime? DocumentDate { get; set; }
	    public int? EmployeeID { get; set; }
		public EmployeeDetail EmployeeDetail { get; set; }

		public byte[] FilePath { get; set; }
        public string DocumentGUID { get; set; }
        public string Extension { get; set; }
		public string DocumentFilePath { get; set; }
		public int? DocumentType { get; set; }
	}
}
