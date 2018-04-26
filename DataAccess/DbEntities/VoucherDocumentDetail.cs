using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class VoucherDocumentDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int DocumentID { get; set; }
        [StringLength(100)]
        public string DocumentName { get; set; }
        //[StringLength(200)]
        //public byte[] DocumentFilePath { get; set; }
        public byte[] FilePath { get; set; }
        public DateTime? DocumentDate { get; set; }
        public VoucherDetail VoucherDetails { get; set; }
        public long VoucherNo { get; set; }
		public string Extension { get; set; }
		public string DocumentGUID { get; set; }
		public int? DocumentType { get; set; }
	}
}
