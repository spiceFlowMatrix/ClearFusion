using System;
using HumanitarianAssistance.Application.Store.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Common.Enums;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.FileManagement.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetComparativeStatementQueryHandler: IRequestHandler<GetComparativeStatementQuery, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IFileManagementService _fileManagement;

        public GetComparativeStatementQueryHandler(HumanitarianAssistanceDbContext dbContext, IFileManagementService fileManagement)
        {
            _dbContext= dbContext;
            _fileManagement = fileManagement;
        }

        public async Task<ApiResponse> Handle(GetComparativeStatementQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                
                
                response.data.ComparativeStatement = model;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        
    }
}