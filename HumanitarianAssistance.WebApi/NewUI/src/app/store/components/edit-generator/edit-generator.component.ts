import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-generator',
  templateUrl: './edit-generator.component.html',
  styleUrls: ['./edit-generator.component.scss']
})
export class EditGeneratorComponent implements OnInit {

  constructor(private router : Router) { }

  ngOnInit() {
  }
  backToDetails(){
  this.router.navigate(['store/generator/detail',1])
  }
}
