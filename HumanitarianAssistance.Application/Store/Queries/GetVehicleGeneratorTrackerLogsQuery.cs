using System.Collections.Generic;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleGeneratorTrackerLogsQuery: IRequest<List<StoreLogsModel>>
    {
        
    }
}