using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class InventoryItemType : BaseEntity
    {
        [Key]
        public int ItemType { get; set; }
        public string TypeName { get; set; }
    }
}
