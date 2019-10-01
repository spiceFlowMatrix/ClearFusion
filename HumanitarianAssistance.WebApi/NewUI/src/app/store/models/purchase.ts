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
  SubItems: IProcurementList[];
}
export interface IProcurementList {
  EmployeeName?: string;
  IssueDate?: string;
  IssueId?: number;
  MustReturn?: boolean;
  ProcuredAmount?: string;
  Returned: boolean;
}

export interface IAddEditPurchaseModel {
  PurchaseId?: number;
  InventoryTypeId?: any;
  PurchaseName: string;
  InventoryId: any;
  ItemGroupId?: any;
  InventoryItem: any;
  OfficeId: any;
  ProjectId: any;
  BudgetLineId: any;
  PurchaseOrderNo: any;
  PurchaseDate: any;
  InvoiceDate: any;
  DeliveryDate: any;
  InvoiceNo: any;
  ReceivedFromLocation: any;
  PurchasedById: any;

  ReceiptTypeId: any;
  Status: any;

  Quantity: any;
  Currency: any;
  UnitCost: any;
  AssetTypeId: any;
  UnitType: any;
  ApplyDepreciation: boolean;
  DepreciationRate: any;
  TimezoneOffset: any;

  PageIndex?: number;
  PageSize?: number;
  TotalCount?: number;
}
