import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  items = [{Name:"abc",Qty:21},{Name:"cxx",Qty:11}];

  constructor() { }

  ngOnInit() {
  }

}
