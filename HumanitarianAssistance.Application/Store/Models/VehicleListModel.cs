using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{

    public class VehicleTrackerListModel 
    {
        public VehicleTrackerListModel()
        {
            VehicleList= new List<VehicleTrackerModel>();
        }
        public int TotalRecords {get; set;}
        public List<VehicleTrackerModel> VehicleList {get; set;}
    }
    public class VehicleTrackerModel 
    {
        public long VehicleId { get; set; }
        public string PlateNo {get; set;}
        public int EmployeeId {get; set;}
        public int OfficeId {get; set;}
        public string EmployeeName { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double TotalMileage { get; set; }
        public double TotalCost { get; set; }
        public double OriginalCost { get; set; }
    }
}