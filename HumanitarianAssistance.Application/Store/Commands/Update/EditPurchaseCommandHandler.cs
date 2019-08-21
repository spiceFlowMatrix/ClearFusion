using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditPurchaseCommandHandler : IRequestHandler<EditPurchaseCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditPurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditPurchaseCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var purchaseRecord = await _dbContext.StoreItemPurchases.FirstOrDefaultAsync(x => x.PurchaseId == request.PurchaseId);

                    if (purchaseRecord != null)
                    {
                        _mapper.Map(request, purchaseRecord);

                        if (!string.IsNullOrEmpty(request.ImageFileName))
                        {
                            if (request.ImageFileName.Contains(","))
                            {
                                string[] str = request.ImageFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.ImageFileName = guidname;
                                purchaseRecord.ImageFileType = "." + ex;

                            }
                        }

                        if (request.InvoiceFileName != null && request.InvoiceFileName != "")
                        {
                            if (request.InvoiceFileName.Contains(","))
                            {
                                string[] str = request.InvoiceFileName.Split(",");
                                byte[] filepath = Convert.FromBase64String(str[1]);
                                string ex = str[0].Split("/")[1].Split(";")[0];
                                string guidname = Guid.NewGuid().ToString();
                                string filename = guidname + "." + ex;
                                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                                File.WriteAllBytes(@"Documents/" + filename, filepath);

                                purchaseRecord.InvoiceFileName = guidname;
                                purchaseRecord.InvoiceFileType = "." + ex;
                            }
                        }

                        purchaseRecord.IsDeleted = false;
                        purchaseRecord.ModifiedById = request.ModifiedById;
                        purchaseRecord.ModifiedDate = request.ModifiedDate;
                        await _dbContext.SaveChangesAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Record cannot be updated";
                        return response;
                    }
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "request values are inappropriate";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }
            return response;
        }
    }
}
