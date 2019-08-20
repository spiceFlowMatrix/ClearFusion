using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Create
{
    public class AddClientDetailsCommandHandler : IRequestHandler<AddClientDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddClientDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddClientDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            long LatestClientId = 0;
            var clientcode = string.Empty;     
            long ClientId = 0;
            ClientDetails clientDetails = new ClientDetails();
            ClientDetailModel mod = new ClientDetailModel();

            try
            {
                if (request.ClientId == 0)
                {
                    var ClientDetail = _dbContext.ClientDetails
                                                           .OrderByDescending(x => x.ClientId)
                                                           .FirstOrDefault();
                    if (ClientDetail == null)
                    {
                        LatestClientId = 1;
                        clientcode = ProjectUtility.GetClientCode(LatestClientId.ToString());
                    }
                    else
                    {
                        LatestClientId = ClientDetail.ClientId + 1;
                        clientcode = ProjectUtility.GetClientCode(LatestClientId.ToString());
                    }
                    ClientDetails ob = new ClientDetails();
                    //ClientDetails obj = _mapper.Map<ClientDetailModel, ClientDetails>(model);
                    ob.ClientName = request.ClientName;
                    ob.CategoryId = request.CategoryId;
                    ob.ClientBackground = request.ClientBackground;
                    ob.Email = request.Email;
                    ob.FocalPoint = request.FocalPoint;
                    ob.History = request.History;
                    ob.ClientCode = clientcode;
                    ob.Phone = request.Phone;
                    ob.PhysicialAddress = request.PhysicialAddress;
                    ob.Position = request.Position;
                    ob.IsDeleted = false;
                    ob.CreatedById = request.CreatedById;
                    ob.CreatedDate = request.CreatedDate;
                    await _dbContext.ClientDetails.AddAsync(ob);
                    await _dbContext.SaveChangesAsync();

                    ClientId = ob.ClientId;
                    mod.ClientId = ClientId;
                    mod.ClientName = ob.ClientName;
                    mod.CategoryId = ob.CategoryId;
                    mod.ClientBackground = ob.ClientBackground;
                    mod.Email = ob.Email;
                    mod.FocalPoint = ob.FocalPoint;
                    mod.History = ob.History;
                    mod.ClientCode = ob.ClientCode;
                    mod.type = "Add";
                    mod.Phone = ob.Phone;
                    mod.PhysicialAddress = ob.PhysicialAddress;
                    mod.Position = ob.Position;
                    mod.Count = await _dbContext.ClientDetails.CountAsync(x => x.IsDeleted == false);
                    response.data.clientDetailsById = mod;
                    response.Message = "Client added successfully";
                    response.StatusCode = StaticResource.successStatusCode;
                }
                else
                {
                    ClientDetails existRecords = await _dbContext.ClientDetails.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ClientId == request.ClientId);
                    if (existRecords != null)
                    {
                        //existRecords = _mapper.Map<AddClientDetailsCommand, ClientDetails>(request);
                        _mapper.Map(request, existRecords);
                        existRecords.IsDeleted = false;
                        existRecords.ModifiedById = request.ModifiedById;
                        existRecords.ModifiedDate = request.ModifiedDate;
                        await _dbContext.SaveChangesAsync();
                        ClientDetailModel data = await (from c in _dbContext.ClientDetails
                                                        where c.IsDeleted == false && c.ClientId == request.ClientId
                                                        select new ClientDetailModel
                                                        {
                                                            ClientId = request.ClientId,
                                                            ClientName=request.ClientName,
                                                            ClientCode=request.ClientCode,
                                                            type= "Edit"
                                                        }).FirstOrDefaultAsync();
                        response.data.clientDetailsById = data;
                        response.Message = "Client updated successfully";
                        response.StatusCode = StaticResource.successStatusCode;
                    }
                    LatestClientId = Convert.ToInt32(request.ClientId);
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
