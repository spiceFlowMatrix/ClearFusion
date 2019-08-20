using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddPurchaseCommandHandler : IRequestHandler<AddPurchaseCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddPurchaseCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddPurchaseCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    StoreItemPurchase purchase = _mapper.Map<StoreItemPurchase>(request);

                    // For Image 

                    if (request.ImageFileName != null && request.ImageFileName != "")
                    {
                        string[] str = request.ImageFileName.Split(",");
                        byte[] filepath = Convert.FromBase64String(str[1]);
                        string ex = str[0].Split("/")[1].Split(";")[0];
                        string guidname = Guid.NewGuid().ToString();
                        string filename = guidname + "." + ex;
                        var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                        File.WriteAllBytes(@"Documents/" + filename, filepath);

                        purchase.ImageFileName = guidname;
                        purchase.ImageFileType = "." + ex;
                    }
                    else
                    {
                        purchase.ImageFileName = null;
                        purchase.ImageFileType = null;
                    }

                    // For invoice 

                    if (request.InvoiceFileName != null && request.InvoiceFileName != "")
                    {
                        string[] str = request.InvoiceFileName.Split(",");
                        byte[] filepath = Convert.FromBase64String(str[1]);
                        string ex = str[0].Split("/")[1].Split(";")[0];
                        if (ex == "plain")
                            ex = "txt";
                        string guidname = Guid.NewGuid().ToString();
                        string filename = guidname + "." + ex;
                        var pathFile = Path.Combine(Directory.GetCurrentDirectory(), @"Documents/") + filename;
                        File.WriteAllBytes(@"Documents/" + filename, filepath);

                        purchase.InvoiceFileName = guidname;
                        purchase.InvoiceFileType = "." + ex;
                    }
                    else
                    {
                        purchase.InvoiceFileName = null;
                        purchase.InvoiceFileType = null;
                    }

                    purchase.IsDeleted = false;
                    purchase.CreatedById = request.CreatedById;
                    purchase.CreatedDate = request.CreatedDate;

                    await _dbContext.StoreItemPurchases.AddAsync(purchase);
                    await _dbContext.SaveChangesAsync();
                    //await _uow.SaveAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
