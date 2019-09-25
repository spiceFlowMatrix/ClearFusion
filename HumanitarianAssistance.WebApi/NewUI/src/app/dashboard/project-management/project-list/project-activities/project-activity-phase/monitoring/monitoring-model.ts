export interface MonitoringModel {
  MonitoringReviewModel: MonitoringReviewModel[];
  PositivePoints: string;
  NegativePoints: string;
  Recommendations: string;
  MonitoringDate: any;
  Remarks: string;
  ProjectId: number;
  ActivityId: number;
  ProjectMonitoringReviewId?: number;
  Rating?: number;
}

export interface MonitoringReviewModel {
  ProjectIndicatorId: number;
  MonitoringIndicatorId?: number;
  IndicatorName?: string;
  TotalScore?: number;
  IndicatorQuestions: MonitoringReviewQuestionModel[];
  IsDeleted?: boolean;
  QuestionTypeId: number;
}

export interface MonitoringReviewQuestionModel {
  IndicatorQuestionId?: number;
  IndicatorQuestion: string;
  QuestionType?: number;
  QuestionTypeName?: string;
  ProjectIndicatorId: number;
  IsDeleted?: boolean;
  VerificationSources: IVerificationSourceModel[];
}
export interface IVerificationSourceModel {
  VerificationSourceId?: number;
  VerificationSourceName: string;
  IndicatorQuestionId?: number;
  IsDeleted?: boolean;
}
