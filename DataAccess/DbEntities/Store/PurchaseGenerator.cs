﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class PurchaseGenerator : BaseEntityWithoutId
    {
        [Key]
        public int GeneratorId { get; set; }
        [Required]
        public string Purchase { get; set; }

        public string GeneratorBrand { get; set; }
        public string GeneratorModel { get; set; }
        public string MakeYear { get; set; }
        public string SerialBarCode { get; set; }

        [ForeignKey("Purchase")]
        public StoreItemPurchase ItemPurchase { get; set; }
    }
}
