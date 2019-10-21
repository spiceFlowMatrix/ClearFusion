using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditPensionRateCommandHandler : IRequestHandler<EditPensionRateCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditPensionRateCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditPensionRateCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var lst = await _dbContext.EmployeePensionRate.FirstOrDefaultAsync(x => x.IsDefault == true);

                if (request.IsDefault == true)
                {
                    if (lst != null)
                    {
                        lst.IsDefault = false;
                        _dbContext.EmployeePensionRate.Update(lst);
                        await _dbContext.SaveChangesAsync();
                    }

                }
                if (request.IsDefault == false)
                {
                    if (lst == null)
                        request.IsDefault = true;
                    if (lst.IsDefault == true && lst.FinancialYearId == request.FinancialYearId)
                        request.IsDefault = true;
                    else
                        request.IsDefault = false;
                }

                EmployeePensionRate obj = await _dbContext.EmployeePensionRate.FirstOrDefaultAsync(x => x.FinancialYearId == request.FinancialYearId);
                
                obj.FinancialYearId = request.FinancialYearId;
                obj.PensionRate = request.PensionRate;
                obj.IsDefault = (request.IsDefault == false && lst == null) ? true : request.IsDefault;
                obj.IsDefault = true;
                obj.ModifiedById = request.ModifiedById;
                obj.ModifiedDate = DateTime.UtcNow;
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