
// import { Injectable } from '@angular/core';
// import { Actions, Effect, ofType } from '@ngrx/effects';
// import { Observable, pipe } from 'rxjs';
// import { SHOW_LIST, ADD_PROJECT_DETAIL, AddProjectDetail_Action, AddProjectDetail_ActionFailed, AddProjectDetail_ActionSuccess } from '../actions/project-list.actions';
// import { switchMap, tap, take, map } from 'rxjs/operators';
// import { ProjectListService } from '../../service/project-list.service';
// import { AppUrlService } from '../../../../../shared/services/app-url.service';
// import { GLOBAL } from '../../../../../shared/global';
// import { HttpClient } from '@angular/common/http';

// @Injectable()
// export class ProjectListEffects {

//     constructor(
//         private projectService: ProjectListService,
//         private actions$: Actions,
//         private appURL: AppUrlService,
//         private http: HttpClient
//     ) { }

//     // @Effect()
//     // addProjectDetail$: Observable<any> = this.actions$.pipe(
//     //     ofType(ADD_PROJECT_DETAIL), // whenever this action call this event triggered
//     //     take(1),
//     //     switchMap((data: AddProjectDetail_Action) => {
//     //         return this.projectService.AddProjectDetail(this.appURL.getApiUrl() + GLOBAL.API_Project_AddEditProjectDetail, data.projectDetail)
//     //     },
//     //         (error) => {
//     //             // Observable.of({ type: ADD_TODO_ERROR }))
//     //         }
//     //     ));

//     @Effect()
//     addProjectDetail: Observable<any> = this.actions$.pipe(
//         ofType<AddProjectDetail_Action>(ADD_PROJECT_DETAIL),
//         map(data => data.projectDetail),
//         switchMap((payload) => this.projectService
//             .AddProjectDetail(this.appURL.getApiUrl() + GLOBAL.API_Project_AddEditProjectDetail, payload).pipe(
//                 map(data => {
//                     // this.sto
//                     // new AddProjectDetail_ActionSuccess()
//                 },
//                     error => {
//                         // new AddProjectDetail_ActionSuccess()
//                     }
//                 ),
//                 // (error) => Observable.throw(new AddProjectDetail_ActionFailed())
//             )
//         )
//     );
// }
