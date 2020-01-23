import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeControlPanelComponent } from './employee-control-panel.component';

describe('EmployeeControlPanelComponent', () => {
  let component: EmployeeControlPanelComponent;
  let fixture: ComponentFixture<EmployeeControlPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeControlPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeControlPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
