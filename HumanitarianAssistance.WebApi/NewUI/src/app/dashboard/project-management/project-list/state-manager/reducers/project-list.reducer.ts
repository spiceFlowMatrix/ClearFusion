// import * as projectActions from '../actions/project-list.actions';
// import { ProjectDetailModel } from '../../project-details/models/project-details.model';

// let initialState: ProjectDetailModel[] = [];
// let projectDetailInitialState: ProjectDetailModel = {};
// let successFailedState: any;


// export function showProjectList_Reducer(state = initialState, action: projectActions.projectActions) {
//     switch (action.type) {
//         case projectActions.SHOW_LIST:
//             return {
//                 data: [...state],
//                 statusCode: 200
//             };
//         default:
//             return state;
//     }
// }

// export function projectDetail_Reducer(state = projectDetailInitialState, action: projectActions.projectDetailActions) {
//     switch (action.type) {
//         case projectActions.ADD_PROJECT_DETAIL:
//             state = projectDetailInitialState;
//             return [state, action.projectDetail];
//         default:
//             return state;
//     }
// }

// //NOTE: Success / fail
// export function successFail_Reducer(state = projectDetailInitialState, action: projectActions.sucessFailActions) {
//     switch (action.type) {
//         case projectActions.ADD_PROJECT_DETAIL_FAILED:
//             // state = projectDetailInitialState;
//             return [state];
//         case projectActions.ADD_PROJECT_DETAIL_SUCCESS:
//             // state = projectDetailInitialState;
//             return [state];
//         default:
//             return state;
//     }
// }
