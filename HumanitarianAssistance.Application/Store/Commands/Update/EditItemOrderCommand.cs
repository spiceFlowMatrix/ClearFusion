using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditItemOrderCommand:BaseModel,IRequest<ApiResponse>
    {
        public long OrderId { get; set; }
        public long Purchase { get; set; }                                // PurchaseID DropDown
        public long InventoryItem { get; set; }                           // InventoryID DropDown
        public int IssuedQuantity { get; set; }                             // Text Box
        public bool MustReturn { get; set; }                                // CheckBox
        public bool Returned { get; set; }                                // CheckBox
        public int IssuedToEmployeeId { get; set; }                         // EmployeeID DropDown
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public long VoucherNo { get; set; }
        public string Remarks { get; set; }
        public long Project { get; set; }
        public string IssedToLocation { get; set; }
        public int StatusAtTimeOfIssue { get; set; }

        public string CreatedBy { get; set; }

    }
}
