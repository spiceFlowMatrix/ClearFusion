using System.ComponentModel.DataAnnotations;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PurchaseUnitType : BaseEntity
    {
        [Key]
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
    }
}
