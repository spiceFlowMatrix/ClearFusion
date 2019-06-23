export interface MonitoringModel {
  MonitoringReviewModel: MonitoringReviewModel[],
  PositivePoints: string;
  NegativePoints: string;
  Recommendations: string;
  MonitoringDate: any;
  Remarks: string;
  ProjectId: number;
  ActivityId: number;
  ProjectMonitoringReviewId? : number
  Rating? : number;
}

export interface MonitoringReviewModel {
  ProjectIndicatorId: number,
  MonitoringIndicatorId? : number,
  IndicatorName? : string,
  TotalScore? : number,
  IndicatorQuestions: MonitoringReviewQuestionModel[],
  IsDeleted? : boolean
}

export interface MonitoringReviewQuestionModel {
  QuestionId: number,
  Question: string,
  Verification? : string,
  VerificationId? : number,
  Score? : number,
  MonitoringIndicatorQuestionId? : number,
}
