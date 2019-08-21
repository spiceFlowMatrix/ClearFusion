using System;
using System.Linq;
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
    public class AddContractContentCommandHandler: IRequestHandler<AddContractContentCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddContractContentCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddContractContentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var recordExists = await _dbContext.ContractTypeContent.FirstOrDefaultAsync(x => x.EmployeeContractTypeId == request.EmployeeContractTypeId);
                
                if (recordExists == null)
                {
                    ContractTypeContent obj = _mapper.Map<ContractTypeContent>(request);
                    obj.IsDeleted = false;
                    await _dbContext.ContractTypeContent.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    recordExists.ContentDari = request.ContentDari;
                    recordExists.ContentEnglish = request.ContentEnglish;
                    _dbContext.ContractTypeContent.Update(recordExists);
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