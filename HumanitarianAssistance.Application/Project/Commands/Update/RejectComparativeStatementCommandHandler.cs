using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
        public class RejectComparativeStatementCommandHandler : IRequestHandler<RejectComparativeStatementCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public RejectComparativeStatementCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(RejectComparativeStatementCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
                {
                    var logisticreq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.LogisticRequestsId == request.requestId);
                    if(logisticreq == null) {
                        throw new Exception("Request doesn't Exist!");
                    }
                    logisticreq.ComparativeStatus = (int)LogisticComparativeStatus.RejectStatement;
                    logisticreq.ModifiedById = request.ModifiedById;
                    logisticreq.ModifiedDate = request.ModifiedDate;
                    var statement = await _dbContext.ComparativeStatementSubmission.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.LogisticRequestsId == request.requestId);
                    if(statement == null) {
                        throw new Exception("Statement doesn't Exist!");
                    }
                    statement.IsDeleted = true;
                    statement.ModifiedById = request.ModifiedById;
                    statement.ModifiedDate = request.ModifiedDate;
                    var docs = await _dbContext.EntitySourceDocumentDetails.Where(x=> x.IsDeleted == false && x.EntityId == request.requestId)
                                                                            .Include(x=> x.DocumentFileDetail)
                                                                            .ToListAsync();
                    if( docs.Count() > 0) {
                        foreach( var item in docs) {
                            item.IsDeleted = true;
                            item.ModifiedById = request.ModifiedById;
                            item.ModifiedDate = request.ModifiedDate;
                            item.DocumentFileDetail.IsDeleted = true;
                        }
                    }
                    await _dbContext.SaveChangesAsync();
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
