using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create {
    public class AddOfficeDetailCommandHandler : IRequestHandler<AddOfficeDetailCommand, ApiResponse> {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public AddOfficeDetailCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle (AddOfficeDetailCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction ()) {
                try {
                    var existoffice = await _dbContext.OfficeDetail.FirstOrDefaultAsync (o => o.OfficeCode == request.OfficeCode); //use only OfficeCode

                    if (existoffice == null) {
                        OfficeDetail obj = new OfficeDetail {
                        OfficeCode = request.OfficeCode,
                        OfficeName = request.OfficeName,
                        SupervisorName = request.SupervisorName,
                        PhoneNo = request.PhoneNo,
                        FaxNo = request.FaxNo,
                        OfficeKey = request.OfficeKey,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                        };
                        await _dbContext.OfficeDetail.AddAsync (obj);
                        await _dbContext.SaveChangesAsync ();

                        List<Department> dep = new List<Department> ();
                        foreach (var item in request.Department) {
                            var existrecord = await _dbContext.Department.FirstOrDefaultAsync (x => x.IsDeleted == false && x.DepartmentName == item.DepartmentName && x.OfficeId == obj.OfficeId);
                            if (existrecord == null) {
                                Department details = new Department () {
                                DepartmentName = item.DepartmentName,
                                OfficeId = obj.OfficeId,
                                OfficeCode = obj.OfficeCode,
                                IsDeleted = false,
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate
                                };
                                dep.Add (details);
                            }
                        }
                        await _dbContext.Department.AddRangeAsync (dep);
                        await _dbContext.SaveChangesAsync ();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    } else {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = StaticResource.MandateNameAlreadyExist;
                    }
                    tran.Commit ();
                } catch (Exception ex) {
                    tran.Rollback ();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = ex.Message;
                }
            }
            return response;
        }

    }
}