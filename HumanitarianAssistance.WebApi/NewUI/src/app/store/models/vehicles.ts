export interface Vehicles {
}

export interface IVehicleList {
    PlateNo?: string;
    Driver?: string;
    FCRate?: string;
    TotalMileage?: string;
    TotalCost?: string;
    OriginalCost?: string;
}


export interface IVehicleTrackerFilter {
  TotalCost?: number;
  EmployeeId?: number;
  OfficeId?: number;
  PlateNo?: string;
  pageSize: 10;
  pageIndex: 0;
}

export interface ILogs {
    EventType?: string;
    EventBy?: string;
    EventOn?: string;
    Detail?: string;

}
