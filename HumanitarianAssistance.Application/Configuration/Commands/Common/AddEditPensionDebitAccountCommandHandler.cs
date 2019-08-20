using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Common
{
    public class AddEditPensionDebitAccountCommandHandler : IRequestHandler<AddEditPensionDebitAccountCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public AddEditPensionDebitAccountCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddEditPensionDebitAccountCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            PensionDebitAccountMaster pensionDebitAccount;

            try
            {
                pensionDebitAccount = await _dbContext.PensionDebitAccountMaster.FirstOrDefaultAsync(x => x.IsDeleted == false);

                if (pensionDebitAccount == null)
                {
                    pensionDebitAccount = new PensionDebitAccountMaster();
                    pensionDebitAccount.ChartOfAccountNewId = request.accountId;
                    pensionDebitAccount.CreatedDate = request.CreatedDate;
                    pensionDebitAccount.CreatedById = request.CreatedById;
                    pensionDebitAccount.IsDeleted = false;

                    await _dbContext.PensionDebitAccountMaster.AddAsync(pensionDebitAccount);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    pensionDebitAccount.ModifiedById = request.ModifiedById;
                    pensionDebitAccount.ModifiedDate = request.ModifiedDate;
                    pensionDebitAccount.ChartOfAccountNewId = request.accountId;

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
