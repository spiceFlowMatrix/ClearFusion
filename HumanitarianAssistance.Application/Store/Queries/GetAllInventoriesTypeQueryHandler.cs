using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllInventoriesTypeQueryHandler : IRequestHandler<GetAllInventoriesTypeQuery, List<InventoryTypeModel>>
    {
        public async Task<List<InventoryTypeModel>> Handle(GetAllInventoriesTypeQuery request, CancellationToken cancellationToken)
        {
            List<InventoryTypeModel> model;

            try
            {
                model= new List<InventoryTypeModel>
                {
                    new InventoryTypeModel { Id= 1, InventoryName="Consumables" },
                    new InventoryTypeModel { Id= 2, InventoryName="Expendables" },
                    new InventoryTypeModel { Id= 3, InventoryName="Non-Expendables" }
                };
            }
            catch(Exception exception)
            {
                throw exception;
            }

            return model;
        }
    }
}