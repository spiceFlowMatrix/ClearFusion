import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficeMasterComponent } from './office-master.component';

describe('OfficeMasterComponent', () => {
  let component: OfficeMasterComponent;
  let fixture: ComponentFixture<OfficeMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OfficeMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OfficeMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
