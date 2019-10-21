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
namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddFinancialYearDetailCommandHandler : IRequestHandler<AddFinancialYearDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddFinancialYearDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(AddFinancialYearDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<FinancialYearDetail> yearlist = await _dbContext.FinancialYearDetail.Where(x => x.IsDeleted == false).ToListAsync();
                if (yearlist != null)
                {
                    if (request.IsDefault == true)
                    {
                        foreach (var i in yearlist)
                        {
                            FinancialYearDetail existrecord = await _dbContext.FinancialYearDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.FinancialYearId == i.FinancialYearId);
                            existrecord.IsDefault = false;
                            existrecord.ModifiedById = request.ModifiedById;
                            existrecord.ModifiedDate = request.ModifiedDate;
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    FinancialYearDetail obj = new FinancialYearDetail
                    {
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        FinancialYearName = request.FinancialYearName,
                        Description = request.Description,
                        IsDefault = request.IsDefault,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow,
                    };
                    await _dbContext.FinancialYearDetail.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                }

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