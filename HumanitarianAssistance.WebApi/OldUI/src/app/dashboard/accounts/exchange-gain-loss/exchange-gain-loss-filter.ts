export class ExchangeGainLossFilter {
  public static ExcGainFilter: ExchangeGainLossFilterModel;
}

class ExchangeGainLossFilterModel {
  OfficeId: any;
  JournalId: any;
  VoucherList: any;
  ProjectList: any;
  FromDate: any;
  ToDate: any;
  DateOfComparison: any;
  ComparisonCurrencyId: number;
  Skip: number;
  Take: number;
}
