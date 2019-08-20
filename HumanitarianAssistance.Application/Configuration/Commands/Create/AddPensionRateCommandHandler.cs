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

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddPensionRateCommandHandler : IRequestHandler<AddPensionRateCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddPensionRateCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddPensionRateCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request != null)
                {
                    // check for financial year is present or not
                    if (!(await _dbContext.FinancialYearDetail.AnyAsync(x => x.FinancialYearId == request.FinancialYearId)))
                    {
                        throw new Exception (StaticResource.FinancialYearNotFound);
                    }

                    // check if pension rate is already assigned to fianacial year
                    if (!(await _dbContext.EmployeePensionRate.AnyAsync(x => x.FinancialYearId == request.FinancialYearId)))
                    {
                        EmployeePensionRate obj = new EmployeePensionRate();

                        // check if default pension rate is already saved or not
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

                        obj.FinancialYearId = request.FinancialYearId;
                        obj.PensionRate = request.PensionRate;
                        obj.IsDefault = (request.IsDefault == false && lst == null) ? true : request.IsDefault;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = DateTime.UtcNow;
                        obj.IsDeleted = false;

                        await _dbContext.EmployeePensionRate.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = 700;
                        response.Message = StaticResource.FinancialYearAlreadyExists;
                    }
                }
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