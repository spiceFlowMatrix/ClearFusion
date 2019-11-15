using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleGeneratorTrackerLogsQueryHandler : IRequestHandler<GetVehicleGeneratorTrackerLogsQuery, List<StoreLogsModel>>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetVehicleGeneratorTrackerLogsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<List<StoreLogsModel>> Handle(GetVehicleGeneratorTrackerLogsQuery request, CancellationToken cancellationToken)
        {
            List<StoreLogsModel> model = new List<StoreLogsModel>();

            try
            {
                var item = await _dbContext.StoreLogger.Where(x => x.IsDeleted == false).ToListAsync();

                model = (from u in await _dbContext.StoreLogger.Where(x => x.IsDeleted == false && x.TransportType == request.TransportType && x.TransportTypeEntityId == request.EntityId).ToListAsync()
                        join usd in await _dbContext.UserDetails.Where(x=> x.IsDeleted == false).ToListAsync() on u.CreatedById equals usd.AspNetUserId into l
                        from z in l.DefaultIfEmpty()
                        join p in await _dbContext.StoreItemPurchases.Where(x=> x.IsDeleted == false).ToListAsync() on u.PurchaseId equals p.PurchaseId into pu
                        from c in pu.DefaultIfEmpty()
                        select new StoreLogsModel
                        {
                            EventType = u.EventType,
                            EventBy=  z != null? $"{z.FirstName} {z.LastName}": "",
                            EventOn= u.CreatedDate != null ? u.CreatedDate.Value.ToShortDateString() : "",
                            LogText= u.LogText,
                            PurchaseId= u.PurchaseId,
                            PurchaseName= (c != null? $"{c.PurchaseName}-{c.PurchaseDate.ToShortDateString()}-{c.PurchaseId}" : "")
                        }).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return model;
        }
    }
}