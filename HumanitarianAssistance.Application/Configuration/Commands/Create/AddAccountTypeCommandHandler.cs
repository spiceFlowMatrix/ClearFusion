using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddAccountTypeCommandHandler : IRequestHandler<AddAccountTypeCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddAccountTypeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddAccountTypeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                bool sameAccount = await _dbContext.AccountType.AnyAsync(x => x.AccountTypeName.ToLower() == request.AccountTypeName.ToLower());
                
                if (!sameAccount)
                {
                    AccountType obj = new AccountType();

                    obj.AccountCategory = request.AccountCategory;
                    obj.AccountHeadTypeId = request.AccountHeadTypeId.Value;
                    obj.AccountTypeName = request.AccountTypeName;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;

                    await _dbContext.AccountType.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    response.CommonId.Id = obj.AccountTypeId;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
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