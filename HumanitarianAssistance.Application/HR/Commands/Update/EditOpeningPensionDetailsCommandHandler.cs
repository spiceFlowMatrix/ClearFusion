using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update {

    public class EditOpeningPensionDetailsCommandHandler : IRequestHandler<EditOpeningPensionDetailsCommand, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditOpeningPensionDetailsCommandHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle (EditOpeningPensionDetailsCommand request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var existRecord = await _dbContext.MultiCurrencyOpeningPension.FirstOrDefaultAsync (x => x.IsDeleted == false && x.PensionId == request.PensionId);
                existRecord.Amount = request.Amount;
                existRecord.ModifiedById = request.ModifiedById;
                existRecord.ModifiedDate = request.ModifiedDate;
                await _dbContext.SaveChangesAsync ();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}