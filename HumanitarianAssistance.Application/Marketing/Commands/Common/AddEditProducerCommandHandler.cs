using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditProducerCommandHandler : IRequestHandler<AddEditProducerCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditProducerCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEditProducerCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.ProducerId == 0 || request.ProducerId == null)
                {
                    Producer obj = new Producer();                    
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsDeleted = false;
                    obj.ProducerName = request.ProducerName;
                    _mapper.Map(request, obj);
                    await _dbContext.Producers.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.producerById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Producer added Successfully";
                }
                else
                {
                    Producer obj1 = await _dbContext.Producers.FirstOrDefaultAsync(x => x.ProducerId == request.ProducerId);
                    obj1.ModifiedById = request.ModifiedById;
                    obj1.ModifiedDate = DateTime.Now;
                    _mapper.Map(request, obj1);
                    await _dbContext.SaveChangesAsync();
                    response.data.producerById = obj1;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Producer updated successfully";
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
