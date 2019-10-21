using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllDocumentDetailByIdQueryHandler : IRequestHandler<GetAllDocumentDetailByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllDocumentDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllDocumentDetailByIdQuery request, CancellationToken cancellationToken)
        {

            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeDocumentDetailModel> documentlist = await _dbContext.EmployeeDocumentDetail.Where(x => x.EmployeeID == request.EmployeeId &&
                                                                                                                    x.IsDeleted == false)
                                                                                                        .Select(x => new EmployeeDocumentDetailModel
                                                                                                        {
                                                                                                            DocumentGUID = x.DocumentGUID + x.Extension,
                                                                                                            DocumentName = x.DocumentName,
                                                                                                        }).ToListAsync();

                response.data.EmployeeDocumentList = documentlist;
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