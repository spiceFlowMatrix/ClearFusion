using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class ItemSpecificationDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int ItemSpecificationDetailsId { get; set; }
        public int ItemSpecificationMasterId { get; set; }
        public ItemSpecificationMaster ItemSpecificationMaster { get; set; }
        public string ItemId { get; set; }
        public string ItemSpecificationValue { get; set; }
    }
}
