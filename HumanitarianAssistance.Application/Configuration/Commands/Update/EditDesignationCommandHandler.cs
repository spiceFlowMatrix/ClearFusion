using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditDesignationCommandHandler: IRequestHandler<EditDesignationCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public EditDesignationCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(EditDesignationCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var designationinfo = await _dbContext.DesignationDetail.FirstOrDefaultAsync(x => x.DesignationId == request.DesignationId && x.IsDeleted == false);
                
                if (designationinfo != null)
                {
                    designationinfo.Designation = request.Designation;
                    designationinfo.ModifiedById = request.ModifiedById;
                    designationinfo.ModifiedDate = request.ModifiedDate;

                    _dbContext.DesignationDetail.Update(designationinfo);
                    await _dbContext.SaveChangesAsync();
                    
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Sucess";
                }
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