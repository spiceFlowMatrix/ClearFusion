using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddPurchaseUnitTypeCommandHandler : IRequestHandler<AddPurchaseUnitTypeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddPurchaseUnitTypeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddPurchaseUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    if(request.IsDefault)
                    {
                        PurchaseUnitType unitType= await _dbContext.PurchaseUnitType.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.IsDefault == true);

                        if(unitType != null)
                        {
                            unitType.IsDefault = false;
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    PurchaseUnitType obj = _mapper.Map<PurchaseUnitType>(request);
                    obj.IsDeleted = false;

                    await _dbContext.PurchaseUnitType.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Please add Unit type details";
                    return response;
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = e.Message;
                return response;
            }

            return response;
        }
    }
}
