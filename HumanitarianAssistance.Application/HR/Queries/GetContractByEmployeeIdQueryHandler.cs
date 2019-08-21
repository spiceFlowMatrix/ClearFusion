using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Application.HR.Models;
using MediatR;
using System.Collections.Generic;
using HumanitarianAssistance.Domain.Entities;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetContractByEmployeeIdQueryHandler: IRequestHandler<GetContractByEmployeeIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetContractByEmployeeIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetContractByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {

                if (request.EmployeeId != 0)
                {
                    EmployeeContractModel EmployeeContractModel = new EmployeeContractModel();


                    ICollection<ContractTypeContent> contractTypeDariModel = await _dbContext.ContractTypeContent.Where(x => x.IsDeleted == false).ToListAsync();

                    List<EmployeeContractModel> dataModel = (from ec in _dbContext.EmployeeContract
                                join e in _dbContext.EmployeeDetail on ec.EmployeeId equals e.EmployeeID
                                join pd in _dbContext.ProvinceDetails on ec.Province equals pd.ProvinceId
                                join epd in _dbContext.EmployeeProfessionalDetail on e.EmployeeID equals epd.EmployeeId
                                join dd in _dbContext.DesignationDetail on ec.Designation equals dd.DesignationId
                                join od in _dbContext.OfficeDetail on epd.OfficeId equals od.OfficeId
                                join ect in _dbContext.EmployeeContractType on epd.EmployeeContractTypeId equals ect.EmployeeContractTypeId
                                join jg in _dbContext.JobGrade on ec.Grade equals jg.GradeId
                                join cd in _dbContext.CountryDetails on ec.Country equals cd.CountryId
                                join bld in _dbContext.ProjectBudgetLineDetail on ec.BudgetLine equals bld.BudgetLineId
                                join p in _dbContext.ProjectDetail on ec.Project equals Convert.ToInt32(p.ProjectId)
                                where ec.IsDeleted== false && ec.EmployeeId == request.EmployeeId
                                select new EmployeeContractModel
                                {
                                    EmployeeContractId = ec.EmployeeContractId,
                                    EmployeeId = ec.EmployeeId,
                                    EmployeeName = ec.Employee.EmployeeName,
                                    FatherName = ec.Employee.FatherName,
                                    EmployeeCode = ec.Employee.EmployeeCode,
                                    DesignationId = ec.Designation,
                                    Designation = dd.Designation,
                                    ContractStartDate = ec.ContractStartDate,
                                    ContractEndDate = ec.ContractEndDate,
                                    DurationOfContract = ec.DurationOfContract,
                                    Salary = ec.Salary,
                                    Grade = ec.Grade,
                                    DutyStationId = od.OfficeId,
                                    DutyStation = od.OfficeName,
                                    ProvinceId = pd.ProvinceId,
                                    Province = pd.ProvinceName,
                                    CountryId = ec.Country,
                                    Country = cd.CountryName,
                                    JobId = null,
                                    Job = ec.Job,
                                    WorkTime = ec.WorkTime,
                                    WorkDayHours = ec.WorkDayHours,
                                    ContentEnglish = contractTypeDariModel.FirstOrDefault(c => c.EmployeeContractTypeId == epd.EmployeeContractTypeId).ContentEnglish,
                                    ContentDari = contractTypeDariModel.FirstOrDefault(c => c.EmployeeContractTypeId == epd.EmployeeContractTypeId).ContentDari,
                                    EmployeeImage = e.DocumentGUID + e.Extension,
                                    CountryDari = ec.CountryDari,
                                    DesignationDari = ec.DesignationDari,
                                    DutyStationDari = ec.DutyStationDari,
                                    GradeDari = ec.GradeDari,
                                    FatherNameDari = ec.FatherNameDari,
                                    JobDari = ec.JobDari,
                                    ProvinceDari = ec.ProvinceDari,
                                    EmployeeNameDari = ec.EmployeeNameDari,
                                    GradeName = jg.GradeName,
                                    ProjectNameDari = ec.ProjectNameDari,
                                    ProjectName = p.ProjectName,
                                    BudgetLine = bld.BudgetName,
                                    BudgetLineDari = ec.BudgetLineDari,
                                    ProjectCode = p.ProjectCode
                                }).ToList();

                    response.data.EmployeeContractDetails = dataModel;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";


                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Employee Id Cannot be 0";
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