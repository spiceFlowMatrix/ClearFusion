using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddDocumentDetailCommandHandler : IRequestHandler<AddDocumentDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddDocumentDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public  async Task<ApiResponse> Handle(AddDocumentDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                byte[] filepathBase64 = Encoding.UTF8.GetBytes(request.FilePath);
                string[] str = request.FilePath.Split(",");
                byte[] filepath = Convert.FromBase64String(str[1]);

                string ex = str[0].Split("/")[1].Split(";")[0];

                string guidname = Guid.NewGuid().ToString();
                string filename = guidname + "." + ex;

                File.WriteAllBytes(@"Documents/" + filename, filepath);    // Document path for server

                EmployeeDocumentDetail obj = new EmployeeDocumentDetail
                {
                    DocumentGUID = guidname,
                    //Doctype 1 for voucher document
                    DocumentType = request.DocumentType,
                    Extension = "." + ex,
                    DocumentName = request.DocumentName,
                    DocumentDate = request.DocumentDate,
                    EmployeeID = request.EmployeeID,
                    CreatedById = request.CreatedById,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                };
                await _dbContext.EmployeeDocumentDetail.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
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
