import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeAdvanceListComponent } from './employee-advance-list.component';

describe('EmployeeAdvanceListComponent', () => {
  let component: EmployeeAdvanceListComponent;
  let fixture: ComponentFixture<EmployeeAdvanceListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeAdvanceListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeAdvanceListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
