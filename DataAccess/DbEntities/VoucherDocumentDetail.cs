﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class VoucherDocumentDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long VoucherDocumentId { get; set; }
        [ForeignKey("VoucherNo")]
		public VoucherDetail VoucherDetails { get; set; }
		public long VoucherNo { get; set; }
        public long DocumentFileId { get; set; }
        [ForeignKey("DocumentFileId")]
        public DocumentFileDetail DocumentFileDetail { get; set; }
    }
}
