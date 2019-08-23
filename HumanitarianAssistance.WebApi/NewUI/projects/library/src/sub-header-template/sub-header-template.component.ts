import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'lib-sub-header-template',
  templateUrl: './sub-header-template.component.html',
  styleUrls: ['./sub-header-template.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SubHeaderTemplateComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
