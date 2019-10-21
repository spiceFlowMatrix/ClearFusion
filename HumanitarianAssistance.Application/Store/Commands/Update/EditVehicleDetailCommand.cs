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
    }
}