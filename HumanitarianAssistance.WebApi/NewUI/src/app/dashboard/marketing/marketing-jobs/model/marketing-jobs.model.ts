export interface MarketingJobDetailModel {
    JobId?: number;
    JobName?: string;
    JobCode?: number;
    UnitRate?: number;
    FinalRate?: number;
    FinalPrice?: number;
    TotalPrice?: number;
    ActualPrice?: number;
    Discount?: number;
    DiscountPercent?: any;
    EndDate?: any;
    StartDate?: string;
    ContractId?: number;
    Minutes?: number;
    IsApproved?: boolean;
    count?: number;
    CreatedBy?: string;
    IsAgreementApproved?: boolean;
    ClientId?: number;
    ClientName?: string;
    CurrencyCode?: string;
    _IsLoading?: boolean;
}

export interface PhaseDetailsModel {
JobPhaseId?: number;
JobPhaseName?: string;
}

export interface ContractDetailsModel {
    ContractId?: number;
    ClientName?: string;
    }

    export interface ContractsListByClient {
      ContractId?: number;
      ClientId?: number;
      ClientName?: string;
      ContractByClients?: string;
      UnitRate?: number;
    }

    export interface JobFilterModel {
      IsApproved?: boolean;
      YesOrNo?: string;
      ContractId?: number;
      TotalPrice?: number;
      unitRate?: number;
      FilterType?: string;
      JobName?: string;
      JobId?: number;
    }

    export interface FilterJobModel {
      Value?: string;
      FinalPrice?: boolean;
      JobId?: boolean;
      JobName?: boolean;
      Approved?: boolean;
    }

    export interface JobPaginationModel {
      pageIndex?: number;
      pageSize?: number;
    }

    export interface InvoiceModel {
      ClientName?: string;
      CurrencyCode?: string;
      EndDate?: Date;
      FinalPrice?: number;
      IsApproved?: boolean;
      IsScheduleExist?: boolean;
      JobId?: number;
      JobName?: string;
      JobRate?: number;
      TotalMinutes?: number;
      TotalRunningMinutes?: number;
      InvoiceId?: number;
    }
