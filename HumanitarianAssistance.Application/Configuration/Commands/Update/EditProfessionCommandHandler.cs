using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditProfessionCommandHandler : IRequestHandler<EditProfessionCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditProfessionCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditProfessionCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ProfessionDetails professioninfo = await _dbContext.ProfessionDetails.FirstOrDefaultAsync(x => x.ProfessionId == request.ProfessionId && x.IsDeleted == false);
                if (professioninfo != null)
                {
                    professioninfo.ProfessionName = request.ProfessionName;
                    professioninfo.ModifiedById = request.ModifiedById;
                    professioninfo.ModifiedDate = request.ModifiedDate;
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
