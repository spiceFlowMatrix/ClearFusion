export interface IPriorityOtherModel {
  PriorityOtherDetailId: number;
  Name: string;
  ProjectId:number;

  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
}

export interface IFeasibilityExpert{
  ExpertOtherDetailId: number;
  Name: string;
  ProjectId:number;

  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
}
export interface ICEAssumptionModel{
  AssumptionDetailId: number;
  Name: string;
  ProjectId:number;

  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
}

export interface ICEAgeDEtailModel{
  AgeGroupOtherDetailId: number;
  Name: string;
  ProjectId:number;

  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
}

export interface ICEOccupationModel{
  OccupationOtherDetailId: number;
  Name: string;
  ProjectId:number;

  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
}
export interface ICEDonorEligibilityModel{
  DonorEligibilityDetailId: number;
  Name: string;
  ProjectId:number;

  _IsDeleted?: boolean;
  _IsLoading?: boolean;
  _IsError?: boolean;
}

export interface ICEisCESubmitModel{
 
  ProjectId?:number;
  IsCriteriaEvaluationSubmit?:boolean

  
}