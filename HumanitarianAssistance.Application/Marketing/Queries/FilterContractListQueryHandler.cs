using HumanitarianAssistance.Application.Infrastructure;
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
    public class FilterContractListQueryHandler : IRequestHandler<FilterContractListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public FilterContractListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(FilterContractListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var contractList = await _dbContext.ContractDetails.Where(x => x.IsDeleted == false).ToListAsync();
                if (request != null)
                {
                    if (request.ActivityTypeId != 0 && request.ActivityTypeId != null)
                    {
                        contractList = contractList.Where(x => x.ActivityTypeId == request.ActivityTypeId).ToList();
                    }
                    if (!string.IsNullOrEmpty(request.ClientName))
                    {
                        contractList = contractList.Where(x => x.ClientName == request.ClientName).ToList();
                    }
                    if (request.ContractId != 0 && request.ContractId != null)
                    {
                        contractList = contractList.Where(x => x.ContractId == request.ContractId).ToList();
                    }
                    if (request.CurrencyId != 0 && request.CurrencyId != null)
                    {
                        contractList = contractList.Where(x => x.CurrencyId == request.CurrencyId).ToList();
                    }
                    if (request.IsApproved == true)
                    {
                        contractList = contractList.Where(x => x.IsApproved == Convert.ToBoolean(request.IsApproved)).ToList();
                    }
                    if (request.YesOrNo == "No")
                    {
                        contractList = contractList.Where(x => x.IsDeclined == true).ToList();
                    }
                    if (!string.IsNullOrEmpty(request.FilterType))
                    {
                        if (request.FilterType == "Equals")
                        {
                            if (request.UnitRate != 0 && request.UnitRate != null)
                            {
                                contractList = contractList.Where(x => x.UnitRate == request.UnitRate).ToList();
                            }
                        }
                        if (request.FilterType == "Greater Than")
                        {
                            if (request.UnitRate != 0 && request.UnitRate != null)
                            {
                                contractList = contractList.Where(x => x.UnitRate > request.UnitRate).ToList();
                            }
                        }
                        if (request.FilterType == "Less Than")
                        {
                            if (request.UnitRate != 0 && request.UnitRate != null)
                            {
                                contractList = contractList.Where(x => x.UnitRate < request.UnitRate).ToList();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(request.FilterType))
                    {
                        if (request.UnitRate != 0 && request.UnitRate != null)
                        {
                            contractList = contractList.Where(x => x.UnitRate == request.UnitRate).ToList();
                        }

                    }
                    response.data.ContractDetails = contractList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Entries Found.Try Different Filters";
                }

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
