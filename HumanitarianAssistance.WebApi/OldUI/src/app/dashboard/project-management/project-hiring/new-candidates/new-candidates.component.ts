import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-new-candidates',
  templateUrl: './new-candidates.component.html',
  styleUrls: ['./new-candidates.component.css']
})
export class NewCandidatesComponent implements OnInit {

  //#region "Variables"
  newEmployeeDataSource: any[];

  constructor() { }

  ngOnInit() {
    this.newEmployeeDataSource = [];
  }


  //#region "logEvent"
  logEvent(eventName, obj) {

  }
  //#endregion

  //#region "onFullDetailEvent"
  onFullDetailEvent(e) {

  }
  //#endregion

  //#region "onCvDocumentEvent"
  onCvDocumentEvent(e) {
    console.log(e);
  }
  //#endregion


  //#region "onActionEvent"
  onActionEvent(actionName, obj) {

  }
  //#endregion

}
