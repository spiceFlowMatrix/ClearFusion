using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditFinancialYearDetailCommandHandler : IRequestHandler<EditFinancialYearDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public EditFinancialYearDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(EditFinancialYearDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                FinancialYearDetail existrecord = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.FinancialYearId == request.FinancialYearId);
                if (existrecord != null)
                {
                    if (request.IsDefault == true)
                    {
                        List<FinancialYearDetail> yearlist = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted == false).ToListAsync();
                        foreach (var i in yearlist)
                        {
                            var existrecord1 = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.FinancialYearId == i.FinancialYearId);
                            existrecord1.IsDefault = false;
                            existrecord1.ModifiedById = request.ModifiedById;
                            existrecord1.ModifiedDate = request.ModifiedDate;
                            await _dbContext.SaveChangesAsync();
                        }
                    }

                    existrecord.FinancialYearName = request.FinancialYearName;
                    existrecord.StartDate = request.StartDate;
                    existrecord.EndDate = request.EndDate;
                    existrecord.Description = request.Description;
                    existrecord.IsDefault = request.IsDefault;
                    existrecord.ModifiedById = request.ModifiedById;
                    existrecord.ModifiedDate = request.ModifiedDate;
                    existrecord.IsDeleted = request.IsDeleted;

                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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