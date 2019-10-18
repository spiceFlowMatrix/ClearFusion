export interface CategoryModel {
    CategoryId?: number;
    CategoryName?: string;
}

export interface ClientDetailsModel {
    ClientId?: number;
    ClientCode?: string;
    ClientName?: string;
    FocalPoint?: string;
    Position?: string;
    Phone?: string;
    Email?: string;
    PhysicialAddress?: string;
    History?: string;
    ClientBackground?: string;
    CategoryId?: number;
    CategoryName?: string;
    Count?: number;
}

export interface ClientNameModel {
    ClientId?: number;
    ClientName: string;
}

export interface FilterModel {
    ContractId?: number;
    ClientId?: number;
    ClientName?: string;
    CurrencyId?: string;
    UnitRate?: number;
    ActivityTypeId?: number;
    FilterType?: string;
    IsApproved?: boolean;
    YesOrNo?: string;
}

export interface FilterClientModel {
    ClientId?: number;
    ClientName?: string;
    CategoryId?: string;
    Email?: string;
    Position?: string;
}
