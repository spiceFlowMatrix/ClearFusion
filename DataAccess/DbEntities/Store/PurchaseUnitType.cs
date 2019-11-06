using System.ComponentModel.DataAnnotations;

namespace DataAccess.DbEntities.Store
{
    public class PurchaseUnitType : BaseEntityWithoutId
    {
        [Key]
        public int UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
    }
}
