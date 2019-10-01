using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllInventoriesTypeQuery: IRequest<List<InventoryTypeModel>>
    {
        
    }
}