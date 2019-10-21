import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-category-populator',
  templateUrl: './category-populator.component.html',
  styleUrls: ['./category-populator.component.css']
})
export class CategoryPopulatorComponent implements OnInit {
  //#region "variables"

  categoryPopulatorTabFlag = true;

  tabs: any[] = [
    {
      id: 1,
      text: 'Balance Sheet'
    },
    {
      id: 2,
      text: 'Income / Expenditure '
    }
  ];
  //#endregion

  //#endregion

  constructor() {}

  ngOnInit() {}

  //#region "On tab Select"
  selectTab(e) {
    e.itemData.id === 1
      ? (this.categoryPopulatorTabFlag = true)
      : (this.categoryPopulatorTabFlag = false);
  }
  //#endregion
}
