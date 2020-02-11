using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update {
  public class EditOfficeDetailCommandHandler : IRequestHandler<EditOfficeDetailCommand, ApiResponse> {
    private readonly HumanitarianAssistanceDbContext _dbContext;
    public EditOfficeDetailCommandHandler (HumanitarianAssistanceDbContext dbContext) {
      _dbContext = dbContext;
    }

    public async Task<ApiResponse> Handle (EditOfficeDetailCommand request, CancellationToken cancellationToken) {
      ApiResponse response = new ApiResponse ();
      using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction ()) {
        try {
          var existoffice = await _dbContext.OfficeDetail.FirstOrDefaultAsync (o => o.OfficeId != request.OfficeId && o.OfficeCode == request.OfficeCode); //use only OfficeCode

          if (existoffice == null) {
            var officeInfo = await _dbContext.OfficeDetail.FirstOrDefaultAsync (c => c.OfficeId == request.OfficeId);

            officeInfo.OfficeCode = request.OfficeCode;
            officeInfo.OfficeName = request.OfficeName;
            officeInfo.SupervisorName = request.SupervisorName;
            officeInfo.PhoneNo = request.PhoneNo;
            officeInfo.FaxNo = request.FaxNo;
            officeInfo.ModifiedById = request.ModifiedById;
            officeInfo.ModifiedDate = request.ModifiedDate;

            _dbContext.OfficeDetail.Update (officeInfo);
            await _dbContext.SaveChangesAsync ();

            if (request.Department.Any ()) {

              List<int?> departmentToBeRemoved = new List<int?> ();
              var data = _dbContext.Department.Where (x => x.IsDeleted == false && x.OfficeId == request.OfficeId).Select (x => new {DepartmentId = x.DepartmentId}).ToList ();
              foreach (var item in request.Department) {
                var items= data.Where(x=>x.DepartmentId == item.DepartmentId).FirstOrDefault();
                if(items == null && item.DepartmentId != null)
                {
                  departmentToBeRemoved.Add(item.DepartmentId);
                }
              }

              foreach (int id in departmentToBeRemoved) {
                Department department = _dbContext.Department.FirstOrDefault (x => x.DepartmentId == id);

                if (department != null) {
                  department.IsDeleted = true;
                  department.ModifiedById = request.ModifiedById;
                  department.ModifiedDate = request.ModifiedDate;

                  _dbContext.Department.Update (department);
                  await _dbContext.SaveChangesAsync ();
                }
              }              
              foreach (var item in request.Department) {
                // Add new Department
                if (item.DepartmentId == null) {
                  Department department = new Department () {
                  DepartmentName = item.DepartmentName,
                  OfficeId = request.OfficeId,
                  OfficeCode = request.OfficeCode,
                  IsDeleted = false,
                  CreatedById = request.CreatedById,
                  CreatedDate = request.CreatedDate
                  };

                  await _dbContext.Department.AddAsync (department);
                  await _dbContext.SaveChangesAsync ();
                } else // Edit Existing Department
                {
                  Department department = _dbContext.Department.FirstOrDefault (x => x.IsDeleted == false && x.DepartmentId == item.DepartmentId);

                  if (department != null) {
                    department.ModifiedDate = DateTime.UtcNow;
                    department.ModifiedById = request.ModifiedById;
                    department.DepartmentName = item.DepartmentName;

                    _dbContext.Department.Update (department);
                    await _dbContext.SaveChangesAsync ();
                  }
                }
              }
            }

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
          response.Message = StaticResource.SomethingWrong + ex.Message;
        }
      }
      return response;
    }
  }
}