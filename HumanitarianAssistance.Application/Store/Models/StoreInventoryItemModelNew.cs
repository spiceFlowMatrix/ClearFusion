﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
   public class StoreInventoryItemModelNew
    {
        public long Id { get; set; }
        public long InventoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long ItemGroupId { get; set; }
        public int? ItemType { get; set; }
        public int? ItemTypeCategory { get; set; }
        public int? DefaultUnitType { get; set; }

    }
}
