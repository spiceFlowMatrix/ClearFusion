using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteEmployeeContractCommandHandler: IRequestHandler<DeleteEmployeeContractCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public DeleteEmployeeContractCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteEmployeeContractCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                EmployeeContract employeeContract = await _dbContext.EmployeeContract.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeContractId == request.EmployeeContractId);

                employeeContract.IsDeleted = true;
                employeeContract.ModifiedById = request.ModifiedById;

                _dbContext.EmployeeContract.Update(employeeContract);
                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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