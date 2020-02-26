using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesPayrollExcelQueryHandler : IRequestHandler<GetEmployeesPayrollExcelQuery, byte[]>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IExcelExportService _excelExportService;
        private readonly IMapper _mapper;

        public GetEmployeesPayrollExcelQueryHandler(IAccountingServices iAccountingServices, IMapper mapper,
                HumanitarianAssistanceDbContext dbContext, IExcelExportService excelExportService)
        {
            _mapper = mapper;
            _dbContext= dbContext;
            _excelExportService= excelExportService;
        }
        public Task<byte[]> Handle(GetEmployeesPayrollExcelQuery request, CancellationToken cancellationToken)
        {
            byte[] result;
            try
            {
                EmployeesPayrollExcelModel model = new EmployeesPayrollExcelModel();

                var result = _dbContext.EmployeePayrollInfoDetail
                                       .Include(x=> x.EmployeeDetail)
                                       .ThenInclude(x=> x.EmployeeProfessionalDetail)
                                       .ThenInclude(x=> x.OfficeDetail)
                                       .Include(x=> x.EmployeeDetail)
                                       .ThenInclude(x=> x.EmployeeProfessionalDetail)
                                       .ThenInclude(x=> x.DesignationDetails)
                                       .Include(x=> x.EmployeeDetail)
                                       .Include(x=> x.EmployeeBasicSalaryDetail)
                                       .ThenInclude(x=> x.CurrencyDetails)
                                       
                                        .Where(x=> x.IsDeleted == false &&
                                            request.SelectedEmployees.Contains(x.EmployeeId))
                                        .Select(x=> new EmployeesPayrollExcelModel {
                                            EmployeeId = x.EmployeeId,

                                        });

                // (from obj in _dbContext.EmployeePayrollInfoDetail
                //                     join e in _dbContext.EmployeeDetail on obj.EmployeeId equals e.EmployeeID into ed
                //                     from e in ed.DefaultIfEmpty()
                //                     join ep in _dbContext.EmployeeProfessionalDetail on obj.EmployeeId equals ep.ProjectId into epd
                //                     from ep in epd.DefaultIfEmpty()
                //                     join od in _dbContext.OfficeDetail on ep.OfficeId equals od.OfficeId into ods
                //                     from od in ods.DefaultIfEmpty()
                //                     join de in _dbContext.DonorEligibilityCriteria on obj.ProjectId equals de.ProjectId into dec
                //                     from de in dec.DefaultIfEmpty()
                //                     join donor in _dbContext.DonorCriteriaDetail on obj.ProjectId equals donor.ProjectId into a
                //                     from donor in a.DefaultIfEmpty()
                //                     join purpose in _dbContext.PurposeofInitiativeCriteria on obj.ProjectId equals purpose.ProjectId into d
                //                     from purpose in d.DefaultIfEmpty()
                //                     join eligibility in _dbContext.EligibilityCriteriaDetail on obj.ProjectId equals eligibility.ProjectId into e
                //                     from eligibility in e.DefaultIfEmpty()
                //                     join feasibility in _dbContext.FeasibilityCriteriaDetail on obj.ProjectId equals feasibility.ProjectId into g
                //                     from feasibility in g.DefaultIfEmpty()
                //                     join priority in _dbContext.PriorityCriteriaDetail on obj.ProjectId equals priority.ProjectId into pr
                //                     from priority in pr.DefaultIfEmpty()
                //                     join financial in _dbContext.FinancialCriteriaDetail on obj.ProjectId equals financial.ProjectId into fi
                //                     from financial in fi.DefaultIfEmpty()
                //                     join risk in _dbContext.RiskCriteriaDetail on obj.ProjectId equals risk.ProjectId into ri
                //                     from risk in ri.DefaultIfEmpty()
                //                     join currency in _dbContext.ProjectProposalDetail on obj.ProjectId equals currency.ProjectId into cr
                //                     from currency in cr.DefaultIfEmpty()


                //                     where obj.IsDeleted == false && obj.ProjectId == request.ProjectId
                //                     select new CriteriaEveluationModel
                //                     { }).


            //    List<ExpandoObject> reportModel= new List<ExpandoObject>();

            //    foreach(var item in response.data.JournalVoucherViewList)
            //    {
            //        dynamic obj = new ExpandoObject();
            //        obj.AccountName = item.AccountName;
            //        obj.AccountCode= item.AccountCode;
            //        obj.BudgetLineDescription= item.BudgetLineDescription;
            //        obj.JobCode = item.JobCode;
            //        obj.ReferenceNo = item.ReferenceNo;
            //        obj.TransactionDate = item.TransactionDate.Value.ToShortDateString();
            //        obj.TransactionDescription= item.TransactionDescription;
            //        obj.Currency = currencyList.FirstOrDefault(y=> y.CurrencyId == item.CurrencyId).CurrencyName;
            //        obj.DebitAmount= item.DebitAmount;
            //        obj.CreditAmount = item.CreditAmount;
            //        obj.Project= item.Project;

            //        reportModel.Add(obj);
            //    }

            //    string headerString = "Journal Report";

            //    List<int> calculateSumOnKeyIndex= new List<int>
            //    {
            //        8,
            //        9
            //    };

            //    result = _excelExportService.ExportToExcel(reportModel, "JournalReport", headerString, true, calculateSumOnKeyIndex);
            // }
            // catch (Exception ex)
            // {
            //     throw ex;
            // }

            return result;
        }
    }
}