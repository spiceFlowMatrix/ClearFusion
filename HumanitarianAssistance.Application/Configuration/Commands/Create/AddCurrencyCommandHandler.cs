using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddCurrencyCommandHandler : IRequestHandler<AddCurrencyCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddCurrencyCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (!(await _dbContext.CurrencyDetails.AnyAsync(c => c.CurrencyCode == request.CurrencyCode)))
                {
                    CurrencyDetails obj = new CurrencyDetails
                    {
                        CurrencyCode = request.CurrencyCode,
                        CurrencyName = request.CurrencyName,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    await _dbContext.CurrencyDetails.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
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