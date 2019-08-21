using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class AnalyticalDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long AnalyticalId { get; set; }
        [StringLength(10)]
        public string MemoCode { get; set; }
        public byte MemoType { get; set; }
        [StringLength(10)]
        public string Program { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(10)]
        public string Job { get; set; }
        [StringLength(10)]
        public string Sector { get; set; }
        [StringLength(10)]
        public string Area { get; set; }
        [StringLength(10)]
        public string MDCode { get; set; }
        [StringLength(200)]
        public string MemoName { get; set; }
        public float? BLAmount { get; set; }
        [StringLength(5)]
        public string BLCurrCode { get; set; }
        [StringLength(10)]
        public string CostBook { get; set; }
        public byte Status { get; set; }
        [StringLength(50)]
        public string DonorCode { get; set; }
        public byte BLType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float? ReceivedAmount { get; set; }
        [StringLength(100)]
        public string Attachment { get; set; }
    }
}
