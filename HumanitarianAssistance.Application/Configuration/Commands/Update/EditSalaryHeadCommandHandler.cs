using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditSalaryHeadCommandHandler : IRequestHandler<EditSalaryHeadCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditSalaryHeadCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditSalaryHeadCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existrecord = await _dbContext.SalaryHeadDetails.FirstOrDefaultAsync(x => x.SalaryHeadId == request.SalaryHeadId);

                if (existrecord != null)
                {
                    existrecord.HeadName = request.HeadName;
                    existrecord.AccountNo = request.AccountNo;
                    existrecord.TransactionTypeId = request.TransactionTypeId;
                    existrecord.Description = request.Description;
                    existrecord.HeadTypeId = request.HeadTypeId;
                    existrecord.ModifiedById = request.ModifiedById;
                    existrecord.ModifiedDate = request.ModifiedDate;
                    existrecord.IsDeleted = false;
                    await _dbContext.SaveChangesAsync();
                }

                if (request.SaveForAll)
                {
                    List<EmployeePayroll> employeePayrollList = await _dbContext.EmployeePayroll.Where(x => x.IsDeleted == false &&
                                                                                                            x.SalaryHeadId == request.SalaryHeadId)
                                                                                                 .ToListAsync();

                    if (employeePayrollList.Any())
                    {
                        employeePayrollList.ForEach(x =>
                        {
                            x.AccountNo = request.AccountNo; x.TransactionTypeId = request.TransactionTypeId;
                            x.HeadTypeId = request.HeadTypeId;
                        });

                        await _dbContext.SaveChangesAsync();
                    }
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