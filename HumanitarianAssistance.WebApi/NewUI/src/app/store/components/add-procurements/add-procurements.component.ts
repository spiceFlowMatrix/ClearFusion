import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-procurements',
  templateUrl: './add-procurements.component.html',
  styleUrls: ['./add-procurements.component.scss']
})
export class AddProcurementsComponent implements OnInit {

  addProcurementForm: FormGroup;

  constructor(private fb: FormBuilder) {

    this.addProcurementForm = this.fb.group({
      'InventoryTypeId': [null, [Validators.required]],
      'InventoryId': [null, [Validators.required]],
      'ItemGroupId': [null, [Validators.required]],
      'ItemId': [null, [Validators.required]],
      'PurchaseName': [null, [Validators.required]],
      'IssuedQuantity': [null, [Validators.required]],
      'IssuedToEmployeeId': [null, [Validators.required]],
      'IssueDate': [null, [Validators.required]],
      'ProjectId': [null, [Validators.required]],
      'IssuedToLocation': [null, [Validators.required]],
      'StatusId': [null, [Validators.required]],
      'MustReturn': [null],
    });
  }

  ngOnInit() {
  }

}
