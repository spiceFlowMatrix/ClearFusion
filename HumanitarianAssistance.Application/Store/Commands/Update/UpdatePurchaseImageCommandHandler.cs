using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class UpdatePurchaseImageCommandHandler : IRequestHandler<UpdatePurchaseImageCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public UpdatePurchaseImageCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(UpdatePurchaseImageCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //byte[] filepathBase64 = Encoding.UTF8.GetBytes(model.EmployeeImage);
                string[] str = request.Invoice.Split(",");
                byte[] filepath = Convert.FromBase64String(str[1]);

                string ex = str[0].Split("/")[1].Split(";")[0];
                if (ex == "plain")
                    ex = "txt";
                string guidname = Guid.NewGuid().ToString();
                //byte[] filepath = Encoding.UTF8.GetBytes(str[1]);
                string filename = guidname + "." + ex;
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                File.WriteAllBytes(@"Documents/" + filename, filepath);

                var employeeinfo = await _dbContext.StoreItemPurchases.FirstOrDefaultAsync(x => x.IsDeleted == false && x.PurchaseId == request.PurchaseId);
                if (employeeinfo != null)
                {
                    employeeinfo.ImageFileName = guidname;
                    employeeinfo.ImageFileType = "." + ex;
                    employeeinfo.ModifiedById = request.ModifiedById;
                    employeeinfo.ModifiedDate = DateTime.Now;
                    employeeinfo.IsDeleted = false;
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
