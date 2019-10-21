export interface IProjectFilterModel {
  FilterValue?: string;
  ProjectNameFlag?: boolean;
  DateFlag?: boolean;
  ProjectIdFlag?: boolean;
  ProjectCodeFlag?: boolean;
  DescriptionFlag?: boolean;

  pageIndex?: number;
  pageSize?: number;
  totalCount?: number;
}

export  interface IWinLossProjectDetailModel {
  //reviewer
  FileName: string;
  FilePath: string;
  // winloss
  WinLossMessage: string;
  WinLossFileName: string;
  WinlossFilePath: string;
  IsReviewApproved?: boolean;
  // * project Proposal accept reject value
  IsProposalAccept?: boolean;
}
