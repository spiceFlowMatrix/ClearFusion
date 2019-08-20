using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteItemOrderCommand:BaseModel,IRequest<ApiResponse>
    {
        public string OrderId { get; set; }
        public string Purchase { get; set; }                                // PurchaseID DropDown
        public string InventoryItem { get; set; }                           // InventoryID DropDown
        public int IssuedQuantity { get; set; }                             // Text Box
        public bool MustReturn { get; set; }                                // CheckBox
        public bool Returned { get; set; }                                // CheckBox
        public int IssuedToEmployeeId { get; set; }                         // EmployeeID DropDown
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public string IssueVoucherNo { get; set; }
        public string Remarks { get; set; }
        public long Project { get; set; }
        public string IssedToLocation { get; set; }
        public int StatusAtTimeOfIssue { get; set; }

        public string CreatedBy { get; set; }
    }
}
