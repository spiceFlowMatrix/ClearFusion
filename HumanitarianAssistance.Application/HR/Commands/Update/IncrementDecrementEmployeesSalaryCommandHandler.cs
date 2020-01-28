using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class IncrementDecrementEmployeesSalaryCommandHandler : IRequestHandler<IncrementDecrementEmployeesSalaryCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public IncrementDecrementEmployeesSalaryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<object> Handle(IncrementDecrementEmployeesSalaryCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            try
            {
                if(request.ReconfigureType == 0) // For Incrementing Salary
                {
                    if(request.Percentage != null && request.Amount == null) 
                    {
                        var nonExistingRecords = request.EmployeeIds.Except(await _dbContext.EmployeeBasicSalaryDetail.Where(x=>x.IsDeleted ==false).Select(x=>x.EmployeeId).ToListAsync()).ToList();
                        if(nonExistingRecords.Any()) {
                            throw new Exception(String.Format("Employees with Code: {0}, doesnot have thier Basic Salary Configured!",
                            String.Join(",", nonExistingRecords.Select(x=>string.Format("E{0}", x)))));
                        }  
                        var EmployeeSalaryDetail = await _dbContext.EmployeeBasicSalaryDetail.Where(x=> x.IsDeleted == false && request.EmployeeIds.Contains(x.EmployeeId)).ToListAsync();
                        foreach(var emp in EmployeeSalaryDetail) 
                        {
                            var amountToIncrement = (request.Percentage/100) * emp.BasicSalary;
                            var oldBasicSalary = emp.BasicSalary;
                            emp.BasicSalary += amountToIncrement.Value;
                            emp.ModifiedDate = request.ModifiedDate;
                            emp.ModifiedById = request.ModifiedById;
                            await _dbContext.EmployeeHistoryDetail.AddAsync(new EmployeeHistoryDetail{
                                EmployeeID = emp.EmployeeId,
                                CreatedDate = request.ModifiedDate,
                                CreatedById = request.ModifiedById,
                                HistoryDate = request.ModifiedDate,
                                Description = String.Format("Basic Salary has been incremented by {0}%, Previous Salary: {1}, Updated Salary: {2}", request.Percentage, oldBasicSalary, emp.BasicSalary)
                            });
                        }
                        await _dbContext.SaveChangesAsync();
                        success = true;
                    } 
                    else if(request.Percentage == null && request.Amount != null) 
                    {
                        var nonExistingRecords = request.EmployeeIds.Except(await _dbContext.EmployeeBasicSalaryDetail.Where(x=>x.IsDeleted ==false).Select(x=>x.EmployeeId).ToListAsync()).ToList();
                        if(nonExistingRecords.Any()) {
                            throw new Exception(String.Format("Employees with Code: {0}, doesnot have thier Basic Salary Configured!",
                            String.Join(",", nonExistingRecords.Select(x=>string.Format("E{0}", x)))));
                        }  
                        var EmployeeSalaryDetail = await _dbContext.EmployeeBasicSalaryDetail.Where(x=> x.IsDeleted == false && request.EmployeeIds.Contains(x.EmployeeId)).ToListAsync();
                        foreach(var emp in EmployeeSalaryDetail) 
                        {
                            var oldBasicSalary = emp.BasicSalary;
                            emp.BasicSalary += request.Amount.Value;
                            emp.ModifiedDate = request.ModifiedDate;
                            emp.ModifiedById = request.ModifiedById;
                            await _dbContext.EmployeeHistoryDetail.AddAsync(new EmployeeHistoryDetail{
                                EmployeeID = emp.EmployeeId,
                                CreatedDate = request.ModifiedDate,
                                CreatedById = request.ModifiedById,
                                HistoryDate = request.ModifiedDate,
                                Description = String.Format("Basic Salary has been incremented by Amount: {0}, Previous Salary: {1}, Updated Salary: {2}", request.Amount, oldBasicSalary, emp.BasicSalary)
                            });
                        }
                        await _dbContext.SaveChangesAsync();
                        success = true;
                    } 
                    else 
                    {
                        throw new Exception("You must enter either Percentage Or Salary to reconfigure!");
                    }
                }
                else // For Decrementing Salary
                {
                    if(request.Percentage != null && request.Amount == null) 
                    {
                        var nonExistingRecords = request.EmployeeIds.Except(await _dbContext.EmployeeBasicSalaryDetail.Where(x=>x.IsDeleted ==false).Select(x=>x.EmployeeId).ToListAsync()).ToList();
                        if(nonExistingRecords.Any()) {
                            throw new Exception(String.Format("Employees with Code: {0}, doesnot have thier Basic Salary Configured!",
                            String.Join(",", nonExistingRecords.Select(x=>string.Format("E{0}", x)))));
                        }  
                        var EmployeeSalaryDetail = await _dbContext.EmployeeBasicSalaryDetail.Where(x=> x.IsDeleted == false && request.EmployeeIds.Contains(x.EmployeeId)).ToListAsync();
                        foreach(var emp in EmployeeSalaryDetail) 
                        {
                            var amountToDecrement = (request.Percentage/100) * emp.BasicSalary;
                            var oldBasicSalary = emp.BasicSalary;
                            emp.BasicSalary -= amountToDecrement.Value;
                            emp.ModifiedDate = request.ModifiedDate;
                            emp.ModifiedById = request.ModifiedById;
                            await _dbContext.EmployeeHistoryDetail.AddAsync(new EmployeeHistoryDetail{
                                EmployeeID = emp.EmployeeId,
                                CreatedDate = request.ModifiedDate,
                                CreatedById = request.ModifiedById,
                                HistoryDate = request.ModifiedDate,
                                Description = String.Format("Basic Salary has been decremented by {0}%, Previous Salary: {1}, Updated Salary: {2}", request.Percentage, oldBasicSalary, emp.BasicSalary)
                            });
                        }
                        await _dbContext.SaveChangesAsync();
                        success = true;
                    } 
                    else if(request.Percentage == null && request.Amount != null) 
                    {
                        var nonExistingRecords = request.EmployeeIds.Except(await _dbContext.EmployeeBasicSalaryDetail.Where(x=>x.IsDeleted ==false).Select(x=>x.EmployeeId).ToListAsync()).ToList();
                        if(nonExistingRecords.Any()) {
                            throw new Exception(String.Format("Employees with Code: {0}, doesnot have thier Basic Salary Configured!",
                            String.Join(",", nonExistingRecords.Select(x=>string.Format("E{0}", x)))));
                        }  
                        var EmployeeSalaryDetail = await _dbContext.EmployeeBasicSalaryDetail.Where(x=> x.IsDeleted == false && request.EmployeeIds.Contains(x.EmployeeId)).ToListAsync();
                        foreach(var emp in EmployeeSalaryDetail) 
                        {
                            var oldBasicSalary = emp.BasicSalary;
                            emp.BasicSalary -= request.Amount.Value;
                            emp.ModifiedDate = request.ModifiedDate;
                            emp.ModifiedById = request.ModifiedById;
                            await _dbContext.EmployeeHistoryDetail.AddAsync(new EmployeeHistoryDetail{
                                EmployeeID = emp.EmployeeId,
                                CreatedDate = request.ModifiedDate,
                                CreatedById = request.ModifiedById,
                                HistoryDate = request.ModifiedDate,
                                Description = String.Format("Basic Salary has been decremented by Amount: {0}, Previous Salary: {1}, Updated Salary: {2}", request.Amount, oldBasicSalary, emp.BasicSalary)
                            });
                        }
                        await _dbContext.SaveChangesAsync();
                        success = true;
                    } 
                    else 
                    {
                        throw new Exception("You must enter either Percentage Or Salary to reconfigure!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }
    }
}