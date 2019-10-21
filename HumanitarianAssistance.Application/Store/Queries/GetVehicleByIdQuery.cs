using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleByIdQuery: IRequest<VehicleModel>
    {
        public long VehicleId { get; set; }
    }
}