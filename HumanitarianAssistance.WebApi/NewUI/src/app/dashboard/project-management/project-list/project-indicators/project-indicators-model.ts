export interface ProjectIndicatorFilterModel {
    pageIndex?: number;
    pageSize?: number;
    totalCount?: number;
    ProjectId: number;
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
    ProjectIndicatorCode: string;
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
