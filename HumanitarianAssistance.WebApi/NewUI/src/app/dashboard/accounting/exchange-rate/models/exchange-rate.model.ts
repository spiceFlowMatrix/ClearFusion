export interface IExchangeRateFilterModel {
  FromDateFilter: any;
  TillDateFilter: any;
  VerifiedFilter: any;
  totalCount: number;
  pageSize: number;
  pageIndex: number;
}

export interface IExchangeRateModel {
  ExchangeRateDate: any;
  VerificationStatus: string;
}

export interface IExchangeRateAddModel {
  CurrencyId: number;
  Date: any;
}

export interface ICurrencyListModel {
  CurrencyId: number;
  CurrencyName: string;
}

export interface IOfficeListModel {
  OfficeId: number;
  OfficeName: string;
}

export interface IExchangeRateModels {
  FromCurrencyId: number;
  ToCurrencyId: number;
  Date: any;
  Rate: number;
}

export interface IOfficeExchangeRateModels {
  GenerateExchangeRateModel: IExchangeRateModels[];
  OfficeId: number;
  SaveForAllOffices: boolean;
}
