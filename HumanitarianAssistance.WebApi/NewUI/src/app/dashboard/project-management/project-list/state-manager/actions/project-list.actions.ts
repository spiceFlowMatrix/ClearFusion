// import { Action } from "@ngrx/store";
// import { ProjectDetailModel } from "../../project-details/models/project-details.model";

// //#region "constants"
// export const VIEW_PROJECT_DETAIL = "VIEW_PROJECT_DETAIL";
// export const ADD_PROJECT_DETAIL = "ADD_PROJECT_DETAIL";
// export const SHOW_LIST = "SHOW_LIST";
// export const ADD_PROJECT_DETAIL_FAILED = "ADD_PROJECT_DETAIL_FAILED";
// export const SET_PROJECT_DETAIL = "SET_PROJECT_DETAIL";
// export const ADD_PROJECT_DETAIL_SUCCESS = "ADD_PROJECT_DETAIL_SUCCESS";

// //#endregion


// //#region "classes"
// export class ViewProjectDetail_Action implements Action {
//     readonly type = VIEW_PROJECT_DETAIL;
//     constructor(public projectId: any) { }
// }

// export class SetProjectDetail_Action implements Action {
//     readonly type = SET_PROJECT_DETAIL; //SetProjectDetail_Action
//     constructor(public projectDetail: ProjectDetailModel) { }
// }

// export class AddProjectDetail_Action implements Action {
//     readonly type = ADD_PROJECT_DETAIL;
//     constructor(public projectDetail: ProjectDetailModel) { }
// }

// export class AddProjectDetail_ActionSuccess implements Action {
//     readonly type = ADD_PROJECT_DETAIL_SUCCESS;
// }
// export class AddProjectDetail_ActionFailed implements Action {
//     readonly type = ADD_PROJECT_DETAIL_FAILED;
// }

// export class ShowProjectList implements Action {
//     readonly type = SHOW_LIST;
// }
// //#endregion





// export type projectDetailActions = ViewProjectDetail_Action | AddProjectDetail_Action;
// export type projectActions = ShowProjectList;
// export type sucessFailActions = AddProjectDetail_ActionSuccess | AddProjectDetail_ActionFailed;