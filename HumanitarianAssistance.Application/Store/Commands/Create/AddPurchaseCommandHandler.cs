﻿using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddPurchaseCommandHandler : IRequestHandler<AddPurchaseCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddPurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddPurchaseCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            request.PurchaseDate = request.TimezoneOffset == null? request.PurchaseDate : request.PurchaseDate.AddMinutes(Math.Abs((double)request.TimezoneOffset));

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
            try
            {
                if (request != null)
                {
                    var exRate = await _dbContext.ExchangeRateDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Date.Date == request.PurchaseDate.Date && x.FromCurrency == request.Currency && x.ToCurrency == (int)Currency.USD);

                    if (exRate == null)
                    {
                        throw new Exception($"Exchange Rates not defined for Date {request.PurchaseDate.Date.ToString("dd/MM/yyyy")}");
                    }

                    StoreItemPurchase purchase = _mapper.Map<StoreItemPurchase>(request);

                    purchase.IsDeleted = false;
                    purchase.CreatedById = request.CreatedById;
                    purchase.CreatedDate = request.CreatedDate;
                    purchase.SerialNo = request.PurchaseOrderNo.ToString();

                    //Add Purchase
                    await _dbContext.StoreItemPurchases.AddAsync(purchase);
                    await _dbContext.SaveChangesAsync();

                    response.ResponseData = new {PurchaseId = purchase.PurchaseId };
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                    tran.Commit();
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "request values are inappropriate";
                    return response;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }
            }
            

            return response;
        }
    }
}
