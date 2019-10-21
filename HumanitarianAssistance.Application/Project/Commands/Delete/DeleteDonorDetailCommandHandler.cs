using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteDonorDetailCommandHandler: IRequestHandler<DeleteDonorDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteDonorDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
         public async Task<ApiResponse> Handle(DeleteDonorDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                DonorDetail DonorInfo = await _dbContext.DonorDetail.FirstOrDefaultAsync(c => c.DonorId == request.DonorId &&
                                                                                              c.IsDeleted == false);
                DonorInfo.IsDeleted = true;
                DonorInfo.ModifiedById = request.ModifiedById;
                DonorInfo.ModifiedDate = DateTime.UtcNow;
                _dbContext.DonorDetail.Update(DonorInfo);
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.data.TotalCount = await _dbContext.DonorDetail.Where(x => x.IsDeleted == false).AsNoTracking().CountAsync();

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