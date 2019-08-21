using System;
using System.Collections.Generic;
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
    public class AddEmployeeApplyLeaveCommandHandler: IRequestHandler<AddEmployeeApplyLeaveCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddEmployeeApplyLeaveCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEmployeeApplyLeaveCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                var existrecord = await _dbContext.AssignLeaveToEmployee.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeApplyLeave[0].EmployeeId && x.LeaveReasonId == request.EmployeeApplyLeave[0].LeaveReasonId);

                int balanceleave = (int)existrecord.AssignUnit - (int)(existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit);
                
                if (balanceleave >= request.EmployeeApplyLeave.Count)
                {
                    List<EmployeeApplyLeave> applyleavelist = new List<EmployeeApplyLeave>();
                    
                    foreach (var list in request.EmployeeApplyLeave)
                    {
                        EmployeeApplyLeave obj = new EmployeeApplyLeave();
                        obj.EmployeeId = list.EmployeeId;
                        obj.FromDate = list.FromDate;
                        obj.ToDate = list.ToDate;
                        obj.LeaveReasonId = list.LeaveReasonId;
                        obj.Remarks = list.Remarks;
                        obj.FinancialYearId = existrecord.FinancialYearId;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = DateTime.UtcNow;
                        obj.IsDeleted = false;
                        applyleavelist.Add(obj);
                    }
                    await _dbContext.EmployeeApplyLeave.AddRangeAsync(applyleavelist);

                    await _dbContext.SaveChangesAsync();

                    if (existrecord != null)
                    {
                        int? usedleaveunit = existrecord.UsedLeaveUnit == null ? 0 : existrecord.UsedLeaveUnit;
                        existrecord.UsedLeaveUnit = usedleaveunit + request.EmployeeApplyLeave.Count;
                        existrecord.ModifiedById = request.ModifiedById;
                        existrecord.ModifiedDate = DateTime.UtcNow;
                        existrecord.IsDeleted = false;
                        _dbContext.AssignLeaveToEmployee.Update(existrecord);
                        await _dbContext.SaveChangesAsync();
                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "More than leave cannot apply from balance leave.";
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