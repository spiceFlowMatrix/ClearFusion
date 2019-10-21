using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
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
namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllSalaryHeadQueryHandler : IRequestHandler<GetAllSalaryHeadQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public GetAllSalaryHeadQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllSalaryHeadQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<SalaryHeadDetails> queryResult = await _dbContext.SalaryHeadDetails.Where(x => x.IsDeleted == false).ToListAsync();


                var salaryheadlist = queryResult.Select(x => new SalaryHeadModel
                {
                    SalaryHeadId = x.SalaryHeadId,
                    HeadTypeId = x.HeadTypeId,
                    HeadName = x.HeadName,
                    Description = x.Description,
                    AccountNo = x?.AccountNo ?? 0,
                    TransactionTypeId = x?.TransactionTypeId ?? 0
                }).OrderBy(x => x.HeadName).ToList();

                response.data.SalaryHeadList = salaryheadlist.OrderBy(x => x.TransactionTypeId).ThenBy(x => x.HeadTypeId).ToList();
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