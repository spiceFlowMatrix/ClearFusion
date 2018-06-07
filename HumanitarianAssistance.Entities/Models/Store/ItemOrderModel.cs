using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Store
{
    public class ItemOrderModel
    {
		public string OrderId { get; set; }
		public string Purchase { get; set; }								// PurchaseID DropDown
		public string InventoryItem { get; set; }							// InventoryID DropDown
		public int IssuedQuantity { get; set; }								// Text Box
		public bool MustReturn { get; set; }								// CheckBox
		public int IssuedToEmployeeId { get; set; }							// EmployeeID DropDown
		public DateTime IssueDate { get; set; }								
		public DateTime ReturnedDate { get; set; }
	}
}
