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

                model = (from u in await _dbContext.StoreLogger.Where(x => x.IsDeleted == false).ToListAsync()
                        join usd in await _dbContext.UserDetails.Where(x=> x.IsDeleted == false).ToListAsync() on u.CreatedById equals usd.AspNetUserId
                        select new StoreLogsModel
                        {
                            EventType = u.EventType,
                            EventBy=  $"{usd.FirstName} {usd.LastName}",
                            EventOn= u.CreatedDate != null ? u.CreatedDate.Value.ToShortDateString() : "",
                            LogText= u.LogText,
                            PurchaseId= u.PurchaseId
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