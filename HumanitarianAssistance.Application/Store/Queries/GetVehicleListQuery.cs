using System.Collections.Generic;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleListQuery: IRequest<VehicleTrackerListModel>
    {
        public double? TotalCost { get; set; }
        public string PlateNo { get; set; }
        public int? EmployeeId { get; set; }
        public int? OfficeId { get; set; }

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; } 
    }
}