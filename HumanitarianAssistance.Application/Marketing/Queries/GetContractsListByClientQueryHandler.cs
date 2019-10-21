using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetContractsListByClientQueryHandler : IRequestHandler<GetContractsListByClientQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetContractsListByClientQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetContractsListByClientQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            List<ContractByClient> modelList = new List<ContractByClient>();

            try
            {
                var list = await _dbContext.ContractDetails.AsNoTracking().AsQueryable().Where(x => !x.IsDeleted && x.IsApproved == true).ToListAsync();
                foreach (var item in list)
                {
                    ContractByClient model = new ContractByClient();
                    model.ContractByClients = item.ClientName + "" + "-" + item.ContractId;
                    model.ContractId = item.ContractId;
                    model.ClientName = item.ClientName;
                    model.ClientId = item.ClientId;
                    model.UnitRate = item.UnitRate;
                    model.CurrencyId = item.CurrencyId;
                    modelList.Add(model);
                }

                response.data.ContractByClientList = modelList;
                response.StatusCode = 200;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
