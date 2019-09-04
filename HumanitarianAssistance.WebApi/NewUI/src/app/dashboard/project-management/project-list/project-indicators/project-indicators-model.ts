export interface ProjectIndicatorFilterModel {
    pageIndex?: number;
    pageSize?: number;
    totalCount?: number;
    ProjectId: number;
    ProjectIndicatorId: number;
    Description: string;
    Questions?: number;

  }

  export interface IndicatorDetailModel {

    indicatorName: string;
    indicatorId: number;
    indicatorQuestions: IndicatorQuestionModel[];
  }

  export interface IndicatorQuestionModel {
    questionId: number;
    questiontext: string;
  }

  export interface ProjectIndicatorModel {
    ProjectIndicatorId: number;
    IndicatorName: string;
    IndicatorCode: string;
    Description: string;
    Questions?: number;
  }

export interface IIndicatorDataSource {
 ProjectId: number;
 ProjectindicatorDetail: any;

}
export interface IProjectIndicatorModel {
  ProjectIndicatorId?: number;
  IndicatorName: string;
  Description: string;
  ProjectId: number;
}
export interface IQuestionsDataSource {
  ProjectId: number;
  ProjectindicatorDetail: any;
 }
 export interface IQuestionDetailModel {
  IndicatorQuestionId?: number;
  IndicatorQuestion: string;
  QuestionType?: boolean;
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
