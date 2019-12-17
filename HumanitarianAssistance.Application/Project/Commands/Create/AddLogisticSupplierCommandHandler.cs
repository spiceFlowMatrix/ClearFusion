using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticSupplierCommandHandler : IRequestHandler<AddLogisticSupplierCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddLogisticSupplierCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddLogisticSupplierCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProjectLogisticSuppliers obj = new ProjectLogisticSuppliers
                {
                    LogisticRequestsId = request.RequestId,
                    // SupplierName = request.SupplierName,
                    Quantity = request.Quantity,
                   // FinalPrice = request.FinalCost,
                    CreatedById = request.CreatedById,
                    CreatedDate = request.CreatedDate
                };

                await _dbContext.ProjectLogisticSuppliers.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

                response.CommonId.LongId = obj.SupplierId;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
