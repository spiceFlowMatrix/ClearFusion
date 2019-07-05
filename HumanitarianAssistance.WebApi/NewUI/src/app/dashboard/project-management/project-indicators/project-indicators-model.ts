export interface ProjectIndicatorFilterModel {
    pageIndex?: number;
    pageSize?: number;
    totalCount?: number;
  }

  export interface IndicatorDetailModel {

    indicatorName: string,
    indicatorId: number,
    indicatorQuestions: IndicatorQuestionModel[]
  }

  export interface IndicatorQuestionModel {
    questionId: number,
    questiontext: string
  }

  export interface ProjectIndicatorModel {
    projectIndicatorId: number,
    projectIndicatorName: string,
    projectIndicatorCode: string
  }
