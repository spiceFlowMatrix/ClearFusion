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
namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeletePayrollAccountHeadCommandHandler : IRequestHandler<DeletePayrollAccountHeadCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeletePayrollAccountHeadCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeletePayrollAccountHeadCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                PayrollAccountHead xPayrollAccountHead = await _dbContext.PayrollAccountHead.FirstOrDefaultAsync(x => x.PayrollHeadId == request.PayrollHeadId);
                xPayrollAccountHead.IsDeleted = true;
                xPayrollAccountHead.ModifiedById = request.ModifiedById;
                xPayrollAccountHead.ModifiedDate = request.ModifiedDate;

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
