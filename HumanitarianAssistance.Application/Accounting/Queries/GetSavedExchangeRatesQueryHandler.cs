using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetSavedExchangeRatesQueryHandler: IRequestHandler<GetSavedExchangeRatesQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        
        public GetSavedExchangeRatesQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetSavedExchangeRatesQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            int skipPages = request.PageSize * request.PageIndex;

            try
            {
                response.data.ExchangeRateVerificationList = new List<ExchangeRateVerificationModel>();

                var recordList = await _dbContext.ExchangeRateVerifications.Where(x => x.IsDeleted == false).ToListAsync();

                var ExchangeRateVerificationList = _dbContext.ExchangeRateVerifications.Where(x => x.IsDeleted == false);

                if (request.FromDate != null)
                {
                    recordList = recordList.Where(x => x.Date.Date >= request.FromDate.Value.Date).ToList();
                    ExchangeRateVerificationList = ExchangeRateVerificationList.Where(x => x.Date.Date >= request.FromDate.Value.Date);
                }
                if (request.ToDate != null)
                {
                    recordList = recordList.Where(x => x.Date.Date <= request.ToDate.Value.Date).ToList();;
                    ExchangeRateVerificationList = ExchangeRateVerificationList.Where(x => x.Date.Date <= request.ToDate.Value.Date);
                }
                if (request.IsVerified != null)
                {
                    recordList = recordList.Where(x => x.IsVerified == request.IsVerified.Value).ToList();;
                    ExchangeRateVerificationList = ExchangeRateVerificationList.Where(x => x.IsVerified== request.IsVerified.Value);
                }

                if (ExchangeRateVerificationList.Any())
                    {

                      ExchangeRateVerificationList = ExchangeRateVerificationList.OrderByDescending(x => x.Date).Skip(skipPages).Take(request.PageSize); ;

                        foreach (ExchangeRateVerification item in ExchangeRateVerificationList)
                        {
                            ExchangeRateVerificationModel exchangeRateVerificationViewModel = new ExchangeRateVerificationModel();
                            exchangeRateVerificationViewModel.ExRateVerificationId = item.ExRateVerificationId;
                            exchangeRateVerificationViewModel.Date = item.Date;
                            exchangeRateVerificationViewModel.IsVerified = item.IsVerified;

                            response.data.ExchangeRateVerificationList.Add(exchangeRateVerificationViewModel);
                        }

                    response.data.TotalExchangeRateCount = recordList.Count();
                }
                
                response.StatusCode = StaticResource.successStatusCode;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = "Something went wrong " + ex.Message;
            }

            return response;
        }
    }
}