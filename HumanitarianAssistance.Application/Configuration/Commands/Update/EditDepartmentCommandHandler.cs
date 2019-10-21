using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditDepartmentCommandHandler : IRequestHandler<EditDepartmentCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditDepartmentCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var existrecord = await _dbContext.Department.FirstOrDefaultAsync(x => x.IsDeleted == false && x.DepartmentId == request.DepartmentId);
                if (existrecord != null)
                {
                    Department checkDepartment = await _dbContext.Department.FirstOrDefaultAsync(x => x.IsDeleted == false && 
                                                                                                      x.DepartmentName == request.DepartmentName && 
                                                                                                      x.OfficeId == request.OfficeId);
                    if (checkDepartment == null)
                    {
                        existrecord.DepartmentName = request.DepartmentName;
                        existrecord.OfficeId = request.OfficeId;
                        existrecord.ModifiedById = request.ModifiedById;
                        existrecord.ModifiedDate = request.ModifiedDate;
                        existrecord.IsDeleted = request.IsDeleted;
                        await _dbContext.SaveChangesAsync();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = "Department Name already exist for this Office.";
                    }
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