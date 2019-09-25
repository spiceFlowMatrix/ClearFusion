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

  pageIndex: any;
  pageSize: any;
  totalCount: any;
}
