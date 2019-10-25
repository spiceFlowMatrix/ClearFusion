using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbEntities.Store
{
    public class InventoryItemType : BaseEntityWithoutId
    {
        [Key]
        public int ItemType { get; set; }
        public string TypeName { get; set; }
    }
}
