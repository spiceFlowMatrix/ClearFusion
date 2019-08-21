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
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddProgramDetailCommandHandler : IRequestHandler<AddProgramDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        private readonly IProjectServices _iProjectServices;

        public AddProgramDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iProjectServices = iProjectServices;
        }

        public async Task<ApiResponse> Handle(AddProgramDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            if (request != null && !string.IsNullOrWhiteSpace(request.ProgramName))
            {
                long LatestCodeId = 0;
                var code = string.Empty;
                try
                {
                    ProgramDetail obj = _mapper.Map<AddProgramDetailCommand, ProgramDetail>(request);


                    var data = await _dbContext.ProgramDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProgramName.Trim().ToLower() == request.ProgramName.Trim().ToLower()); //  Contains(model.ProgramName);                               


                    if (data == null)
                    {
                        var ProgramDetail = await _dbContext.ProgramDetail
                                                            .OrderByDescending(x => x.ProgramId)
                                                            .FirstOrDefaultAsync(x => x.IsDeleted == false);
                        if (ProgramDetail == null)
                        {
                            LatestCodeId = 1;
                            code = ProjectUtility.GenerateCode(LatestCodeId);
                        }
                        else
                        {
                            LatestCodeId = ProgramDetail.ProgramId + 1;
                            code = ProjectUtility.GenerateCode(LatestCodeId);
                        }
                        obj.ProgramName = request.ProgramName;
                        obj.IsDeleted = false;
                        obj.ProgramCode = code;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = DateTime.UtcNow;
                        await _dbContext.ProgramDetail.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();

                        if (obj.ProgramId != 0)
                        {
                            ProjectProgramModel projectProgramModel = new ProjectProgramModel
                            {
                                ProgramId = obj.ProgramId,
                                ProjectId = request.ProjectId.Value,
                                ProjectProgramId = 0
                            };

                            var addEditProjectProgram = await _iProjectServices.AddEditProjectProgram(projectProgramModel, request.CreatedById);

                            if (addEditProjectProgram.StatusCode == 200)
                            {
                                response.data.ProgramDetail = obj;
                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Success";
                            }
                            else
                            {
                                throw new Exception("Project Program could not be saved");
                            }
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.NameAlreadyExist;
                        response.Message = StaticResource.ListNameAlreadyExist;
                    }
                }
                catch (Exception ex)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }

            }
            else if (request != null && string.IsNullOrWhiteSpace(request.ProgramName))
            {//check for emptystring
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