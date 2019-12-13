using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditVehicleDetailCommand: BaseModel, IRequest<bool>
    {
        public long VehicleId { get; set; }
        public string PlateNo { get; set; }
        public int EmployeeId { get; set; }
        public int StartingMileage { get; set; }
        public int IncurredMileage { get; set; }
        public int FuelConsumptionRate { get; set; }
        public int MobilOilConsumptionRate { get; set; }
        public int ModelYear { get; set; }
        public int OfficeId { get; set; }
        public string ChasisNo { get; set; }
        public string RegistrationNo { get; set; }
        public string Remarks { get; set; }
        public string EngineNo { get; set; }
        public string ManufacturerCountry { get; set; }
    }
}