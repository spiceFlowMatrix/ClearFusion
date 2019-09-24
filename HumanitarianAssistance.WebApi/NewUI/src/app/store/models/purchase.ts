export interface Purchase {
}

export interface IPurchaseFilter {
  InventoryType: IInventoryType[];
  Offices: IOffice[];
  ReceiptType: IReceiptType[];
  Currencies: ICurrency[];
  Projects: IProject[];
  StoreInventory: IStoreInventory[];
}

export interface IInventoryType {
  Id: number;
  InventoryName: string;

}

export interface IOffice {
OfficeId: number;
OfficeName: string;
OfficeCode: string;
}

export interface IReceiptType {
  ReceiptTypeId: number;
  ReceiptTypeName: string;
}

export interface ICurrency {
  CurrencyId: number;
  CurrencyName: string;
  CurrencyCode: string;
}

export interface IProject {
  ProjectId: number;
  ProjectName: string;
  ProjectCode: string;
}

export interface IStoreInventory {
  InventoryId: number;
  InventoryName: string;
  InventoryCode: string;
}
