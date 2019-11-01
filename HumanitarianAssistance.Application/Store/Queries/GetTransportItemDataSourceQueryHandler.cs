using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetTransportItemDataSourceQueryHandler : IRequestHandler<GetTransportItemDataSourceQuery, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetTransportItemDataSourceQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetTransportItemDataSourceQuery request, CancellationToken cancellationToken)
        {
            List<TransportItemDataSourceModel> model = new List<TransportItemDataSourceModel>();

            try
            {
                if (request.TransportItemTypeId == (int)TransportItemTypes.Vehicle)
                {
                    List<PurchasedVehicleDetail> purchasedVehicles = new List<PurchasedVehicleDetail>();
                    purchasedVehicles = await _dbContext.PurchasedVehicleDetail.Where(x => x.IsDeleted == false).ToListAsync();

                    foreach (var item in purchasedVehicles)
                    {
                        TransportItemDataSourceModel tItem = new TransportItemDataSourceModel
                        {
                            PurchaseIdName = $"{item.PlateNo}-{item.PurchaseId}",
                            ItemId = item.Id
                        };
                        model.Add(tItem);
                    }
                }
                else
                {
                    List<PurchasedGeneratorDetail> purchasedGenerators = new List<PurchasedGeneratorDetail>();
                    purchasedGenerators = await _dbContext.PurchasedGeneratorDetail.Where(x => x.IsDeleted == false).ToListAsync();

                    foreach (var item in purchasedGenerators)
                    {
                        TransportItemDataSourceModel tItem = new TransportItemDataSourceModel
                        {
                            PurchaseIdName = $"{item.Voltage}-{item.PurchaseId}",
                            ItemId = item.Id
                        };

                        model.Add(tItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return model;
        }
    }
}