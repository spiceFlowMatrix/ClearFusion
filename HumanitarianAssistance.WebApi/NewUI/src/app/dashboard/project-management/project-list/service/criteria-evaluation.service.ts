import { Injectable } from '@angular/core';
import {
  DonorCEModel,
  EligibilityCEModel,
  ProductAndServiceCEModel,
  FeasibilityCEModel,
  PriorityCEmodel,
  RiskSecurityModel,
  FinancialProfitabilityModel,
  TargetBeneficiaryModel
} from '../project-details/models/project-details.model';
import { GlobalService } from 'src/app/shared/services/global-services.service';
import { IPriorityOtherModel, IFeasibilityExpert, ICEAssumptionModel, ICEAgeDEtailModel, ICEOccupationModel, ICEDonorEligibilityModel, ICEisCESubmitModel } from '../criteria-evaluation/criteria-evaluation.model';

@Injectable({
  providedIn: 'root'
})

export class CriteriaEvaluationService {
  constructor(private globalService: GlobalService) { }
  //#region get criteria evaluation detail by projectId
  GetCriteriaEvalDetailsByProjectId(url: string, id: number) {
    return this.globalService.getListById(url, id);
  }
  //#endregion

  //#region "AddEditDonorCriteriaEvaluation Details"
  AddEditDonorCriteriaEvaluationForm(url: string, data: DonorCEModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddEditDonorCriteriaEvaluation Details"
  AddEditProductServiceCEForm(url: string, data: ProductAndServiceCEModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddEditEligibilityCEForm Details"
  AddEditEligibilityCriteriaEForm(url: string, data: EligibilityCEModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddEditFeasibilityCEForm Details"
  AddEditFeasibilityCriteriaEForm(url: string, data: FeasibilityCEModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddEditPriorityCEForm Details"
  AddEditPriorityCriteriaEForm(url: string, data: PriorityCEmodel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddEditFinancialProfitability Details"
  AddEditFinancialCriteriaProfitability(
    url: string,
    data: FinancialProfitabilityModel
  ) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddEditRiskSecurityCEForm Details"
  AddEditRiskSecurityCriteriaEForm(url: string, data: RiskSecurityModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "AddEditTargetBeneficiary"
  AddEditTargetBeneficiary(url: string, data: TargetBeneficiaryModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region GetAllProjectList
  GetAllProjectList(url: string) {
    return this.globalService.getList(url);
  }
  //#endregion
 //#region "GetAllCurrency"
 GetAllCurrency(url: string) {
  return this.globalService.getList(url);
}
//#endregion
  //#region "AddEditPriorityOther"
  AddEditPriorityOther(url: string, data: IPriorityOtherModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region "AddEditPriorityOther"
  AddEditOccupationOther(url: string, data: ICEOccupationModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "addeditAssumptiondetail"
  AddEditAssumptionDetail(url: string, data: ICEAssumptionModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "addeditAssumptiondetail"
  AddEditAgeGroupDetail(url: string, data: ICEAgeDEtailModel) {
    return this.globalService.post(url, data);
  }
  //#endregion

  //#region "addEditFeasibilityExpert"
  AddEditFeasibilityExpert(url: string, data: IFeasibilityExpert) {
    return this.globalService.post(url, data);
  }
  //#endregion
  //#region get criteria evaluation detail by projectId
  GetPriorityOtherDetailByProjectId(url: string, id: number) {
    return this.globalService.getListById(url, id);
  }
  //#endregion

  DeletePriorityDetailByPriorityId(url: string, id: number) {
    return this.globalService.post(url, id);
  }


  //#region "AddEditPriorityOther"
  AddEditDonorEligibilityOther(url: string, data: ICEDonorEligibilityModel) {
    return this.globalService.post(url, data);
  }
  //#endregion
//#region "AddEditPriorityOther"
AddIsCriteriaEvaluationSubmit(url: string, data: ICEisCESubmitModel) {
  return this.globalService.post(url, data);
}
//#endregion


}
