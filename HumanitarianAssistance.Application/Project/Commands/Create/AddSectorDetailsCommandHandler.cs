using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServices;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddSectorDetailsCommandHandler : IRequestHandler<AddSectorDetailsCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProjectServices _iProjectServices;

        public AddSectorDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iProjectServices = iProjectServices;
        }

        public async Task<ApiResponse> Handle(AddSectorDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                long LatestCodeId = 0;
                if (request != null && !string.IsNullOrWhiteSpace(request.SectorName))
                {
                    var code = string.Empty;

                    try
                    {
                        var data = _dbContext.SectorDetails.FirstOrDefault(x => x.IsDeleted == false && x.SectorName.Trim().ToLower() == request.SectorName.Trim().ToLower());

                        if (data == null)
                        {
                            SectorDetails obj = new SectorDetails();
                            var sectorDetail = _dbContext.SectorDetails
                                                         .OrderByDescending(x => x.SectorId)
                                                         .FirstOrDefault(x => x.IsDeleted == false);
                            if (sectorDetail == null)
                            {
                                LatestCodeId = 1;
                                code = ProjectUtility.GenerateCode(LatestCodeId);
                            }
                            else
                            {
                                LatestCodeId = sectorDetail.SectorId + 1;
                                code = ProjectUtility.GenerateCode(LatestCodeId);
                            }
                            obj.SectorName = request.SectorName;
                            obj.IsDeleted = false;
                            obj.SectorCode = code;
                            obj.CreatedById = request.CreatedById;
                            obj.CreatedDate = DateTime.UtcNow;
                            await _dbContext.SectorDetails.AddAsync(obj);
                            await _dbContext.SaveChangesAsync();

                            if (obj.SectorId != 0)
                            {

                                ProjectSector sectorObj = new ProjectSector();
                                sectorObj.ProjectId = request.ProjectId;
                                sectorObj.SectorId = obj.SectorId;
                                sectorObj.IsDeleted = false;
                                sectorObj.CreatedById = request.CreatedById;
                                sectorObj.CreatedDate = request.CreatedDate;
                                await _dbContext.ProjectSector.AddAsync(sectorObj);
                                await _dbContext.SaveChangesAsync();

                            }
                            response.StatusCode = StaticResource.successStatusCode;
                        }
                        else
                        {
                            response.StatusCode = StaticResource.NameAlreadyExist;
                            response.Message = StaticResource.ListNameAlreadyExist;
                        }
                        transaction.Commit();
                        response.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.SomethingWrong + ex.Message;
                    }
                }
                else if (request != null && string.IsNullOrWhiteSpace(request.SectorName)) //check for emptystring
                {
                    response.StatusCode = StaticResource.notValid;
                    response.Message = StaticResource.validData;
                }
                else if (request == null)
                {
                    response.StatusCode = StaticResource.NameAlreadyExist;
                }

                return response;
            }
        }

    }
}