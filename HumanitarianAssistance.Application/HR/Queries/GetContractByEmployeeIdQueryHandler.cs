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
                                join e in _dbContext.EmployeeDetail on ec.EmployeeId equals e.EmployeeID into lec
                                from y in lec.DefaultIfEmpty()
                                join pd in _dbContext.ProvinceDetails on ec.Province equals pd.ProvinceId into lpd
                                from x in lpd.DefaultIfEmpty()
                                join epd in _dbContext.EmployeeProfessionalDetail on ec.EmployeeId equals epd.EmployeeId into lepd
                                from w in lepd.DefaultIfEmpty()
                                join dd in _dbContext.DesignationDetail on ec.Designation equals dd.DesignationId into ldd
                                from v in ldd.DefaultIfEmpty()
                                join od in _dbContext.OfficeDetail on w.OfficeId equals od.OfficeId into lod
                                from u in lod.DefaultIfEmpty()
                                join ect in _dbContext.EmployeeContractType on w.EmployeeContractTypeId equals ect.EmployeeContractTypeId into lect
                                from t in lect.DefaultIfEmpty()
                                join jg in _dbContext.JobGrade on ec.Grade equals jg.GradeId into ljg
                                from s in ljg.DefaultIfEmpty()
                                join cd in _dbContext.CountryDetails on ec.Country equals cd.CountryId into lcd
                                from r in lcd.DefaultIfEmpty()
                                join bld in _dbContext.ProjectBudgetLineDetail on ec.BudgetLine equals bld.BudgetLineId into lbld
                                from q in lbld.DefaultIfEmpty()
                                join p in _dbContext.ProjectDetail on ec.Project equals Convert.ToInt32(p.ProjectId) into lp
                                from px in lp.DefaultIfEmpty()
                                where ec.IsDeleted== false && ec.EmployeeId == request.EmployeeId
                                select new EmployeeContractModel
                                {
                                    EmployeeContractId = ec.EmployeeContractId,
                                    EmployeeId = ec.EmployeeId,
                                    EmployeeName = ec.Employee.EmployeeName,
                                    FatherName = ec.Employee.FatherName,
                                    EmployeeCode = ec.Employee.EmployeeCode,
                                    DesignationId = ec.Designation,
                                    Designation = v.Designation,
                                    ContractStartDate = ec.ContractStartDate,
                                    ContractEndDate = ec.ContractEndDate,
                                    DurationOfContract = ec.DurationOfContract,
                                    Salary = ec.Salary,
                                    Grade = ec.Grade,
                                    DutyStationId = u.OfficeId,
                                    DutyStation = u.OfficeName,
                                    ProvinceId = x.ProvinceId,
                                    Province = x.ProvinceName,
                                    CountryId = ec.Country,
                                    Country = r.CountryName,
                                    JobId = null,
                                    Job = ec.Job,
                                    WorkTime = ec.WorkTime,
                                    WorkDayHours = ec.WorkDayHours,
                                    ContentEnglish = contractTypeDariModel.FirstOrDefault(c => c.EmployeeContractTypeId == w.EmployeeContractTypeId).ContentEnglish,
                                    ContentDari = contractTypeDariModel.FirstOrDefault(c => c.EmployeeContractTypeId == w.EmployeeContractTypeId).ContentDari,
                                    EmployeeImage = y.DocumentGUID + y.Extension,
                                    CountryDari = ec.CountryDari,
                                    DesignationDari = ec.DesignationDari,
                                    DutyStationDari = ec.DutyStationDari,
                                    GradeDari = ec.GradeDari,
                                    FatherNameDari = ec.FatherNameDari,
                                    JobDari = ec.JobDari,
                                    ProvinceDari = ec.ProvinceDari,
                                    EmployeeNameDari = ec.EmployeeNameDari,
                                    GradeName = s.GradeName,
                                    ProjectNameDari = ec.ProjectNameDari,
                                    ProjectName = px.ProjectName,
                                    BudgetLine = q.BudgetName,
                                    BudgetLineDari = ec.BudgetLineDari,
                                    ProjectCode = px.ProjectCode
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