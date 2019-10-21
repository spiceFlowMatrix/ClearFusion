using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditCurrencyCommandHandler : IRequestHandler<EditCurrencyCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public EditCurrencyCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditCurrencyCommand request, CancellationToken cancellationToken)
        {

            ApiResponse response = new ApiResponse();
            try
            {
                var currencyInfo = await _dbContext.CurrencyDetails.FirstOrDefaultAsync(c => c.CurrencyId == request.CurrencyId);

                if (currencyInfo == null)
                {
                    throw new Exception(StaticResource.CurrencyNotFound);
                }

                currencyInfo.CurrencyCode = request.CurrencyCode;
                currencyInfo.CurrencyName = request.CurrencyName;
                currencyInfo.ModifiedById = request.ModifiedById;
                currencyInfo.ModifiedDate = request.ModifiedDate;

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