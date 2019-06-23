export interface IResponseData {
  data: any;
  statusCode: number;
  message: string;
  total?: number;
  IsExchangeRateVerified?: boolean;
}
