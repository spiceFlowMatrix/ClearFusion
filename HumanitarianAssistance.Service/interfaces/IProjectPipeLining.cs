//using DataAccess.DbEntities;
//using HumanitarianAssistance.Service.APIResponses;
//using HumanitarianAssistance.ViewModels.Models;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace HumanitarianAssistance.Service.interfaces
//{
//    public interface IProjectPipeLining
//    {
//        Task<APIResponse> AddBudgetLine(ProjectBudgetLineModel model);
//        Task<APIResponse> GetProjectLineDetail(long projectId);
//        Task<APIResponse> EditProjectLineDetail(ProjectBudgetLineModel model);
//        Task<APIResponse> AddBudgetRecivable(BudgetReceivableModel model);
//        Task<APIResponse> GetBudgetRecivable(long projectId,long budgetlineId);
//        Task<APIResponse> EditBudgetReceivable(BudgetReceivableModel model);
//        Task<APIResponse> AddBudgetLineReceived(BudgetReceivedAmountModel model);
//        Task<APIResponse> EditBudgetLineReceived(BudgetReceivedAmountModel model);
//        Task<APIResponse> GetBudgetReceived(long projectId,long budgetLineId,long recivableId);
//        Task<APIResponse> GetAllProjectBudgetLineByProjectId(long ProjectId);
//        Task<APIResponse> AddBudgetPayable(BudgetPayableModel model);
//        Task<APIResponse> GetBudgetPayable(long projectId, long budgetlineId);
//        Task<APIResponse> EditBudgetPayable(BudgetPayableModel model);
//        Task<APIResponse> AddBudgetLinePaid(BudgetPayableAmountModel model);
//        Task<APIResponse> GetBudgetPaidAmount(long projectId, long budgetLineId, long payableId);
//        Task<APIResponse> EditBudgetLinePaid(BudgetPayableAmountModel model);
//        Task<APIResponse> GetBudgetSummary(long projectId);
//        Task<APIResponse> AddProjectDocument(ProjectDocumentModel model);
//        Task<APIResponse> DeleteProjectDocument(int projectdocumentid, string userid);
//        Task<APIResponse> GetProjectDocumentDetail(int projectid);
//		Task<APIResponse> AssignEmployeeToBudgetLine(List<BudgetLineEmployeesModel> model, string UserId);
//		Task<APIResponse> GetAssignedEmployeesInBudgetLine(int OfficeId, int ProjectId, int BudgetLineId);

//	}
//}
