using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditStoreSourceCodeCommandHandler : IRequestHandler<EditStoreSourceCodeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditStoreSourceCodeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditStoreSourceCodeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //Retrieving the Old storeSourceCode record from  db
                StoreSourceCodeDetail storeSourceCodeDetail = await _dbContext.StoreSourceCodeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.SourceCodeId == request.SourceCodeId);

                //Mapping new values in old storeSourceCode record
                storeSourceCodeDetail.Address = request.Address;
                storeSourceCodeDetail.Code = request.Code;
                storeSourceCodeDetail.CodeTypeId = request.CodeTypeId;
                storeSourceCodeDetail.Description = request.Description;
                storeSourceCodeDetail.EmailAddress = request.EmailAddress;
                storeSourceCodeDetail.Fax = request.Fax;
                storeSourceCodeDetail.Guarantor = request.Guarantor;
                storeSourceCodeDetail.Phone = request.Phone;
                storeSourceCodeDetail.IsDeleted = false;
                storeSourceCodeDetail.ModifiedDate = request.ModifiedDate;
                storeSourceCodeDetail.ModifiedById = request.ModifiedById;

                //saving newly mapped object
                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
