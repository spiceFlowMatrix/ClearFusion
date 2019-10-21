using HumanitarianAssistance.Application.Infrastructure;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeePayrollAccountModel : BaseModel
    {
        public int PayrollHeadId { get; set; }
        public int EmployeeId { get; set; }
        public int PayrollHeadTypeId { get; set; }
        public string PayrollHeadName { get; set; }
        public string Description { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public string SalaryHeadType { get; set; }
    }
}