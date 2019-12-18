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
  DisplayCurrency?: number;
  DepreciationComparisionDate: any;
  SelectedColumns?: any[];

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
  subItemSubtitle?: string;
  subItems: IProcurementList[];
}

export interface IPurchaseFilterConfigColList {
  PurchaseId: number;
  ItemName: string;
  ItemId: string;
  EmployeeName: string;
  ProjectName: string;
  OriginalCost: number;
  DepreciatedCost: number;
  PurchasedQuantity: number;
  ItemCodeDescription: string;
  BudgetLineName: string;
  ChasisNo: string;
  CurrencyName: string;
  DepreciationRate: any;
  EngineSerialNo: string;
  IdentificationNo: string;
  ItemCode: string;
  MasterInventoryCode: string;
  ModelType: string;
  OfficeCode: string;
  PurchaseOrderNo: any;
  InvoiceDate: any;
  ReceivedFromLocation: string;
  Status: string;
  subItems: IProcurementList[];
}

export interface IProcurementList {
  EmployeeName?: string;
  IssueDate?: string;
  OrderId?: number;
  MustReturn?: string;
  ProcuredAmount?: string;
  Returned: string;
  ReturnedOn?: any;
  EmployeeId?: any;
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
  TransportItemId?: number;
  ItemGroupTransportCategory?: number;
  ItemTransportCategory?: number;
  PurchasedVehicleList: IPurchasedVehicleModel[];
  PurchasedGeneratorList: IPurchasedGeneratorModel[];

  PageIndex?: number;
  PageSize?: number;
  TotalCount?: number;
}

export interface IAddEditProcurementModel {
  OrderId?: number;
  Purchase?: number;
  InventoryItem: string;
  IssuedQuantity: any;
  MustReturn?: boolean;
  Returned?: boolean;
  IssuedToEmployeeId: number;
  IssueDate: any;
  ReturnedDate?: any;
  Remarks?: string;
  IssedToLocation: number;
  StatusAtTimeOfIssue: number;
  Project: number;
}

export interface IDeleteProcurementModel {
  PurchaseId?: number;
  OrderId: number;
}

export interface IPurchasedVehicleModel {
  Id?: number;
  PlateNo: string;
  EmployeeId: number;
  StartingMileage: number;
  IncurredMileage: number;
  FuelConsumptionRate: number;
  MobilOilConsumptionRate: number;
  OfficeId: number;
  ModelYear: number;
  PurchaseId?: number;
}

export interface IPurchasedGeneratorModel {
  Id?: number;
  Voltage: number;
  EmployeeId: number;
  StartingUsage: number;
  IncurredUsage: number;
  FuelConsumptionRate: number;
  MobilOilConsumptionRate: number;
  OfficeId: number;
  ModelYear: number;
  PurchaseId?: number;
}

export interface IPurchasedFiles {
  Id: number;
  File: any;
  DocumentTypeId: number;
  Date: any;
  Filename: string;
  UploadedBy: string;
  DocumentTypeName: string;
  SignedUrl: string;
}

export interface IPurchaseItemDataSource {
  PurchaseId: number;
  PurchaseName: string;
}

export interface IMonthlyBreakDown {
  StartingMileage?: number;
  IncurredMileage?: number;
  StartingUsage?: number;
  IncurredUsage?: number;
  StandardMobilOilConsumptionRate: number;
  StandardFuelConsumptionRate: number;
  StartingCost: number;
  UsageAnalysisBreakDownList: any[];
  CostAnalysisBreakDownList: any[];

}
