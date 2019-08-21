using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditProjectDetailCommandHandler: IRequestHandler<AddEditProjectDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditProjectDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddEditProjectDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            ProjectPhaseTime _ProjectPhase = new ProjectPhaseTime();
            ProjectDetail _ProjectDetail = new ProjectDetail();
            DbContext db = _dbContext;
            long LatestprojectId = 0;
            var projectcode = string.Empty;
            using (IDbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {
                    if (request.ProjectId == 0)
                    {
                        var ProjectDetail =  _dbContext.ProjectDetail
                                                      .OrderByDescending(x => x.ProjectId)
                                                      .FirstOrDefault(x => x.IsDeleted == false);
                        if (ProjectDetail == null)
                        {
                            LatestprojectId = 1;
                            projectcode = ProjectUtility.GenerateProjectCode(LatestprojectId);
                        }
                        else
                        {
                            LatestprojectId = ProjectDetail.ProjectId + 1;
                            projectcode = ProjectUtility.GenerateProjectCode(LatestprojectId);
                        }

                        ProjectDetail obj = _mapper.Map<AddEditProjectDetailCommand, ProjectDetail>(request);
                        obj.ProjectCode = projectcode; // ProjectDetail != null ? getProjectCode(ProjectDetail.ProjectId.ToString()) : getProjectCode("1");

                        obj.ProjectName = request.ProjectName;
                        obj.ProjectDescription = request.ProjectDescription;
                        obj.StartDate = DateTime.UtcNow;
                        obj.EndDate = request.EndDate;
                        obj.IsProposalComplate = request.IsProposalComplate;
                        obj.ReviewerId = request.ReviewerId;
                        obj.DirectorId = request.DirectorId;
                        obj.ProjectPhaseDetailsId = (int)ProjectPhaseType.Planning;
                        obj.IsDeleted = false;
                        obj.IsActive = true;
                        obj.CreatedById = request.CreatedById;
                        obj.CreatedDate = DateTime.UtcNow;
                         _dbContext.ProjectDetail.Add(obj);
                         _dbContext.SaveChanges();
                        _ProjectPhase.ProjectId = LatestprojectId = obj.ProjectId;
                        _ProjectPhase.ProjectPhaseDetailsId = (int)ProjectPhaseType.Planning;
                        _ProjectPhase.PhaseStartData = DateTime.UtcNow;
                        _ProjectPhase.IsDeleted = false;
                        _ProjectPhase.CreatedById = request.CreatedById;
                        _ProjectPhase.CreatedDate = DateTime.UtcNow;
                         _dbContext.ProjectPhaseTime.Add(_ProjectPhase);
                         _dbContext.SaveChanges();

                        response.data.ProjectDetail = obj; //don't remove this

                    }
                    else
                    {
                        ProjectDetail existProjectRecord =  _dbContext.ProjectDetail.FirstOrDefault(x => x.IsDeleted == false && x.ProjectId == request.ProjectId);
                        ProjectPhaseTime exstingProjectTimePhase = _dbContext.ProjectPhaseTime.FirstOrDefault(y => y.IsDeleted == false && y.ProjectId == request.ProjectId);
                        if (existProjectRecord != null)
                        {
                            existProjectRecord.ProjectName = request.ProjectName;
                            existProjectRecord.ProjectDescription = request.ProjectDescription;
                            existProjectRecord.IsProposalComplate = request.IsProposalComplate;
                            existProjectRecord.ReviewerId = request.ReviewerId;
                            existProjectRecord.DirectorId = request.DirectorId;
                            existProjectRecord.IsDeleted = false;
                            existProjectRecord.ModifiedById = request.ModifiedById;
                            existProjectRecord.ModifiedDate = DateTime.UtcNow;
                            _dbContext.SaveChanges();
                            if (exstingProjectTimePhase != null)
                            {
                                _ProjectPhase.ProjectPhaseDetailsId = (int)ProjectPhaseType.Planning;
                                _ProjectPhase.PhaseStartData = DateTime.UtcNow;
                                _ProjectPhase.IsDeleted = false;
                                _ProjectPhase.ModifiedById = request.ModifiedById;
                                _ProjectPhase.ModifiedDate = DateTime.UtcNow;
                                _dbContext.SaveChanges();

                            }
                            //_uow.GetDbContext().SaveChanges();
                            LatestprojectId = request.ProjectId;
                        }
                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.CommonId.Id = Convert.ToInt32(LatestprojectId);

                    response.Message = "Success";
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }
            return response;
        }
    }
}