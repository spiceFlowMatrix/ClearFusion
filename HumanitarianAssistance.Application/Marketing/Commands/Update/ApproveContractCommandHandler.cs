using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class ApproveContractCommandHandler : IRequestHandler<ApproveContractCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public ApproveContractCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(ApproveContractCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            var existRecord = await _dbContext.ContractDetails.FirstAsync(x => x.IsDeleted == false && x.ContractId == request.ContractId);
            if (existRecord != null)
            {
                try
                {
                    if (request.Type == "Approve")
                    {
                        existRecord.IsApproved = true;
                    }
                    if (request.Type == "Rejected")
                    {
                        existRecord.IsDeclined = true;
                    }
                    existRecord.ModifiedDate = request.ModifiedDate;
                    existRecord.ModifiedById = request.ModifiedById;
                    _dbContext.ContractDetails.Update(existRecord);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    if (request.Type == "Approve")
                    {
                        response.Message = "Contract approved successfully";
                    }
                    if (request.Type == "Rejected")
                    {
                        response.Message = "Contract rejected successfully";
                    }
                    response.data.contractDetails = existRecord;
                }
                catch (Exception ex)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
    }
}
