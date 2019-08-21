using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditItemOrderCommandHandler : IRequestHandler<EditItemOrderCommand, ApiResponse>
    {
        private IMapper _mapper;
        private HumanitarianAssistanceDbContext _dbContext;
        public EditItemOrderCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditItemOrderCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var recordExits = await _dbContext.StorePurchaseOrders.FirstOrDefaultAsync(x => x.OrderId == request.OrderId && x.IsDeleted == false);
                    _mapper.Map(request, recordExits);

                    recordExits.IsDeleted = false;

                    await _dbContext.SaveChangesAsync();
                    //await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record cannot be updated";
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
