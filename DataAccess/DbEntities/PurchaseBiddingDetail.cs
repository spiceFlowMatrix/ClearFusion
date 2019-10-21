using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class PurchaseBiddingDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int PurchaseBiddingID { get; set; }
        [Key, ForeignKey("PurchaseRequestDetail")]
        public int? PurchaseRequestID { get; set; }
        [StringLength(100)]
        public string ReceivedOffersByName { get; set; }
        [StringLength(100)]
        public string ReceivedOffersByOwner { get; set; }
        [StringLength(200)]
        public string ReceivedOffersByAddress { get; set; }
        [StringLength(100)]
        public string ReceivedOffersByEmail { get; set; }
        [StringLength(20)]
        public string ReceivedOffersByPhone { get; set; }
        public float? BidSecurityAmount { get; set; }
        public DateTime? BiddingDate { get; set; }
        public DateTime? BidOpening { get; set; }
        public float? BEAdministrativeTechnicalScore { get; set; }
        public float? BEFinancialScore { get; set; }
        public float? BETotalMarks { get; set; }
        public Boolean? ResultQualified { get; set; }
        public Boolean? ResultNotQualified { get; set; }
        public string ResultGuaranteeLetters { get; set; }
        public byte? Status { get; set; }
    }
}
