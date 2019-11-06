using System.Collections.Generic;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleMonthlyBreakdownDataByIdQuery: IRequest<List<VehicleMonthlyBreakdownDataModel>>
    {
        public int SelectedYear { get; set; }
        public long VehicleId { get; set; }
    }
}