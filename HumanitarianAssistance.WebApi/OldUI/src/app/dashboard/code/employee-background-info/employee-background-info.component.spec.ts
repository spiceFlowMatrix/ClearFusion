import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeBackgroundInfoComponent } from './employee-background-info.component';

describe('EmployeeBackgroundInfoComponent', () => {
  let component: EmployeeBackgroundInfoComponent;
  let fixture: ComponentFixture<EmployeeBackgroundInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeBackgroundInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeBackgroundInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
