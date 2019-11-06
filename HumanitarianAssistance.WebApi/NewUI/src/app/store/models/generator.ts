export interface IGeneratorTrackerFilter {
  TotalCost?: number;
  OfficeId?: number;
  ModelYear?: number;
  Voltage?: number;
  DisplayCurrency?: number;
  pageSize: 10;
  pageIndex: 0;
}

export interface IGeneratorList {
  Voltage?: string;
  FuelConsumptionRate?: string;
  IncurredUsage?: string;
  TotalUsage?: string;
  TotalCost?: string;
  OriginalCost?: string;
}

export interface IGeneratorItem {
  GeneratorId: number;
  Voltage?: number;
  StartingUsage?: number;
  IncurredUsage?: number;
  MobilOilConsumptionRate?: number;
  ModelYear?: number;
  OfficeId?: number;
  FuelConsumptionRate?: number;
}
