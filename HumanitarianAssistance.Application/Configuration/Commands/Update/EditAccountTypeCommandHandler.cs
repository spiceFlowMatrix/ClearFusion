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
    public class EditAccountTypeCommandHandler: IRequestHandler<EditAccountTypeCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditAccountTypeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditAccountTypeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var accountTypeDetail = await _dbContext.AccountType.FirstOrDefaultAsync(x => x.AccountTypeId == request.AccountTypeId);

                bool sameAccountName = await _dbContext.AccountType.AnyAsync(x =>
                                                                                x.AccountTypeName.ToLower() == request.AccountTypeName.ToLower() &&
                                                                                x.AccountTypeId != request.AccountTypeId
                                                                                );
                if (!sameAccountName)
                {
                    accountTypeDetail.AccountTypeName = request.AccountTypeName;
                    _dbContext.AccountType.Update(accountTypeDetail);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.NameExist;
                }


            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        
    }
}