import { IBudgetLineModel } from '../models/budget-line.models';
import { ICurrencyList, IProjectList } from 'src/app/dashboard/accounting/gain-loss-report/gain-loss-report.model';
import { IProjectJobModel } from '../../project-jobs/project-jobsmodel';

export interface IBudgetLineDataSource {
  data: any;
  CurrencyList: ICurrencyList[];
  ProjectList: IProjectList[];
  BudgetLineDetailList: IBudgetLineModel[];
  ProjectJobList: IProjectJobModel[];
  Projectid: number;
}
