using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddAdvanceCommandHandler: IRequestHandler<AddAdvanceCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddAdvanceCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(AddAdvanceCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                var record = await _dbContext.Advances.Include(x => x.EmployeeDetail)
                                                      .FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.EmployeeId == request.EmployeeId
                                                               && x.AdvanceDate.Date.Month <= request.AdvanceDate.Date.Month
                                                               && x.AdvanceDate.Date.Year <= request.AdvanceDate.Date.Year && x.IsDeleted == false
                                                               && x.IsDeducted == false);

                if (record == null)
                {
                    Advances obj = _mapper.Map<Advances>(request);
                    obj.IsDeleted = false;

                    await _dbContext.Advances.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = string.Format(StaticResource.CannotAddAdvance, record.EmployeeDetail.EmployeeCode, record.EmployeeDetail.EmployeeName);

                }
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