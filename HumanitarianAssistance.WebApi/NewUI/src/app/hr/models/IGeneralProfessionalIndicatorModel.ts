export interface IGeneralProfessionalIndicatorModel {
  SequenceNumber: number;
  Question: string;
  Score: number;
  Remarks: string;
}
export class EmployeeAppraisalQuestionList {
  EmployeeAppraisalQuestionsId?: number;
  QuestionEnglish: any;
  QuestionDari: any;
  AppraisalGeneralQuestionsId: any;
  SequenceNo: any;
  Score: any;
  Remarks: any;
  AppraisalScore: any;
}

export interface IAppraisalMemberList {
  EmployeeCode: string;
  EmployeeName: string;
  Type: string;
  EmployeeId?: number;
  EmployeeAppraisalTeamMemberId: number;
}

export interface IEmployeeDetailModel {
  EmployeeId: any;
  EmployeeCode: string;
  EmployeeName: string;
  Type: string;
}
export interface IEmployeeListModel {
  EmployeeId?: number;
  EmployeeName: string;
}

export interface ITrainigDetailModel {
  TrainingProgramBasedOn: string;
  Program: string;
  Participated: string;
  CatchLevel: string;
  RefresherTrm: string;
  OtherRecommemenedTraining: string;
  EmployeeEvaluationTrainingId: number;
}

export interface IAppraisalStrongPoints {
  StrongPoints: string;
  AppraisalStrongPointsId: number;
}

export interface IAppraisalWeakPoints {
  WeakPoints: string;
  AppraisalWeakPointsId: number;
}

export interface IAppraisalDetailModel {
  EmployeeAppraisalDetailsId?: number;
  EmployeeId: number;
  AppraisalPeriod: number;
  CurrentAppraisalDate: any;
  FinalResultQues1: string;
  FinalResultQues2: string;
  FinalResultQues3: string;
  FinalResultQues4: string;
  FinalResultQues5: string;
  CommentsByEmployee: string;
  AppraisalMembers: IAppraisalMemberList[];
  AppraisalTraining: ITrainigDetailModel[];
  AppraisalStrongPoints: IAppraisalStrongPoints[];
  AppraisalWeakPoints: IAppraisalWeakPoints[];
  GeneralProfessionalIndicatorQuestion: EmployeeAppraisalQuestionList[];
}
