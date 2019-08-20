using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class UpdateEmployeeImageCommandHandler : IRequestHandler<UpdateEmployeeImageCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public UpdateEmployeeImageCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(UpdateEmployeeImageCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //byte[] filepathBase64 = Encoding.UTF8.GetBytes(model.EmployeeImage);
                string[] str = request.EmployeeImage.Split(",");
                byte[] filepath = Convert.FromBase64String(str[1]);

                string ex = str[0].Split("/")[1].Split(";")[0];

                string guidname = Guid.NewGuid().ToString();
                //byte[] filepath = Encoding.UTF8.GetBytes(str[1]);
                string filename = guidname + "." + ex;
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;


                File.WriteAllBytes(@"Documents/" + filename, filepath);

                var employeeinfo = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId);
                if (employeeinfo != null)
                {
                    employeeinfo.DocumentGUID = guidname;
                    // For Employee Image
                    employeeinfo.DocumentType = 2;
                    employeeinfo.Extension = "." + ex;
                    employeeinfo.EmployeePhoto = null;
                    employeeinfo.ModifiedById = request.ModifiedById;
                    employeeinfo.ModifiedDate = request.ModifiedDate;
                    employeeinfo.IsDeleted = false;
                     _dbContext.EmployeeDetail.Update(employeeinfo);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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