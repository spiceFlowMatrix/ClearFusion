﻿using AutoMapper;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Project.Commands.Common;
using HumanitarianAssistance.Application.Project.Commands.Create;
using HumanitarianAssistance.Application.Project.Commands.Update;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Application.Project.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanitarianAssistance.WebApi.Infrastructure
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<ProgramDetail, EditProgramDetailCommand>().ReverseMap();
            CreateMap<AddEditProjectDetailCommand, ProjectDetail>().ReverseMap();
            CreateMap<AddEditDonorDetailsCommand, DonorDetail>().ReverseMap();
            CreateMap<AddEditProjectAssignToEmployeeCommand, ProjectAssignTo>().ReverseMap();
            CreateMap<AddEditProjectBudgetLineDetailCommand, ProjectBudgetLineDetail>().ReverseMap();
            CreateMap<AddEditProjectJobDetailCommand, ProjectJobDetail>().ReverseMap();
            CreateMap<AddAreaDetailCommand, AreaDetail>().ReverseMap();
            CreateMap<AddProgramDetailCommand, ProgramDetail>().ReverseMap();
            CreateMap<AddProjectActivityDetailCommand, ProjectActivityDetail>().ReverseMap();
            CreateMap<AddProjectSubActivityDetailCommand, ProjectActivityDetail>().ReverseMap();
            CreateMap<EditAreaDetailCommand, AreaDetail>().ReverseMap();
            CreateMap<EditProjectActivityDetailCommand, ProjectActivityDetail>().ReverseMap();
            CreateMap<EditSectorDetailCommand, SectorDetails>().ReverseMap();
            CreateMap<ProjectBudgetLineDetailModel, ProjectBudgetLineDetail>().ReverseMap();
            CreateMap<ProjectActivityDetail,ProjectActivityModel>().ReverseMap();
            CreateMap<ProjectOtherDetailPdfModel,ProjectOtherDetailNewPdfModel>().ReverseMap();
            CreateMap<ProjectPdfFlags,GetProjectOtherDetailReportPdfQuery>().ReverseMap();
            CreateMap<TenderBidSubmission,AddTenderBidCommand>().ReverseMap();
            CreateMap<TenderBidSubmission,TenderBidsModel>().ReverseMap();
            CreateMap<TenderBidSubmission,EditTenderBidCommand>().ReverseMap();
            
        }
    }
}
