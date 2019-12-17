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


namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditLogisticSupplierCommandHandler : IRequestHandler<EditLogisticSupplierCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditLogisticSupplierCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditLogisticSupplierCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var supplier = await _dbContext.ProjectLogisticSuppliers
                                                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.SupplierId == request.SupplierId);

                if (supplier == null)
                {
                    throw new Exception("Supplier doesnot exists!");
                }
                // supplier.SupplierName = request.SupplierName;
                supplier.Quantity = request.Quantity;
                // supplier.FinalPrice = request.FinalPrice;

                supplier.ModifiedDate = request.ModifiedDate;
                supplier.ModifiedById = request.ModifiedById;

                await _dbContext.SaveChangesAsync();

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
