using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeProfessionalDetailCommandHandler : IRequestHandler<EditEmployeeProfessionalDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IHRService _hrService;

        public EditEmployeeProfessionalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IHRService hrService)
        {
            _dbContext = dbContext;
            _hrService= hrService;
        }
        public async Task<ApiResponse> Handle(EditEmployeeProfessionalDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                EmployeeProfessionalDetail existrecord = await _dbContext.EmployeeProfessionalDetail.FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                                              x.EmployeeId == request.EmployeeId);
                if (existrecord == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }
                existrecord.EmployeeTypeId = request.EmployeeTypeId;
                existrecord.OfficeId = request.OfficeId;
                existrecord.DepartmentId = request.DepartmentId;
                existrecord.DesignationId = request.DesignationId;
                existrecord.EmployeeContractTypeId = request.EmployeeContractTypeId;
                existrecord.HiredOn = request.HiredOn;
                existrecord.FiredOn = request.FiredOn;
                existrecord.FiredReason = request.FiredReason;
                existrecord.ResignationOn = request.ResignationOn;
                existrecord.TrainingBenefits = request.TrainingBenefits;
                existrecord.JobDescription = request.JobDescription;
                existrecord.ResignationReason = request.ResignationReason;
                existrecord.AttendanceGroupId = request.AttendanceGroupId;
                existrecord.MembershipSupportInPoliticalParty = request.MembershipSupportInPoliticalParty;
                existrecord.ModifiedById = request.ModifiedById;
                existrecord.ModifiedDate = request.ModifiedDate;
                existrecord.DutyStation=  request.DutyStation;
                
                await _dbContext.SaveChangesAsync();

                var employeeinfo = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.EmployeeID == request.EmployeeId && x.IsDeleted == false);
                if (employeeinfo != null)
                {
                    employeeinfo.EmployeeTypeId = request.EmployeeTypeId;
                    await _dbContext.SaveChangesAsync();
                }

                //get userdetail based on EmployeeId
                UserDetails user = await _dbContext.UserDetails.FirstOrDefaultAsync(x=> x.EmployeeId == request.EmployeeId);

                //when employee is active
                if (request.EmployeeTypeId == (int)EmployeeTypeStatus.Active)
                {
                    bool isSalaryHeadSaved=  await _hrService.AddEmployeePayrollDetails(request.EmployeeId);

                    if(!isSalaryHeadSaved)
                    {
                        throw new Exception(StaticResource.SalaryHeadNotSaved);
                    }

                    if(user != null)
                    {
                        user.IsDeleted = false;
                        await _dbContext.SaveChangesAsync();
                    }
                    
                } //when employee is terminated get its record from UserDetail and delete it
                else if(request.EmployeeTypeId == (int)EmployeeTypeStatus.Terminated && user != null)
                {
                    
                    user.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                } //when employee is terminated to active get its record from UserDetail and undelete

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