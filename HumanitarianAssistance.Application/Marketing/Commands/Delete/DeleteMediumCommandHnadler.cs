using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
     public class DeleteMediumCommandHnadler : IRequestHandler<DeleteMediumCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteMediumCommandHnadler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteMediumCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();   
                try
            {
                var mediumDetails = await _dbContext.Mediums.FirstOrDefaultAsync(x => x.IsDeleted == false && x.MediumId == request.MediumId);
                if (mediumDetails != null)
                {
                    mediumDetails.ModifiedById = request.ModifiedById;
                    mediumDetails.ModifiedDate = request.ModifiedDate;
                    mediumDetails.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Medium deleted successfully";
                }
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