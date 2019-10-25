using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class PurchaseVehicleModel
    {
        public string PurchaseId { get; set; }
        public string VehicleDescription { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleMakeYear { get; set; }
        public string VehiclePlateNo { get; set; }
        public string VehicleSerialNo { get; set; }

        public string VehicleImageName { get; set; }
        public string VehicleImageFileType { get; set; }
    }
}
