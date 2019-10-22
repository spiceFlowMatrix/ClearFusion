using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddVehicleMileageCommand: BaseModel, IRequest<bool>
    {
        public long VehicleId { get; set; }
        public int Mileage { get; set; }
        public DateTime Month {get; set;}
    }
}