import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-purchase',
  templateUrl: './add-purchase.component.html',
  styleUrls: ['./add-purchase.component.scss']
})
export class AddPurchaseComponent implements OnInit {
  animalControl = new FormControl('', [Validators.required]);
  selectFormControl = new FormControl('', Validators.required);
  animals = [
    {name: 'Dog',value:  1},
    {name: 'Cat', value: 2},
    {name: 'Cow', value: 3},
    {name: 'Fox', value: 4},
  ];
  constructor() { }

  ngOnInit() {
  }
  getSelectedValue(event){
    console.log(event);
  }
}
