using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class UpdatePayrollAccountHeadCommandHandler : IRequestHandler<UpdatePayrollAccountHeadCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public UpdatePayrollAccountHeadCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(UpdatePayrollAccountHeadCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                PayrollAccountHead xPayrollAccountHead =await _dbContext.PayrollAccountHead.FirstOrDefaultAsync(x => x.PayrollHeadId == request.PayrollHeadId);

                xPayrollAccountHead.AccountNo = request.AccountNo;
                xPayrollAccountHead.Description = request.Description;
                xPayrollAccountHead.PayrollHeadId = request.PayrollHeadId;
                xPayrollAccountHead.PayrollHeadName = request.PayrollHeadName;
                xPayrollAccountHead.PayrollHeadTypeId = request.PayrollHeadTypeId;
                xPayrollAccountHead.TransactionTypeId = request.TransactionTypeId;
                xPayrollAccountHead.ModifiedById = request.ModifiedById;
                xPayrollAccountHead.ModifiedDate = request.ModifiedDate;
                xPayrollAccountHead.IsDeleted = false;

                await _dbContext.SaveChangesAsync();
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
