using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllJournalQueryHandler : IRequestHandler<GetAllJournalQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetAllJournalQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllJournalQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var journallist = await (from o in _dbContext.JournalDetail
                                         where o.IsDeleted == false
                                         orderby o.JournalCode ascending
                                         select new JournalDetailModel
                                         {
                                             JournalCode = o.JournalCode,
                                             JournalName = o.JournalName,
                                             JournalType = o.JournalType,
                                             CreatedById = o.CreatedById,
                                             CreatedDate = o.CreatedDate,
                                             ModifiedById = o.ModifiedById,
                                             ModifiedDate = o.ModifiedDate
                                         }).ToListAsync();

                response.data.JournalDetailList = journallist;
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