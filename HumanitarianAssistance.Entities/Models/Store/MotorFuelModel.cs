using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class MotorFuelModel : BaseModel
    {
        public string FuelId { get; set; }
        public string OrderId { get; set; }
        public int Vehicle { get; set; }
        public int Generator { get; set; }

        public long FuelQuantity { get; set; }
    }
}
