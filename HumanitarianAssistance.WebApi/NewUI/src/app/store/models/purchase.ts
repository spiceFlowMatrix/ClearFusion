export interface Purchase {
}

export interface IPurchaseFilter {
  InventoryType: IDropDownModel[];
  Offices: IDropDownModel[];
  ReceiptType: IDropDownModel[];
  Currencies: IDropDownModel[];
  Projects: IDropDownModel[];
  StoreInventory: IDropDownModel[];
}


export interface IDropDownModel {
  name: string;
  value: number;
}

export interface IFilterValueModel {
  InventoryTypeId: any;
  ReceiptTypeId: any;
  OfficeId: any;
  CurrencyId: any;
  ProjectId: any;
  PurchaseStartDate: any;
  PurchaseEndDate: any;
  IssueStartDate: any;
  IssueEndDate: any;
  InventoryId: any;
  ItemGroupId: any;
  ItemId: any;
  JobId: any;

  PageIndex?: number;
  PageSize?: number;
  TotalCount?: number;
}

export interface IPurchaseList {
  Id: number;
  Item: string;
  PurchasedBy: string;
  Project: string;
  OriginalCost: number;
  DepreciatedCost: number;
}
export interface IProcurementList {
  EmployeeName?: string;
  IssueDate?: string;
  IssueId?: number;
  MustReturn?: boolean;
  ProcuredAmount?: string;
  Returned: boolean
}
