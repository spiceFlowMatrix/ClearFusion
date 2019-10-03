using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
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
    public class AddItemOrderCommandHandler : IRequestHandler<AddItemOrderCommand, ApiResponse>
    {
        private IMapper _mapper;
        private HumanitarianAssistanceDbContext _dbContext;

        public AddItemOrderCommandHandler(IMapper mapper, HumanitarianAssistanceDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddItemOrderCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    StorePurchaseOrder obj = _mapper.Map<StorePurchaseOrder>(request);
                    obj.PurchaseId = request.Purchase;

                    obj.IsDeleted = false;

                    await _dbContext.StorePurchaseOrders.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.ProcurementModel = new ProcurmentSummaryModel();
                    response.data.ProcurementModel.ProcurementId = obj.OrderId;
                    EmployeeDetail employee = await _dbContext.EmployeeDetail
                                                              .FirstOrDefaultAsync(x=> !x.IsDeleted && 
                                                              x.EmployeeID == request.IssuedToEmployeeId);

                    if(employee != null)
                    {
                        response.data.ProcurementModel.EmployeeName= employee.EmployeeCode + "-" +employee.EmployeeName;
                    }

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model value inappropriate";
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
