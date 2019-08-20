using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{

    public class AddEditTransactionListCommandHandler : IRequestHandler<AddEditTransactionListCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IAccountingServices _iAccountingServices;

        public AddEditTransactionListCommandHandler(HumanitarianAssistanceDbContext dbContext, IAccountingServices iAccountingServices)
        {
            _dbContext = dbContext;
            _iAccountingServices = iAccountingServices;
        }

        public async Task<ApiResponse> Handle(AddEditTransactionListCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                await Task.Run(() =>
                {
                    // Common Function to Add/Update Transaction
                    bool transactionAddedFlag = _iAccountingServices.AddEditTransactionList(request);

                    if (!transactionAddedFlag)
                    {
                        throw new Exception(StaticResource.SomethingWentWrong);
                    }
                });

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