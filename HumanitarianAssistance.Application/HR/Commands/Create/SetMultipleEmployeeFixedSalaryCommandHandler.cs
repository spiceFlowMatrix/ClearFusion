using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class SetMultipleEmployeeFixedSalaryCommandHandler : IRequestHandler<SetMultipleEmployeeFixedSalaryCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public SetMultipleEmployeeFixedSalaryCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(SetMultipleEmployeeFixedSalaryCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {
                var existingRecords = await _dbContext.EmployeeBasicSalaryDetail.Where(x=>x.IsDeleted == false && request.EmployeeIds.Contains(x.EmployeeId)).ToListAsync();
                foreach( var emp in existingRecords) {
                    if(request.FixedSalary != null) {
                        emp.BasicSalary = request.FixedSalary.Value;
                    }
                    if(request.CapacityBuilding != null) {
                        emp.CapacityBuildingAmount = request.CapacityBuilding.Value;
                    }
                    if(request.Security != null) {
                        emp.SecurityAmount = request.Security.Value;
                    }
                    if((request.FixedSalary != null) || (request.CapacityBuilding != null) || (request.Security != null)) {
                        emp.ModifiedById = request.ModifiedById;
                        emp.ModifiedDate = request.ModifiedDate;
                    }
                }

                var nonExistingRecords = request.EmployeeIds.Except(await _dbContext.EmployeeBasicSalaryDetail.Where(x=>x.IsDeleted ==false).Select(x=>x.EmployeeId).ToListAsync()).ToList();
                foreach( var empId in nonExistingRecords) {
                    await _dbContext.EmployeeBasicSalaryDetail.AddAsync(new EmployeeBasicSalaryDetail{
                        CreatedById = request.ModifiedById,
                        CreatedDate = request.ModifiedDate,
                        IsDeleted = false,
                        EmployeeId = empId,
                        CurrencyId = null,
                        BasicSalary = (request.FixedSalary != null) ? request.FixedSalary.Value : 0,
                        CapacityBuildingAmount = (request.CapacityBuilding != null) ? request.CapacityBuilding.Value : 0,
                        SecurityAmount = (request.Security != null) ? request.Security.Value : 0,
                    });
                }
                await _dbContext.SaveChangesAsync();
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}