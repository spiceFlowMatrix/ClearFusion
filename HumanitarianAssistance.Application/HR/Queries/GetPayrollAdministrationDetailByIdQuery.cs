using MediatR;
using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.HR.Models;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPayrollAdministrationDetailByIdQuery: IRequest<object>
    {
        public List<int> EmpIds { get; set; }
        public int Month { get; set; }
        public List<int> OfficeId { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class PayrollAdministrationModel {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Currency { get; set; }
        public double GrossSalary { get; set; }
        public double NETSalary { get; set; }
        public bool IsApproved { get; set; }
        public List<AccumulatedPayrollHeads> AccumulatedPayrollHeadList {get; set;}
        public List<SavedAccumulatedPayrollHeads> SavedAccumulatedPayrollHeadList { get; set; }
    }
}