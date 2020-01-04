using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteTenderProposalDocumentCommandHandler: IRequestHandler<DeleteTenderProposalDocumentCommand, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public DeleteTenderProposalDocumentCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteTenderProposalDocumentCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var _docDetail = await _dbContext.EntitySourceDocumentDetails
                                .Where(x=>x.IsDeleted == false && x.DocumentFileId == request.docTypeId)
                                .Include(x=>x.DocumentFileDetail).FirstOrDefaultAsync();
                if(_docDetail == null) {
                    throw new Exception("Document doesn't exists!");
                }

                _docDetail.IsDeleted = true;
                _docDetail.DocumentFileDetail.IsDeleted = true;
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